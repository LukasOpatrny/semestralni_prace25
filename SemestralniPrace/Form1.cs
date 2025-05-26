using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Forms;

namespace SemestralniPrace
{
    public partial class Form1 : Form
    {
        private int selectedId = -1;
        private List<ConsumptionRecord> allRecords = DatabaseHelper.GetAllRecords();

        public Form1()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
            LoadRecords();
            //DatabaseHelper.SeedRandomData(100);
            //DatabaseHelper.DeleteAllRecords();
            Form1_Load(null, EventArgs.Empty);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxType.Items.Clear();
            comboBoxType.Items.AddRange(new string[] { "Elektřina", "Voda", "Plyn", "Internet", "Jiné" });
            comboBoxType.SelectedIndex = 0;

            comboBoxFilterType.Items.Clear();
            comboBoxFilterType.Items.Add("Všechny");
            comboBoxFilterType.Items.AddRange(new string[] { "Elektřina", "Voda", "Plyn", "Internet", "Jiné" });
            comboBoxFilterType.SelectedIndex = 0;

            comboBoxDatePreset.Items.Clear();
            comboBoxDatePreset.Items.AddRange(new string[] { "Kdykoli", "Dnes", "Posledních 7 dní", "Tento měsíc", "Tento rok", "Vybraný datum" });
            comboBoxDatePreset.SelectedIndex = 0;

            comboBoxDatePreset.SelectedIndexChanged += comboBoxDatePreset_SelectedIndexChanged;
            dataGridViewRecords.CellDoubleClick += dataGridViewRecords_CellDoubleClick;
            ApplyFilterAndShowSummary();

            var allRecords = DatabaseHelper.GetAllRecords();
            var summary = allRecords
                .GroupBy(r => r.ConsumptionType)
                .Select(g => new { Type = g.Key, TotalAmount = g.Sum(x => x.Amount) })
                .ToList();

            var types = summary.Select(x => x.Type).ToArray();
            var values = summary.Select(x => x.TotalAmount).ToArray();

