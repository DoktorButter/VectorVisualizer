using System;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace VectorVisualizer._3DView
{
    public partial class ScalarField3DView : System.Windows.Controls.UserControl
    {
        public ScalarField3DView()
        {
            InitializeComponent();
        }

        public void PlotSurfaceLevel(
            Func<double, double, double, double> scalarField3D,
            double xMin, double xMax,
            double yMin, double yMax,
            double zFixed, int resolution,
            double? px = null, double? py = null, double? pz = null,
            double[] gradient = null)
        {
            ViewPort.Children.Clear();
            ViewPort.Children.Add(new DefaultLights());

            double dx = (xMax - xMin) / resolution;
            double dy = (yMax - yMin) / resolution;
            var meshBuilder = new MeshBuilder(false, false);

            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    double x0 = xMin + i * dx;
                    double x1 = x0 + dx;
                    double y0 = yMin + j * dy;
                    double y1 = y0 + dy;

                    double z00 = scalarField3D(x0, y0, zFixed);
                    double z10 = scalarField3D(x1, y0, zFixed);
                    double z11 = scalarField3D(x1, y1, zFixed);
                    double z01 = scalarField3D(x0, y1, zFixed);

                    meshBuilder.AddQuad(
                        new Point3D(x0, y0, z00),
                        new Point3D(x1, y0, z10),
                        new Point3D(x1, y1, z11),
                        new Point3D(x0, y1, z01)
                    );
                }
            }

            var mesh = meshBuilder.ToMesh();
            var material = MaterialHelper.CreateMaterial(Colors.Cyan);
            var geom = new GeometryModel3D { Geometry = mesh, Material = material, BackMaterial = material };
            ViewPort.Children.Add(new ModelVisual3D { Content = geom });

            // Marker anzeigen, wenn übergeben
            if (px.HasValue && py.HasValue && pz.HasValue)
            {
                var sphere = new SphereVisual3D
                {
                    Center = new Point3D(px.Value, py.Value, pz.Value),
                    Radius = (xMax - xMin) * 0.02,
                    Fill = Brushes.Red
                };
                ViewPort.Children.Add(sphere);
            }
            // XY-Ebene (Z = konstant)
            var gridXY = new GridLinesVisual3D
            {
                Width = 10,
                Length = 10,
                MinorDistance = 1,
                Thickness = 0.01,
                Center = new Point3D(0, 0, 0),
                Normal = new Vector3D(0, 0, 1), // Z-Richtung → XY-Ebene
                Fill = Brushes.Gray
            };
            ViewPort.Children.Add(gridXY);

            // XZ-Ebene (Y = konstant)
            var gridXZ = new GridLinesVisual3D
            {
                Width = 10,
                Length = 10,
                MinorDistance = 1,
                Thickness = 0.01,
                Center = new Point3D(0, 0, 0),
                Normal = new Vector3D(0, 1, 0), // Y-Richtung → XZ-Ebene
                Fill = Brushes.DimGray
            };
            ViewPort.Children.Add(gridXZ);

            // YZ-Ebene (X = konstant)
            var gridYZ = new GridLinesVisual3D
            {
                Width = 10,
                Length = 10,
                MinorDistance = 1,
                Thickness = 0.01,
                Center = new Point3D(0, 0, 0),
                Normal = new Vector3D(1, 0, 0), // X-Richtung → YZ-Ebene
                Fill = Brushes.DarkGray
            };
            ViewPort.Children.Add(gridYZ);


        }
    }
}
