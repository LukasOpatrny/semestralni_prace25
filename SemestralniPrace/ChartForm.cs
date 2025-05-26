using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SemestralniPrace
{
    public partial class ChartForm : Form
    {
        private List<ConsumptionRecord> allRecords;

        public ChartForm(List<ConsumptionRecord> records)
        {
            InitializeComponent();

            this.Text = "Přehled spotřeby";
            this.BackColor = Color.White;

            allRecords = records ?? new List<ConsumptionRecord>();

            toolStripMenuItemWeek.Click += (s, e) => ApplyFilter(TimeSpan.FromDays(7));
            toolStripMenuItemMonth.Click += (s, e) => ApplyFilter(TimeSpan.FromDays(30));
            toolStripMenuItemYear.Click += (s, e) => ApplyFilter(TimeSpan.FromDays(365));

            ApplyFilter(TimeSpan.FromDays(30));
        }

        private void ApplyFilter(TimeSpan period)
        {
            DateTime fromDate = DateTime.Now - period;

            var filtered = allRecords
                .Where(r => r.Date >= fromDate)
                .ToList();

            UpdateOverviewChart(filtered);
            UpdateTimeSeriesChart(filtered);
            UpdatePieChart(filtered);
        }

        private void UpdateOverviewChart(List<ConsumptionRecord> records)
        {
            var grouped = records
                .GroupBy(r => r.ConsumptionType)
                .Select(g => new
                {
                    Type = g.Key,
                    Total = g.Sum(r => r.Amount)
                }).ToList();

            cartesianChartOverview.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Spotřeba (Kč)",
                    Values = new ChartValues<double>(grouped.Select(g => g.Total)),
                    Fill = System.Windows.Media.Brushes.SteelBlue
                }
            };

            cartesianChartOverview.AxisX.Clear();
            cartesianChartOverview.AxisX.Add(new Axis
            {
                Title = "Typ spotřeby",
                Labels = grouped.Select(g => g.Type).ToList()
            });

            cartesianChartOverview.AxisY.Clear();
            cartesianChartOverview.AxisY.Add(new Axis
            {
                Title = "Celkem (Kč)",
                LabelFormatter = value => $"{value:N0} Kč"
            });

            labelOverviewInfo.Text = $"Počet záznamů: {records.Count}\nCelkem: {grouped.Sum(g => g.Total):N0} Kč\n\nČástky podle typu:\n";

            foreach (var item in grouped)
            {
                labelOverviewInfo.Text += $"{item.Type}: {item.Total:N0} Kč\n";
            }
        }

        private void UpdateTimeSeriesChart(List<ConsumptionRecord> records)
        {
            var grouped = records
                .GroupBy(r => r.Date.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key,
                    Total = g.Sum(r => r.Amount)
                }).ToList();

            cartesianChartTimeSeries.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Denní spotřeba",
                    Values = new ChartValues<double>(grouped.Select(g => g.Total)),
                    StrokeThickness = 2,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 6
                }
            };

            cartesianChartTimeSeries.AxisX.Clear();
            cartesianChartTimeSeries.AxisX.Add(new Axis
            {
                Title = "Datum",
                Labels = grouped.Select(g => g.Date.ToString("dd.MM")).ToList()
            });

            cartesianChartTimeSeries.AxisY.Clear();
            cartesianChartTimeSeries.AxisY.Add(new Axis
            {
                Title = "Spotřeba (Kč)",
                LabelFormatter = value => $"{value:N0} Kč"
            });

            labelTimeSeriesInfo.Text = $"Počet záznamů: {records.Count}\nCelkem: {grouped.Sum(g => g.Total):N0} Kč";
        }

        private void UpdatePieChart(List<ConsumptionRecord> records)
        {
            var grouped = records
                .GroupBy(r => r.ConsumptionType)
                .Select(g => new
                {
                    Type = g.Key,
                    Total = g.Sum(r => r.Amount)
                }).ToList();

            pieChartShare.Series = new SeriesCollection();

            foreach (var item in grouped)
            {
                pieChartShare.Series.Add(new PieSeries
                {
                    Title = item.Type,
                    Values = new ChartValues<double> { item.Total },
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y:N0} Kč"
                });
            }

            pieChartShare.LegendLocation = LegendLocation.Right;

            labelPieChartInfo.Text = $"Počet záznamů: {records.Count}\nCelkem: {grouped.Sum(g => g.Total):N0} Kč\n\nPodíl podle typu:\n";

            foreach (var item in grouped)
            {
                labelPieChartInfo.Text += $"{item.Type}: {item.Total:N0} Kč\n";
            }
        }


    }
}
