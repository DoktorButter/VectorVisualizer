using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace VectorVisualizer.GUI
{
    public partial class VektorPlotForm3D : Form
    {
        public VektorPlotForm3D(
            double[][,,] vectorField,             // [0]=Fx, [1]=Fy, [2]=Fz
            double xMin, double xMax,
            double yMin, double yMax,
            double zMin, double zMax,
            double[,,]? divergence = null,
            double[][,,]? curl = null,            // [0]=CurlX, [1]=CurlY, [2]=CurlZ
            double? px = null, double? py = null, double? pz = null)
        {
            InitializeComponent();

            this.Text = "3D-Vektorfeld Visualisierung";
            this.Size = new Size(900, 700);
            this.BackColor = Color.FromArgb(33, 33, 33);
            
            // 3D-Host
            var host = new ElementHost { Dock = DockStyle.Fill };
            var viewPort = new HelixViewport3D
            {
                Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(25, 25, 25)), // fast schwarz
                ZoomExtentsWhenLoaded = true
            };
            host.Child = viewPort;
            // === Orientierungsgitter hinzufügen ===
            var fillColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(160, 160, 160));
            // XY-Ebene (Boden)
            var gridXY = new GridLinesVisual3D
            {
                Width = 10,
                Length = 10,
                MinorDistance = 1,
                Thickness = 0.04,
                Center = new Point3D(0, 0, 0),
                Normal = new Vector3D(0, 0, 1),
                Fill = fillColor
            };
            viewPort.Children.Add(gridXY);

            // XZ-Ebene (Rückwand)
            var gridXZ = new GridLinesVisual3D
            {
                Width = 10,
                Length = 10,
                MinorDistance = 1,
                Thickness = 0.04,
                Center = new Point3D(0, 0, 0),
                Normal = new Vector3D(0, 1, 0),
                Fill = fillColor
            };
            viewPort.Children.Add(gridXZ);

            // YZ-Ebene (Seitenwand)
            var gridYZ = new GridLinesVisual3D
            {
                Width = 10,
                Length = 10,
                MinorDistance = 1,
                Thickness = 0.04,
                Center = new Point3D(0, 0, 0),
                Normal = new Vector3D(1, 0, 0),
                Fill = fillColor
            };
            viewPort.Children.Add(gridYZ);
            // X-Achse-Beschriftung
            var labelX = new BillboardTextVisual3D
            {
                Text = "x",
                Position = new Point3D(5.5, 0, 0),
                Foreground = System.Windows.Media.Brushes.White,
                Background = System.Windows.Media.Brushes.Transparent
            };
            viewPort.Children.Add(labelX);

            // Y-Achse-Beschriftung
            var labelY = new BillboardTextVisual3D
            {
                Text = "y",
                Position = new Point3D(0, 5.5, 0),
                Foreground = System.Windows.Media.Brushes.White,
                Background = System.Windows.Media.Brushes.Transparent
            };
            viewPort.Children.Add(labelY);

            // Z-Achse-Beschriftung
            var labelZ = new BillboardTextVisual3D
            {
                Text = "z",
                Position = new Point3D(0, 0, 5.5),
                Foreground = System.Windows.Media.Brushes.White,
                Background = System.Windows.Media.Brushes.Transparent
            };
            viewPort.Children.Add(labelZ);

            viewPort.Children.Add(new DefaultLights());

            int sizeX = vectorField[0].GetLength(0);
            int sizeY = vectorField[0].GetLength(1);
            int sizeZ = vectorField[0].GetLength(2);

            double stepX = (xMax - xMin) / sizeX;
            double stepY = (yMax - yMin) / sizeY;
            double stepZ = (zMax - zMin) / sizeZ;

            // Pfeile zeichnen
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    for (int k = 0; k < sizeZ; k++)
                    {
                        double x = xMin + i * stepX;
                        double y = yMin + j * stepY;
                        double z = zMin + k * stepZ;

                        double fx = vectorField[0][i, j, k];
                        double fy = vectorField[1][i, j, k];
                        double fz = vectorField[2][i, j, k];

                        if (double.IsFinite(fx) && double.IsFinite(fy) && double.IsFinite(fz))
                        {
                            var arrow = new ArrowVisual3D
                            {
                                Point1 = new Point3D(x, y, z),
                                Point2 = new Point3D(x + fx * 0.2, y + fy * 0.2, z + fz * 0.2),
                                Diameter = 0.05,
                                Fill = System.Windows.Media.Brushes.White
                            };
                            viewPort.Children.Add(arrow);
                        }
                    }
                }
            }

            // Info-Panel links
            Panel? leftPanel = null;

            if ((divergence != null || curl != null) && px.HasValue && py.HasValue && pz.HasValue)
            {
                string infoText = "";

                int i = (int)((px.Value - xMin) / (xMax - xMin) * sizeX);
                int j = (int)((py.Value - yMin) / (yMax - yMin) * sizeY);
                int k = (int)((pz.Value - zMin) / (zMax - zMin) * sizeZ);

                i = Math.Clamp(i, 0, sizeX - 1);
                j = Math.Clamp(j, 0, sizeY - 1);
                k = Math.Clamp(k, 0, sizeZ - 1);

                if (divergence != null)
                {
                    double divVal = divergence[i, j, k];
                    infoText += $"Divergenz:\n∇·F(x, y, z) = {Math.Round(divVal, 4)}\n\n";
                }

                if (curl != null)
                {
                    double cx = curl[0][i, j, k];
                    double cy = curl[1][i, j, k];
                    double cz = curl[2][i, j, k];
                    infoText += $"Rotation:\n∇×F = ({Math.Round(cx, 4)}, {Math.Round(cy, 4)}, {Math.Round(cz, 4)})";
                }

                var label = new Label
                {
                    Text = infoText,
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(10, 20),
                    BackColor = Color.FromArgb(33, 33, 33)
                };

                leftPanel = new Panel
                {
                    Width = 220,
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(33, 33, 33)
                };
                leftPanel.Controls.Add(label);
            }


            if ((divergence != null || curl != null) && px.HasValue && py.HasValue && pz.HasValue)
            {
                string infoText = "";

                int i = (int)((px.Value - xMin) / (xMax - xMin) * sizeX);
                int j = (int)((py.Value - yMin) / (yMax - yMin) * sizeY);
                int k = (int)((pz.Value - zMin) / (zMax - zMin) * sizeZ);

                i = Math.Clamp(i, 0, sizeX - 1);
                j = Math.Clamp(j, 0, sizeY - 1);
                k = Math.Clamp(k, 0, sizeZ - 1);

                if (divergence != null)
                {
                    double divVal = divergence[i, j, k];
                    infoText += $"Divergenz:\n∇·F(x, y, z) = {Math.Round(divVal, 4)}\n\n";
                }

                if (curl != null)
                {
                    double cx = curl[0][i, j, k];
                    double cy = curl[1][i, j, k];
                    double cz = curl[2][i, j, k];
                    infoText += $"Rotation:\n∇×F = ({Math.Round(cx, 4)}, {Math.Round(cy, 4)}, {Math.Round(cz, 4)})";
                }

                var label = new Label
                {
                    Text = infoText,
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(10, 20),
                    BackColor = Color.FromArgb(33, 33, 33)
                };
                leftPanel.Controls.Add(label);
            }

            // Layout
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };

            if (leftPanel != null)
            {
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                layout.Controls.Add(leftPanel, 0, 0);
                layout.Controls.Add(host, 1, 0);
            }
            else
            {
                layout.ColumnCount = 1;
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                layout.Controls.Add(host, 0, 0);
            }

            this.Controls.Add(layout);

        }
    }
}
