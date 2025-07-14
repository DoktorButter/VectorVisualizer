using System;
using System.Drawing;
using System.Windows.Forms;
using NCalc;
using VectorVisualizer.MathLib;

namespace VectorVisualizer.GUI
{
    public partial class Form1 : Form
    {
        private Button btnSkalarfeld;
        private Button btnVektorfeld;
        private CheckBox cbIs3D;

        public Form1()
        {
            InitializeComponent();
            InitializeStartScreen();
        }

        private void InitializeStartScreen()
        {
            this.BackColor = Color.FromArgb(33, 33, 33);
            this.ForeColor = Color.White;
            this.Text = "Vektorkalkül Visualisierung";
            this.Size = new Size(600, 400);

            btnSkalarfeld = new Button();
            btnSkalarfeld.Text = "Skalarfeld";
            btnSkalarfeld.Size = new Size(120, 40);
            btnSkalarfeld.Location = new Point(100, 100);
            btnSkalarfeld.Click += BtnSkalarfeld_Click;
            this.Controls.Add(btnSkalarfeld);

            btnVektorfeld = new Button();
            btnVektorfeld.Text = "Vektorfeld";
            btnVektorfeld.Size = new Size(120, 40);
            btnVektorfeld.Location = new Point(100, 160);
            btnVektorfeld.Click += BtnVektorfeld_Click;
            this.Controls.Add(btnVektorfeld);

            cbIs3D = new CheckBox();
            cbIs3D.Text = "Ist die Funktion 3D?";
            cbIs3D.ForeColor = Color.White;
            cbIs3D.BackColor = Color.FromArgb(33, 33, 33);
            cbIs3D.Location = new Point(250, 130);
            cbIs3D.AutoSize = true;
            this.Controls.Add(cbIs3D);
        }

