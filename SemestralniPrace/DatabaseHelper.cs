using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace SemestralniPrace
{
    internal class DatabaseHelper
    {
        private const string DatabaseFile = "data.db";
        private const string ConnectionString = "Data Source=" + DatabaseFile + ";Version=3;";

        public static string GetConnectionString() => ConnectionString;

        public static void InitializeDatabase()
        {
            if (!File.Exists(DatabaseFile))
            {
                SQLiteConnection.CreateFile(DatabaseFile);
                using (var conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = @"
                        CREATE TABLE ConsumptionRecords (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ConsumptionType TEXT NOT NULL,
                            Amount REAL NOT NULL,
                            Date TEXT NOT NULL,
                            Note TEXT
                        );
                    ";
                    using (var command = new SQLiteCommand(sql, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static List<ConsumptionRecord> GetAllRecords()
        {
            var records = new List<ConsumptionRecord>();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM ConsumptionRecords ORDER BY Date DESC";

                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new ConsumptionRecord
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ConsumptionType = reader["ConsumptionType"].ToString(),
                            Amount = Convert.ToDouble(reader["Amount"]),
                            Date = DateTime.Parse(reader["Date"].ToString()),
                            Note = reader["Note"].ToString()
                        });
                    }
                }
            }

            return records;
        }

        public static List<ConsumptionRecord> GetFilteredRecords(DateTime? from, DateTime? to, List<string> types, string noteSearch)
        {
            var records = new List<ConsumptionRecord>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var sqlBuilder = new StringBuilder("SELECT * FROM ConsumptionRecords WHERE 1=1 ");

                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = connection;

                    if (from.HasValue)
                    {
                        sqlBuilder.Append("AND Date >= @from ");
                        cmd.Parameters.AddWithValue("@from", from.Value.ToString("yyyy-MM-dd"));
                    }

                    if (to.HasValue)
                    {
                        sqlBuilder.Append("AND Date <= @to ");
                        cmd.Parameters.AddWithValue("@to", to.Value.ToString("yyyy-MM-dd"));
                    }

                    if (types != null && types.Count > 0 && !(types.Count == 1 && types[0] == "Všechny"))
                    {
                        var typeParams = new List<string>();
                        for (int i = 0; i < types.Count; i++)
                        {
                            string paramName = $"@type{i}";
                            typeParams.Add(paramName);
                            cmd.Parameters.AddWithValue(paramName, types[i]);
                        }

                        sqlBuilder.Append($"AND ConsumptionType IN ({string.Join(",", typeParams)}) ");
                    }

                    if (!string.IsNullOrEmpty(noteSearch))
                    {
                        sqlBuilder.Append("AND Note LIKE @note ");
                        cmd.Parameters.AddWithValue("@note", $"%{noteSearch}%");
                    }

                    sqlBuilder.Append("ORDER BY Date DESC "); // řazení vždy

                    cmd.CommandText = sqlBuilder.ToString();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new ConsumptionRecord
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Date = DateTime.Parse(reader["Date"].ToString()),
                                ConsumptionType = reader["ConsumptionType"].ToString(),
                                Amount = Convert.ToDouble(reader["Amount"]),
                                Note = reader["Note"].ToString()
                            });
                        }
                    }
                }
            }
            return records;
        }

        public static void AddRecord(string type, double amount, string date, string note)
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "INSERT INTO ConsumptionRecords (ConsumptionType, Amount, Date, Note) VALUES (@type, @amount, @date, @note)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@note", note);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Dictionary<string, double> GetConsumptionSummaryByType()
        {
            var result = new Dictionary<string, double>();

            using (var conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                string sql = @"SELECT ConsumptionType, SUM(Amount) as Total 
                       FROM ConsumptionRecords 
                       GROUP BY ConsumptionType";

                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string type = reader.GetString(0);
                        double total = reader.GetDouble(1);
                        result[type] = total;
                    }
                }
            }

            return result;
        }
        public static void SeedRandomData(int count = 30)
        {
            var rnd = new Random();
            string[] types = { "Elektřina", "Voda", "Plyn", "Internet", "Jiné" };

            var notesByType = new Dictionary<string, string[]>
            {
                { "Elektřina", new[] { "Faktura za elektřinu", "Vyúčtování ČEZ", "Záloha na elektřinu", "Spotřeba za měsíc" } },
                { "Voda", new[] { "Měsíční účet za vodu", "Vodné a stočné", "Faktura z vodáren", "Spotřeba vody" } },
                { "Plyn", new[] { "Záloha za plyn", "Účet za plyn", "Faktura Innogy", "Topná sezóna" } },
                { "Internet", new[] { "Platba za internet", "Měsíční tarif", "UPC faktura", "Záloha Wi-Fi" } },
                { "Jiné", new[] { "Správa budovy", "Pojištění domácnosti", "Ostatní výdaje", "Údržba bytu" } }
            };

            using (var conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                for (int i = 0; i < count; i++)
                {
                    string type = types[rnd.Next(types.Length)];
                    double amount = Math.Round(rnd.NextDouble() * 1000 + 100, 2);
                    DateTime date = DateTime.Today.AddDays(-rnd.Next(0, 365));
                    string note = notesByType[type][rnd.Next(notesByType[type].Length)];

                    string sql = "INSERT INTO ConsumptionRecords (ConsumptionType, Amount, Date, Note) VALUES (@type, @amount, @date, @note)";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@note", note);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void DeleteAllRecords()
        {
            using (var conn = new SQLiteConnection(GetConnectionString()))
            {
                conn.Open();
                string sql = "DELETE FROM ConsumptionRecords";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static FilteredRecordsSummary GetFilteredRecordsWithStatistics(DateTime? from, DateTime? to, string type, string noteSearch)
        {
            var result = new FilteredRecordsSummary();

            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                var sqlBuilder = new StringBuilder("SELECT * FROM ConsumptionRecords WHERE 1=1 ");
                using (var cmd = new SQLiteCommand(conn))
                {
                    if (from.HasValue)
                    {
                        sqlBuilder.Append("AND Date >= @from ");
                        cmd.Parameters.AddWithValue("@from", from.Value.ToString("yyyy-MM-dd"));
                    }
                    if (to.HasValue)
                    {
                        sqlBuilder.Append("AND Date <= @to ");
                        cmd.Parameters.AddWithValue("@to", to.Value.ToString("yyyy-MM-dd"));
                    }
                    if (!string.IsNullOrEmpty(type) && type != "Všechny")
                    {
                        sqlBuilder.Append("AND ConsumptionType = @type ");
                        cmd.Parameters.AddWithValue("@type", type);
                    }
                    if (!string.IsNullOrEmpty(noteSearch))
                    {
                        sqlBuilder.Append("AND Note LIKE @note ");
                        cmd.Parameters.AddWithValue("@note", $"%{noteSearch}%");
                    }

                    sqlBuilder.Append("ORDER BY Date DESC ");

                    cmd.CommandText = sqlBuilder.ToString();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var record = new ConsumptionRecord
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ConsumptionType = reader["ConsumptionType"].ToString(),
                                Amount = Convert.ToDouble(reader["Amount"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Note = reader["Note"].ToString()
                            };

                            result.Records.Add(record);

                            if (!result.SumByType.ContainsKey(record.ConsumptionType))
                                result.SumByType[record.ConsumptionType] = 0;
                            result.SumByType[record.ConsumptionType] += record.Amount;
                        }
                    }

                    result.TotalCount = result.Records.Count;
                }
            }

            return result;
        }
    }
}
