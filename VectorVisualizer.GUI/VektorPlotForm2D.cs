using System;
using System.Drawing;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WinForms;

namespace VectorVisualizer.GUI
{
    public partial class VektorPlotForm2D : Form
    {
        public VektorPlotForm2D(
            double[][,] vectorField, // [0]=Fx, [1]=Fy
            double xMin, double xMax,
            double yMin, double yMax,
            double[,]? divergence = null,
            double[,]? rotation = null,
            double? px = null,
            double? py = null)
        {
            InitializeComponent();

            // Fenster Setup
            this.Text = "Vektorfeld Visualisierung";
            this.Size = new Size(900, 700);
            this.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);

            // ScottPlot vorbereiten
            var formsPlot = new FormsPlot();
            formsPlot.Dock = DockStyle.Fill;
            var plt = formsPlot.Plot;

            int sizeX = vectorField[0].GetLength(0);
            int sizeY = vectorField[0].GetLength(1);
            double stepX = (xMax - xMin) / sizeX;
            double stepY = (yMax - yMin) / sizeY;

            // Vektorfeld zeichnen
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    double x = xMin + i * stepX;
                    double y = yMin + j * stepY;
                    double fx = vectorField[0][i, j];
                    double fy = vectorField[1][i, j];

                    if (double.IsFinite(fx) && double.IsFinite(fy))
                    {
                        var arrow = plt.Add.Arrow(x, y, x + fx * 0.2, y + fy * 0.2);

                    }
                }
            }

            // Info-Panel vorbereiten (falls gewünscht)
            Panel? leftPanel = null;
            if ((divergence != null || rotation != null) && px.HasValue && py.HasValue)
            {
                leftPanel = new Panel();
                leftPanel.Dock = DockStyle.Fill;
                leftPanel.Width = 200;
                leftPanel.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);

                string infoText = "";

                if (divergence != null)
                {
                    int i = (int)((px.Value - xMin) / (xMax - xMin) * divergence.GetLength(0));
                    int j = (int)((py.Value - yMin) / (yMax - yMin) * divergence.GetLength(1));
                    i = Math.Clamp(i, 0, divergence.GetLength(0) - 1);
                    j = Math.Clamp(j, 0, divergence.GetLength(1) - 1);
                    double divVal = divergence[i, j];
                    infoText += $"Divergenz: ∇·F(x, y): \n {Math.Round(divVal, 4)}\n\n";
                }

                if (rotation != null)
                {
                    int i = (int)((px.Value - xMin) / (xMax - xMin) * rotation.GetLength(0));
                    int j = (int)((py.Value - yMin) / (yMax - yMin) * rotation.GetLength(1));
                    i = Math.Clamp(i, 0, rotation.GetLength(0) - 1);
                    j = Math.Clamp(j, 0, rotation.GetLength(1) - 1);
                    double rotVal = rotation[i, j];
                    infoText += $"Rotation: ∇×F(x, y): \n {Math.Round(rotVal, 4)}";
                }

                var infoLabel = new System.Windows.Forms.Label();
                infoLabel.Text = infoText;
                infoLabel.ForeColor = System.Drawing.Color.White;
                infoLabel.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
                infoLabel.AutoSize = true;
                infoLabel.Location = new Point(10, 20);
                leftPanel.Controls.Add(infoLabel);
            }

            // Plot-Design
            plt.FigureBackground.Color = Colors.Black;
            plt.DataBackground.Color = new ScottPlot.Color(33, 33, 33);
            plt.Axes.Color(Colors.White);
            plt.Axes.Left.Label.Text = "y";
            plt.Axes.Bottom.Label.Text = "x";

            // Layout
            var layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 2;
            layout.RowCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            if (leftPanel != null)
                layout.Controls.Add(leftPanel, 0, 0);
            else
                layout.ColumnStyles[0].Width = 0;

            layout.Controls.Add(formsPlot, 1, 0);
            this.Controls.Add(layout);


            formsPlot.Refresh();
        }
    }
}
