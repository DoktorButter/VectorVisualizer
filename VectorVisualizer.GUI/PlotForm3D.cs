using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using VectorVisualizer._3DView;

namespace VectorVisualizer.GUI
{
    public partial class PlotForm3D : Form
    {
        public PlotForm3D(
            Func<double, double, double, double> scalarField3D,
            double zFixed,
            int resolution = 80,
            string? gradientText = null)
        {
            InitializeComponent();

            Text = "3D-Skalarfeld (Ebene z=f)";
            Size = new System.Drawing.Size(800, 600);

            // Host für WPF-View
            var host = new ElementHost { Dock = DockStyle.Fill };
            View3D = new ScalarField3DView();
            host.Child = View3D;

            if (!string.IsNullOrWhiteSpace(gradientText))
            {
                // Linkes Panel mit Gradiententext
                var infoPanel = new Panel();
                infoPanel.Width = 200;
                infoPanel.Dock = DockStyle.Fill;
                infoPanel.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);

                var label = new System.Windows.Forms.Label();
                label.Text = gradientText;
                label.ForeColor = System.Drawing.Color.White;
                label.AutoSize = true;
                label.Location = new System.Drawing.Point(10, 20);
                infoPanel.Controls.Add(label);

                // Layout mit InfoPanel + 3D-Plot
                var layout = new TableLayoutPanel();
                layout.Dock = DockStyle.Fill;
                layout.ColumnCount = 2;
                layout.RowCount = 1;
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                layout.Controls.Add(infoPanel, 0, 0);
                layout.Controls.Add(host, 1, 0);
                this.Controls.Add(layout);
            }
            else
            {
                // Nur 3D-Plot, kein Panel
                this.Controls.Add(host);
            }

            View3D.PlotSurfaceLevel(
                scalarField3D,
                -5, 5, -5, 5,
                zFixed, resolution
            );
        }




        public ScalarField3DView View3D { get; private set; }
    }
}
