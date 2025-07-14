using System;
using System.Drawing;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.Colormaps;

namespace VectorVisualizer.GUI
{
    public partial class PlotForm : Form
    {
        public PlotForm(
            double[,] data,
            double xMin, double xMax,
            double yMin, double yMax,
            string? gradientText = null,
            bool drawGradientVector = false,
            double? px = null,
            double? py = null)
        {
            InitializeComponent();

            // Fenster
            this.Text = "Skalarfeld Visualisierung";
            this.Size = new Size(800, 600);
            this.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);

            // Gradient-Panel vorbereiten (falls benötigt)
            Panel? leftPanel = null;
            if (!string.IsNullOrWhiteSpace(gradientText))
            {
                leftPanel = new Panel();
                leftPanel.Dock = DockStyle.Fill;
                leftPanel.Width = 200;
                leftPanel.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);

                var gradientLabel = new System.Windows.Forms.Label();
                gradientLabel.Text = gradientText;
                gradientLabel.ForeColor = System.Drawing.Color.White;
                gradientLabel.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
                gradientLabel.AutoSize = true;
                gradientLabel.Location = new Point(10, 20);
                leftPanel.Controls.Add(gradientLabel);
            }

            // Plot erstellen
            var formsPlot = new ScottPlot.WinForms.FormsPlot();
            formsPlot.Dock = DockStyle.Fill;

            var plt = formsPlot.Plot;
            var heatmap = plt.Add.Heatmap(data);
            plt.Title("f(x, y) als Heatmap");
            plt.FigureBackground.Color = Colors.Black;
            plt.DataBackground.Color = new ScottPlot.Color(33, 33, 33);
            plt.Axes.Color(Colors.White);
            plt.Axes.Left.Label.Text = "y";
            plt.Axes.Bottom.Label.Text = "x";

            // Farblegende
            heatmap.Colormap = new Turbo();
            var bar = plt.Add.ColorBar(heatmap);
            // Hauptlayout (Plot + Panel)
            var mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200)); // linker Rand
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); // Plot

            // Panel (nur falls vorhanden)
            if (leftPanel != null)
                mainLayout.Controls.Add(leftPanel, 0, 0);
            else
                mainLayout.ColumnStyles[0].Width = 0;

            mainLayout.Controls.Add(formsPlot, 1, 0);
            this.Controls.Add(mainLayout);

            formsPlot.Refresh();
        }
    }
}