        private void BtnSkalarfeld_Click(object sender, EventArgs e)
        {
            {
}
            bool is3D = cbIs3D.Checked;

            this.Controls.Clear();
            this.BackColor = Color.FromArgb(33, 33, 33);
            this.ForeColor = Color.White;

            Label lblFunc = new Label();
            lblFunc.Text = is3D ? "Skalarfeld f(x, y, z):" : "Skalarfeld f(x, y):";
            lblFunc.Location = new Point(30, 30);
            lblFunc.AutoSize = true;
            this.Controls.Add(lblFunc);

            TextBox txtFunc = new TextBox();
            txtFunc.Location = new Point(170, 25);
            txtFunc.Width = 300;
            this.Controls.Add(txtFunc);

            Label lblPoint = new Label();
            lblPoint.Text = is3D ? "Punkt (x, y, z):" : "Punkt (x, y):";
            lblPoint.Location = new Point(30, 70);
            lblPoint.AutoSize = true;
            this.Controls.Add(lblPoint);

            TextBox txtX = new TextBox();
            txtX.Location = new Point(170, 65);
            txtX.Width = 60;
            this.Controls.Add(txtX);

            TextBox txtY = new TextBox();
            txtY.Location = new Point(240, 65);
            txtY.Width = 60;
            this.Controls.Add(txtY);

            TextBox txtZ = null;
            if (is3D)
            {
                txtZ = new TextBox();
                txtZ.Location = new Point(310, 65);
                txtZ.Width = 60;
                this.Controls.Add(txtZ);
            }

            Label lblInfo = new Label();
            lblInfo.Text = is3D
                ? "Hinweis: Nur x, y und z verwenden. Beispiel: x*y + z"
                : "Hinweis: Nur x und y verwenden. Beispiel: 2*x + y*y";
            lblInfo.ForeColor = Color.LightGray;
            lblInfo.Location = new Point(30, 110);
            lblInfo.AutoSize = true;
            this.Controls.Add(lblInfo);

            CheckBox cbGradient = new CheckBox();
            cbGradient.Text = "Gradient anzeigen (Falls valide Punkte gegeben sind)";
            cbGradient.ForeColor = Color.White;
            cbGradient.BackColor = Color.FromArgb(33, 33, 33);
            cbGradient.Location = new Point(170, 130);
            cbGradient.AutoSize = true;
            this.Controls.Add(cbGradient);
            Button btnVisualisieren = new Button();
            btnVisualisieren.Text = "Visualisieren";
            btnVisualisieren.Location = new Point(170, 170);
            btnVisualisieren.Click += (s, args) =>
            {
                try
                {
                    string expr = txtFunc.Text.Trim();
                    if (string.IsNullOrWhiteSpace(expr))
                    {
                        MessageBox.Show("Bitte eine Funktion eingeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Vorab prüfen, ob Ausdruck gültig ist
                    try
                    {
                        var testExpr = new Expression(expr);
                        testExpr.Parameters["x"] = 1.0;
                        testExpr.Parameters["y"] = 1.0;
                        testExpr.Evaluate();
                    }
                    catch
                    {
                        MessageBox.Show("Die Funktion ist ungültig.", "Parserfehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    double? px = null, py = null, pz = null;
                    double xVal = 0, yVal = 0;
                    bool hasX = !string.IsNullOrWhiteSpace(txtX.Text);
                    bool hasY = !string.IsNullOrWhiteSpace(txtY.Text);

                    if ((hasX && !hasY) || (!hasX && hasY))
                    {
                        MessageBox.Show("Bitte entweder beide Koordinaten x und y angeben oder keine.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (hasX && !double.TryParse(txtX.Text, out xVal))
                    {
                        MessageBox.Show("x ist keine gültige Zahl.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (hasY && !double.TryParse(txtY.Text, out yVal))
                    {
                        MessageBox.Show("y ist keine gültige Zahl.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (hasX) px = xVal;
                    if (hasY) py = yVal;

                    if (is3D && txtZ != null && !string.IsNullOrWhiteSpace(txtZ.Text))
                    {
                        if (!double.TryParse(txtZ.Text, out double zVal))
                        {
                            MessageBox.Show("z ist keine gültige Zahl.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        pz = zVal;
                    }

                    if (!is3D)
                    {
                        int size = 50;
                        double xMin = -5, xMax = 5;
                        double yMin = -5, yMax = 5;
                        double stepX = (xMax - xMin) / size;
                        double stepY = (yMax - yMin) / size;

                        double[,] values = new double[size, size];

                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                double x = xMin + i * stepX;
                                double y = yMin + j * stepY;
                                try
                                {
                                    values[i, j] = EvaluateScalarField(expr, x, y);
                                }
                                catch
                                {
                                    values[i, j] = double.NaN;
                                }
                            }
                        }

                        string? gradientText = null;
                        if (cbGradient.Checked && px.HasValue && py.HasValue)
                        {
                            Func<double, double, double> funcDelegate = (x, y) => EvaluateScalarField(expr, x, y);
                            var grad = FieldOperations.ComputeGradient2D(funcDelegate, px.Value, py.Value);
                            gradientText = $"(∂f/∂x, ∂f/∂y) = ({Math.Round(grad[0], 4)}, {Math.Round(grad[1], 4)})";
                        }

                        var plotWindow = new PlotForm(values, xMin, xMax, yMin, yMax, gradientText, cbGradient.Checked);
                        plotWindow.StartPosition = FormStartPosition.CenterScreen;
                        plotWindow.Show();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler bei der Berechnung:\n" + ex.Message,
                        "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            this.Controls.Add(btnVisualisieren);
        }
        
        private void BtnVektorfeld_Click(object sender, EventArgs e)
        {
            {
}
            this.Controls.Clear();
            this.BackColor = Color.FromArgb(33, 33, 33);
            this.ForeColor = Color.White;

            Label lblFx = new Label();
            lblFx.Text = "Fx(x, y):";
            lblFx.Location = new Point(30, 30);
            lblFx.AutoSize = true;
            this.Controls.Add(lblFx);

            TextBox txtFx = new TextBox();
            txtFx.Location = new Point(100, 25);
            txtFx.Width = 300;
            this.Controls.Add(txtFx);

            Label lblFy = new Label();
            lblFy.Text = "Fy(x, y):";
            lblFy.Location = new Point(30, 70);
            lblFy.AutoSize = true;
            this.Controls.Add(lblFy);

            TextBox txtFy = new TextBox();
            txtFy.Location = new Point(100, 65);
            txtFy.Width = 300;
            this.Controls.Add(txtFy);

            Label lblPoint = new Label();
            lblPoint.Text = "Punkt (x, y):";
            lblPoint.Location = new Point(30, 110);
            lblPoint.AutoSize = true;
            this.Controls.Add(lblPoint);

            TextBox txtX = new TextBox();
            txtX.Location = new Point(130, 105);
            txtX.Width = 60;
            this.Controls.Add(txtX);

            TextBox txtY = new TextBox();
            txtY.Location = new Point(200, 105);
            txtY.Width = 60;
            this.Controls.Add(txtY);

            CheckBox cbDivergenz = new CheckBox();
            cbDivergenz.Text = "Divergenz anzeigen";
            cbDivergenz.ForeColor = Color.White;
            cbDivergenz.BackColor = Color.FromArgb(33, 33, 33);
            cbDivergenz.Location = new Point(30, 150);
            cbDivergenz.AutoSize = true;
            this.Controls.Add(cbDivergenz);

            CheckBox cbRotation = new CheckBox();
            cbRotation.Text = "Rotation anzeigen";
            cbRotation.ForeColor = Color.White;
            cbRotation.BackColor = Color.FromArgb(33, 33, 33);
            cbRotation.Location = new Point(30, 180);
            cbRotation.AutoSize = true;
            this.Controls.Add(cbRotation);

            Button btnVisualisieren = new Button();
            btnVisualisieren.Text = "Visualisieren";
            btnVisualisieren.ForeColor = Color.White;
            btnVisualisieren.BackColor = Color.FromArgb(33, 33, 33);
            btnVisualisieren.Location = new Point(30, 220);
            btnVisualisieren.AutoSize = true;
            btnVisualisieren.Click += (s, args) =>
            {
                try
                {
                    string fxExpr = txtFx.Text.Trim();
                    string fyExpr = txtFy.Text.Trim();

                    if (string.IsNullOrWhiteSpace(fxExpr) || string.IsNullOrWhiteSpace(fyExpr))
                    {
                        MessageBox.Show("Bitte sowohl Fx als auch Fy eingeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Ausdruck prüfen
                    try
                    {
                        var testFx = new Expression(fxExpr);
                        var testFy = new Expression(fyExpr);
                        testFx.Parameters["x"] = 1.0;
                        testFx.Parameters["y"] = 1.0;
                        testFy.Parameters["x"] = 1.0;
                        testFy.Parameters["y"] = 1.0;
                        testFx.Evaluate();
                        testFy.Evaluate();
                    }
                    catch
                    {
                        MessageBox.Show("Mindestens ein Ausdruck ist ungültig.", "Parserfehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Optionaler Punkt
                    double? px = null, py = null;
                    bool hasX = !string.IsNullOrWhiteSpace(txtX.Text);
                    bool hasY = !string.IsNullOrWhiteSpace(txtY.Text);

                    if ((hasX && !hasY) || (!hasX && hasY))
                    {
                        MessageBox.Show("Bitte entweder beide Koordinaten x und y angeben oder keine.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    double xVal = 0, yVal = 0;
                    if (hasX && !double.TryParse(txtX.Text, out xVal))
                    {
                        MessageBox.Show("x ist keine gültige Zahl.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (hasY && !double.TryParse(txtY.Text, out yVal))
                    {
                        MessageBox.Show("y ist keine gültige Zahl.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (hasX && hasY)
                    {
                        px = xVal;
                        py = yVal;
                    }


                    // Feld berechnen
                    int size = 25;
                    double xMin = -5, xMax = 5;
                    double yMin = -5, yMax = 5;
                    double stepX = (xMax - xMin) / size;
                    double stepY = (yMax - yMin) / size;

                    double[][,] vectorField = new double[2][,];
                    vectorField[0] = new double[size, size]; // Fx
                    vectorField[1] = new double[size, size]; // Fy

                    double[,] divergence = null;
                    double[,] rotation = null;

                    Func<double, double, double[]> vectorFunc = (x, y) =>
                    {
                        double fx = EvaluateScalarField(fxExpr, x, y);
                        double fy = EvaluateScalarField(fyExpr, x, y);
                        return new double[] { fx, fy };
                    };

                    if (cbDivergenz.Checked)
                    {
                        divergence = new double[size, size];
                    }

                    if (cbRotation.Checked)
                    {
                        rotation = new double[size, size];
                    }

                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            double x = xMin + i * stepX;
                            double y = yMin + j * stepY;
                            try
                            {
                                var vec = vectorFunc(x, y);
                                vectorField[0][i, j] = vec[0]; // Fx
                                vectorField[1][i, j] = vec[1]; // Fy

                                if (cbDivergenz.Checked)
                                    divergence[i,j] = FieldOperations.ComputeDivergence2D(vectorFunc, x, y);

                                if (cbRotation.Checked)
                                    rotation[i, j] = FieldOperations.ComputeCurl2D(vectorFunc, x, y);
                            }
                            catch
                            {
                                vectorField[0][i, j] = double.NaN;
                                vectorField[1][i, j] = double.NaN;
                                if (divergence != null) divergence[i, j] = double.NaN;
                                if (rotation != null) rotation[i, j] = double.NaN;
                            }
                        }
                    }
                    var plotWindow = new VektorPlotForm2D(vectorField, xMin, xMax, yMin, yMax, divergence, rotation, px, py);
                    plotWindow.StartPosition = FormStartPosition.CenterScreen;
                    plotWindow.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler bei der Berechnung:\n" + ex.Message,
                        "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            };
            this.Controls.Add(btnVisualisieren);
        }


        private double EvaluateScalarField(string expressionText, double x, double y, double? z = null)
        {
            var expr = new Expression(expressionText);
            expr.Parameters["x"] = x;
            expr.Parameters["y"] = y;
            if (z.HasValue)
                expr.Parameters["z"] = z.Value;

            var result = expr.Evaluate();
            return Convert.ToDouble(result);
        }
        private double Evaluate(Expression expr, double x, double y)
        {
            expr.Parameters["x"] = x;
            expr.Parameters["y"] = y;
            return Convert.ToDouble(expr.Evaluate());
        }

    }
}
