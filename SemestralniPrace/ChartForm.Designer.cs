namespace SemestralniPrace
{
    partial class ChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Přehled = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nadpis = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cartesianChartOverview = new LiveCharts.WinForms.CartesianChart();
            this.Vývoj_spotřeby = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cartesianChartTimeSeries = new LiveCharts.WinForms.CartesianChart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pieChartShare = new LiveCharts.WinForms.PieChart();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.menuStripFilters = new System.Windows.Forms.MenuStrip();
            this.filtrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWeek = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemYear = new System.Windows.Forms.ToolStripMenuItem();
            this.labelOverviewInfo = new System.Windows.Forms.Label();
            this.labelTimeSeriesInfo = new System.Windows.Forms.Label();
            this.labelPieChartInfo = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.Přehled.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Vývoj_spotřeby.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.menuStripFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.Přehled);
            this.tabControl1.Controls.Add(this.Vývoj_spotřeby);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1451, 918);
            this.tabControl1.TabIndex = 0;
            // 
            // Přehled
            // 
            this.Přehled.Controls.Add(this.panel2);
            this.Přehled.Controls.Add(this.panel1);
            this.Přehled.Controls.Add(this.cartesianChartOverview);
            this.Přehled.Location = new System.Drawing.Point(4, 29);
            this.Přehled.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Přehled.Name = "Přehled";
            this.Přehled.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Přehled.Size = new System.Drawing.Size(1443, 885);
            this.Přehled.TabIndex = 0;
            this.Přehled.Text = "Přehled";
            this.Přehled.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nadpis);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1435, 100);
            this.panel2.TabIndex = 13;
            // 
            // nadpis
            // 
            this.nadpis.AutoSize = true;
            this.nadpis.BackColor = System.Drawing.Color.Transparent;
            this.nadpis.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nadpis.Location = new System.Drawing.Point(507, 15);
            this.nadpis.Name = "nadpis";
            this.nadpis.Size = new System.Drawing.Size(414, 65);
            this.nadpis.TabIndex = 12;
            this.nadpis.Text = "Přehled spotřeby";
            this.nadpis.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.labelOverviewInfo);
            this.panel1.Location = new System.Drawing.Point(1101, 102);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 760);
            this.panel1.TabIndex = 1;
            // 
            // cartesianChartOverview
            // 
            this.cartesianChartOverview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cartesianChartOverview.Location = new System.Drawing.Point(0, 102);
            this.cartesianChartOverview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cartesianChartOverview.Name = "cartesianChartOverview";
            this.cartesianChartOverview.Size = new System.Drawing.Size(1097, 783);
            this.cartesianChartOverview.TabIndex = 0;
            this.cartesianChartOverview.Text = "cartesianChart1";
            // 
            // Vývoj_spotřeby
            // 
            this.Vývoj_spotřeby.Controls.Add(this.panel3);
            this.Vývoj_spotřeby.Controls.Add(this.panel4);
            this.Vývoj_spotřeby.Controls.Add(this.cartesianChartTimeSeries);
            this.Vývoj_spotřeby.Location = new System.Drawing.Point(4, 29);
            this.Vývoj_spotřeby.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Vývoj_spotřeby.Name = "Vývoj_spotřeby";
            this.Vývoj_spotřeby.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Vývoj_spotřeby.Size = new System.Drawing.Size(1443, 885);
            this.Vývoj_spotřeby.TabIndex = 1;
            this.Vývoj_spotřeby.Text = "Vývoj spotřeby";
            this.Vývoj_spotřeby.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1435, 100);
            this.panel3.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(489, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 65);
            this.label1.TabIndex = 12;
            this.label1.Text = "Vývoj spotřeby";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.labelTimeSeriesInfo);
            this.panel4.Location = new System.Drawing.Point(1103, 99);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(335, 760);
            this.panel4.TabIndex = 15;
            // 
            // cartesianChartTimeSeries
            // 
            this.cartesianChartTimeSeries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cartesianChartTimeSeries.Location = new System.Drawing.Point(2, 99);
            this.cartesianChartTimeSeries.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cartesianChartTimeSeries.Name = "cartesianChartTimeSeries";
            this.cartesianChartTimeSeries.Size = new System.Drawing.Size(1097, 783);
            this.cartesianChartTimeSeries.TabIndex = 14;
            this.cartesianChartTimeSeries.Text = "cartesianChart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pieChartShare);
            this.tabPage3.Controls.Add(this.panel5);
            this.tabPage3.Controls.Add(this.panel6);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1443, 885);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Rozložení";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pieChartShare
            // 
            this.pieChartShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pieChartShare.Location = new System.Drawing.Point(-4, 102);
            this.pieChartShare.Name = "pieChartShare";
            this.pieChartShare.Size = new System.Drawing.Size(1098, 760);
            this.pieChartShare.TabIndex = 17;
            this.pieChartShare.Text = "pieChart1";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1443, 100);
            this.panel5.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(487, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(458, 65);
            this.label4.TabIndex = 12;
            this.label4.Text = "Rozložení spotřeby";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.labelPieChartInfo);
            this.panel6.Location = new System.Drawing.Point(1101, 102);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(335, 760);
            this.panel6.TabIndex = 15;
            // 
            // menuStripFilters
            // 
            this.menuStripFilters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtrToolStripMenuItem});
            this.menuStripFilters.Location = new System.Drawing.Point(0, 0);
            this.menuStripFilters.Name = "menuStripFilters";
            this.menuStripFilters.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStripFilters.Size = new System.Drawing.Size(1451, 25);
            this.menuStripFilters.TabIndex = 1;
            this.menuStripFilters.Text = "menuStrip1";
            // 
            // filtrToolStripMenuItem
            // 
            this.filtrToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemWeek,
            this.toolStripMenuItemMonth,
            this.toolStripMenuItemYear});
            this.filtrToolStripMenuItem.Name = "filtrToolStripMenuItem";
            this.filtrToolStripMenuItem.Size = new System.Drawing.Size(39, 19);
            this.filtrToolStripMenuItem.Text = "Filtr";
            // 
            // toolStripMenuItemWeek
            // 
            this.toolStripMenuItemWeek.Name = "toolStripMenuItemWeek";
            this.toolStripMenuItemWeek.Size = new System.Drawing.Size(153, 22);
            this.toolStripMenuItemWeek.Text = "Poslední týden";
            // 
            // toolStripMenuItemMonth
            // 
            this.toolStripMenuItemMonth.Name = "toolStripMenuItemMonth";
            this.toolStripMenuItemMonth.Size = new System.Drawing.Size(153, 22);
            this.toolStripMenuItemMonth.Text = "Poslední měsíc";
            // 
            // toolStripMenuItemYear
            // 
            this.toolStripMenuItemYear.Name = "toolStripMenuItemYear";
            this.toolStripMenuItemYear.Size = new System.Drawing.Size(153, 22);
            this.toolStripMenuItemYear.Text = "Poslední rok";
            // 
            // labelOverviewInfo
            // 
            this.labelOverviewInfo.AutoSize = true;
            this.labelOverviewInfo.Location = new System.Drawing.Point(60, 48);
            this.labelOverviewInfo.Name = "labelOverviewInfo";
            this.labelOverviewInfo.Size = new System.Drawing.Size(129, 20);
            this.labelOverviewInfo.TabIndex = 17;
            this.labelOverviewInfo.Text = "labelOverviewInfo";
            // 
            // labelTimeSeriesInfo
            // 
            this.labelTimeSeriesInfo.AutoSize = true;
            this.labelTimeSeriesInfo.Location = new System.Drawing.Point(59, 47);
            this.labelTimeSeriesInfo.Name = "labelTimeSeriesInfo";
            this.labelTimeSeriesInfo.Size = new System.Drawing.Size(140, 20);
            this.labelTimeSeriesInfo.TabIndex = 17;
            this.labelTimeSeriesInfo.Text = "labelTimeSeriesInfo";
            // 
            // labelPieChartInfo
            // 
            this.labelPieChartInfo.AutoSize = true;
            this.labelPieChartInfo.Location = new System.Drawing.Point(91, 76);
            this.labelPieChartInfo.Name = "labelPieChartInfo";
            this.labelPieChartInfo.Size = new System.Drawing.Size(123, 20);
            this.labelPieChartInfo.TabIndex = 19;
            this.labelPieChartInfo.Text = "labelPieChartInfo";
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1451, 943);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStripFilters);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ChartForm";
            this.Text = "ChartForm";
            this.tabControl1.ResumeLayout(false);
            this.Přehled.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Vývoj_spotřeby.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.menuStripFilters.ResumeLayout(false);
            this.menuStripFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Přehled;
        private System.Windows.Forms.TabPage Vývoj_spotřeby;
        private LiveCharts.WinForms.CartesianChart cartesianChartOverview;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.MenuStrip menuStripFilters;
        private System.Windows.Forms.ToolStripMenuItem filtrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemWeek;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMonth;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemYear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label nadpis;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private LiveCharts.WinForms.CartesianChart cartesianChartTimeSeries;
        private LiveCharts.WinForms.PieChart pieChartShare;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label labelOverviewInfo;
        private System.Windows.Forms.Label labelTimeSeriesInfo;
        private System.Windows.Forms.Label labelPieChartInfo;
    }
}