            cartesianChartConsumption.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Spotřeba",
                    Values = new ChartValues<double>(values)
                }
            };

            cartesianChartConsumption.AxisX.Clear();
            cartesianChartConsumption.AxisX.Add(new Axis
            {
                Title = "Typ spotřeby",
                Labels = types.ToList()
            });

            cartesianChartConsumption.AxisY.Clear();
            cartesianChartConsumption.AxisY.Add(new Axis
            {
                Title = "Celková spotřeba",
                LabelFormatter = value => value.ToString("N2")
            });

        }

        private void LoadRecords()
        {
            var records = DatabaseHelper.GetAllRecords();
            dataGridViewRecords.DataSource = null;
            dataGridViewRecords.DataSource = records;

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dataGridViewRecords.EnableHeadersVisualStyles = false;
            dataGridViewRecords.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridViewRecords.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewRecords.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewRecords.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewRecords.DefaultCellStyle.BackColor = Color.White;
            dataGridViewRecords.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewRecords.GridColor = Color.LightGray;
            dataGridViewRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridViewRecords.Columns["Id"] != null)
            {
                dataGridViewRecords.Columns["Id"].Visible = false;

                int lastIndex = dataGridViewRecords.Columns.Count - 1;
                dataGridViewRecords.Columns["Id"].DisplayIndex = 0;
            }

            if (dataGridViewRecords.Columns["Date"] != null)
            {
                dataGridViewRecords.Columns["Date"].HeaderText = "Datum";
                dataGridViewRecords.Columns["Date"].DisplayIndex = 1;
            }

            if (dataGridViewRecords.Columns["ConsumptionType"] != null)
            {
                dataGridViewRecords.Columns["ConsumptionType"].HeaderText = "Typ spotřeby";
                dataGridViewRecords.Columns["ConsumptionType"].DisplayIndex = 2;
            }

            if (dataGridViewRecords.Columns["Note"] != null)
            {
                dataGridViewRecords.Columns["Note"].HeaderText = "Poznámka";
                dataGridViewRecords.Columns["Note"].DisplayIndex = 3;
            }

            if (dataGridViewRecords.Columns["Amount"] != null)
            {
                dataGridViewRecords.Columns["Amount"].HeaderText = "Částka (Kč)";
                dataGridViewRecords.Columns["Amount"].DisplayIndex = 4;
            }
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            try
            {
                string type = comboBoxType.Text;

                if (string.IsNullOrWhiteSpace(textBoxAmount.Text))
                {
                    MessageBox.Show("Musíte zadat částku.");
                    return;
                }

                double amount = double.Parse(textBoxAmount.Text);
                string date = dateTimePickerDate.Value.ToString("yyyy-MM-dd");
                string note = textBoxNote.Text;

                DatabaseHelper.AddRecord(type, amount, date, note);

                dataGridViewRecords.DataSource = null;
                dataGridViewRecords.DataSource = DatabaseHelper.GetAllRecords();

                textBoxAmount.Clear();
                textBoxNote.Clear();
                comboBoxType.SelectedIndex = 0;

                ApplyFilterAndShowSummary();
                LoadRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při přidávání záznamu: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewRecords.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Opravdu chceš tento záznam smazat?", "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewRecords.SelectedRows[0].Cells["Id"].Value);

                    using (var conn = new SQLiteConnection(DatabaseHelper.GetConnectionString()))
                    {
                        conn.Open();
                        string sql = "DELETE FROM ConsumptionRecords WHERE Id = @id";
                        using (var cmd = new SQLiteCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadRecords();
                }
            }
            else
            {
                MessageBox.Show("Vyber prosím záznam ke smazání.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridViewRecords_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewRecords.Rows[e.RowIndex];

                comboBoxType.Text = row.Cells["ConsumptionType"].Value.ToString();
                textBoxAmount.Text = row.Cells["Amount"].Value.ToString();
                dateTimePickerDate.Value = DateTime.Parse(row.Cells["Date"].Value.ToString());
                textBoxNote.Text = row.Cells["Note"].Value?.ToString();
                selectedId = Convert.ToInt32(row.Cells["Id"].Value);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Vyber prosím záznam k úpravě dvojklikem.");
                return;
            }

            try
            {
                string type = comboBoxType.Text;
                double amount = double.Parse(textBoxAmount.Text);
                string date = dateTimePickerDate.Value.ToString("yyyy-MM-dd");
                string note = textBoxNote.Text;

                using (var conn = new SQLiteConnection(DatabaseHelper.GetConnectionString()))
                {
                    conn.Open();
                    string sql = "UPDATE ConsumptionRecords SET ConsumptionType=@type, Amount=@amount, Date=@date, Note=@note WHERE Id=@id";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@note", note);
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Záznam byl úspěšně upraven.");
                LoadRecords();
                selectedId = -1;
                comboBoxType.SelectedIndex = 0;
                textBoxAmount.Clear();
                textBoxNote.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při úpravě záznamu: " + ex.Message);
            }
        }

        private void btnShowChart_Click(object sender, EventArgs e)
        {
            var records = DatabaseHelper.GetAllRecords();

            if (records.Count == 0)
            {
                MessageBox.Show("Nejsou k dispozici žádná data pro zobrazení grafu.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var chartForm = new ChartForm(records);
            chartForm.Show();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? from = null;
            DateTime? to = null;

            if (comboBoxDatePreset.SelectedItem != null)
            {
                string preset = comboBoxDatePreset.SelectedItem.ToString();
                DateTime today = DateTime.Today;

                switch (preset)
                {
                    case "Kdykoli":
                        break;

                    case "Dnes":
                        from = today;
                        to = today;
                        break;

                    case "Posledních 7 dní":
                        from = today.AddDays(-6);
                        to = today;
                        break;

                    case "Tento měsíc":
                        from = new DateTime(today.Year, today.Month, 1);
                        to = today;
                        break;

                    case "Tento rok":
                        from = new DateTime(today.Year, 1, 1);
                        to = today;
                        break;

                    case "Vybraný datum":
                        if (dateTimePickerFrom.Checked)
                            from = dateTimePickerFrom.Value.Date;
                        if (dateTimePickerTo.Checked)
                            to = dateTimePickerTo.Value.Date;
                        break;
                }
            }

            string selectedType = comboBoxFilterType.SelectedItem?.ToString() ?? "Všechny";
            string noteSearchText = textBoxNote.Text;
            var summary = DatabaseHelper.GetFilteredRecordsWithStatistics(from, to, selectedType, noteSearchText);

            dataGridViewRecords.DataSource = summary.Records;

            labelTotalCount.Text = $"Celkem záznamů: {summary.TotalCount}";

            StringBuilder sb = new StringBuilder();
            foreach (var pair in summary.SumByType)
            {
                sb.AppendLine($"{pair.Key}: {pair.Value} Kč");
            }

            labelSumByType.Text = sb.ToString();
        }

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            dateTimePickerFrom.Checked = false;
            dateTimePickerTo.Checked = false;

            comboBoxFilterType.Items.Clear();
            comboBoxFilterType.Items.Add("Všechny"); 
            comboBoxFilterType.Items.AddRange(new string[] { "Elektřina", "Voda", "Plyn", "Internet", "Jiné" });
            comboBoxFilterType.SelectedIndex = 0; 
            comboBoxDatePreset.SelectedIndex = 0;
            
            textBoxSearchNote.Clear();

            LoadRecords();
            ApplyFilterAndShowSummary();
        }

        private void comboBoxDatePreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDatePreset.SelectedItem != null)
            {
                string preset = comboBoxDatePreset.SelectedItem.ToString();

                if (preset == "Kdykoli")
                {
                   
                }
                else if (preset == "Vybraný datum")
                {
                    DateTime dateFrom = dateTimePickerFrom.Value.Date;
                    DateTime dateTo = dateTimePickerTo.Value.Date;
                }
                else
                {
                    DateTime dateFrom, dateTo;
                    DateTime today = DateTime.Today;

                    switch (preset)
                    {
                        case "Dnes":
                            dateFrom = today;
                            dateTo = today;
                            break;

                        case "Posledních 7 dní":
                            dateFrom = today.AddDays(-6);
                            dateTo = today;
                            break;

                        case "Tento měsíc":
                            dateFrom = new DateTime(today.Year, today.Month, 1);
                            dateTo = today;
                            break;

                        case "Tento rok":
                            dateFrom = new DateTime(today.Year, 1, 1);
                            dateTo = today;
                            break;

                        default:
                            return;
                    }
                }
            }

        }

        private void ApplyFilterAndShowSummary()
        {
            DateTime? from = null;
            DateTime? to = null;

            string preset = comboBoxDatePreset.SelectedItem?.ToString() ?? "Kdykoli";
            DateTime today = DateTime.Today;

            switch (preset)
            {
                case "Kdykoli":
                    break;

                case "Dnes":
                    from = today;
                    to = today;
                    break;

                case "Posledních 7 dní":
                    from = today.AddDays(-6);
                    to = today;
                    break;

                case "Tento měsíc":
                    from = new DateTime(today.Year, today.Month, 1);
                    to = today;
                    break;

                case "Tento rok":
                    from = new DateTime(today.Year, 1, 1);
                    to = today;
                    break;

                case "Vybraný datum":
                    if (dateTimePickerFrom.Checked)
                        from = dateTimePickerFrom.Value.Date;
                    if (dateTimePickerTo.Checked)
                        to = dateTimePickerTo.Value.Date;
                    break;
            }

            string selectedType = comboBoxFilterType.SelectedItem?.ToString() ?? "Všechny";
            string noteSearchText = textBoxNote.Text;

            var summary = DatabaseHelper.GetFilteredRecordsWithStatistics(from, to, selectedType, noteSearchText);

            dataGridViewRecords.DataSource = summary.Records;

            labelTotalCount.Text = $"Celkem záznamů: {summary.TotalCount}";

            StringBuilder sb = new StringBuilder();
            foreach (var pair in summary.SumByType)
            {
                sb.AppendLine($"{pair.Key}: {pair.Value} Kč");
            }

            labelSumByType.Text = sb.ToString();
        }

        private void toolStripMenuItemExportCsv_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.Title = "Export do CSV";
                sfd.FileName = "export.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var records = DatabaseHelper.GetAllRecords(); // nebo jak získáváš data

                        var csvLines = new List<string>();
                        csvLines.Add("Id;Date;ConsumptionType;Amount;Note");

                        csvLines.AddRange(records.Select(r =>
                            $"{r.Id};{r.Date:yyyy-MM-dd};{r.ConsumptionType};{r.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture)};{r.Note}"));

                        File.WriteAllLines(sfd.FileName, csvLines, Encoding.UTF8);

                        MessageBox.Show("Export do CSV proběhl úspěšně.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chyba při exportu: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void toolStripMenuItemImportCsv_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV files (*.csv)|*.csv";
                ofd.Title = "Import z CSV";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                        var importedRecords = new List<ConsumptionRecord>();

                        for (int i = 1; i < lines.Length; i++)
                        {
                            var parts = lines[i].Split(';');
                            if (parts.Length >= 5)
                            {
                                if (int.TryParse(parts[0], out int id) &&
                                    DateTime.TryParse(parts[1], out DateTime date) &&
                                    double.TryParse(parts[3], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double amount))
                                {
                                    var record = new ConsumptionRecord
                                    {
                                        Id = id,
                                        Date = date,
                                        ConsumptionType = parts[2],
                                        Amount = amount,
                                        Note = parts[4]
                                    };
                                    importedRecords.Add(record);
                                }
                            }
                        }

                        foreach (var rec in importedRecords)
                        {
                            DatabaseHelper.AddRecord(rec.ConsumptionType, rec.Amount, rec.Date.ToString("yyyy-MM-dd"), rec.Note);
                        }

                        dataGridViewRecords.DataSource = null;
                        dataGridViewRecords.DataSource = DatabaseHelper.GetAllRecords();

                        MessageBox.Show("Import z CSV proběhl úspěšně.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chyba při importu: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string helpText =
        @"Vítejte v aplikaci pro evidenci spotřeby v domácnosti!

        Tato aplikace slouží k přehlednému zaznamenávání a analýze výdajů v rámci domácnosti. Níže naleznete základní návod k použití:

        1. Pro přidání záznamu vyplňte datum, typ spotřeby, částku a poznámku, poté klikněte na tlačítko 'Přidat záznam'.
        2. Pro smazání záznamu vyberte požadovaný řádek v tabulce a klikněte na tlačítko 'Smazat záznam'.
        3. Pro úpravu záznamu dvakrát klikněte na daný řádek, proveďte změny a následně klikněte na 'Upravit záznam'.
        4. V hlavní tabulce se zobrazují všechny zaznamenané spotřeby.
        5. Pro filtrování záznamů vyberte požadované datumové rozmezí, typ spotřeby nebo klíčové slovo a klikněte na tlačítko 'Filtrovat'.
        6. Pro zrušení filtrů a obnovení celého seznamu klikněte na tlačítko 'Obnovit filtry'.
        7. V záložce 'Podrobnější grafy' naleznete více grafických přehledů pro lepší analýzu dat.
        8. V horní části okna s grafy najdete tlačítko 'Filtr', pomocí kterého můžete upravit zobrazené hodnoty podle zvolených kritérií.
        9. Pro export dat lze kliknout na tlačítko 'export do csv'.
        10. Pro import dat lze kliknout na tlačítko 'import z csv'.
        11. Nezapomeňte pravidelně ukládat a zálohovat svá data (doporučujeme přímé zálohování databáze).

        😊";

            MessageBox.Show(helpText, "Nápověda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
