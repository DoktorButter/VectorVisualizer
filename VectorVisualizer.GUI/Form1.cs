using NCalc;
using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
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
            this.Controls.Clear();
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
        private string NormalizeExpression(string expr)
        {
            // einfache Variablen: x^2 → Pow(x,2)
            expr = Regex.Replace(expr, @"(\b[a-zA-Z_]\w*)\^(\d+)", "Pow($1,$2)");
            // Klammerausdrücke: (x+1)^2 → Pow((x+1),2)
            expr = Regex.Replace(expr, @"\(([^()]+)\)\^(\d+)", "Pow(($1),$2)");
            return expr;
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

            CheckBox cbGradient = new CheckBox();
            cbGradient.Text = "Gradient anzeigen (Falls valide Punkte gegeben sind)";
            cbGradient.ForeColor = Color.White;
            cbGradient.BackColor = Color.FromArgb(33, 33, 33);
            cbGradient.Location = new Point(30, 110);
            cbGradient.AutoSize = true;
            this.Controls.Add(cbGradient);
            Button btnVisualisieren = new Button();
            btnVisualisieren.Text = "Visualisieren";
            btnVisualisieren.Location = new Point(30, 140);
            btnVisualisieren.Size = new Size(120, 30);
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
                        expr = NormalizeExpression(expr);
                        var testExpr = new Expression(expr);

                        testExpr.Parameters["x"] = 1.0;
                        testExpr.Parameters["y"] = 1.0;
                        if (is3D)
                            testExpr.Parameters["z"] = 1.0;

                        var result = testExpr.Evaluate();
                        if (result == null || double.IsNaN(Convert.ToDouble(result)))
                            throw new Exception("Ergebnis ist ungültig");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Die Funktion ist ungültig oder nicht auswertbar.\nDetails: " + ex.Message,
                            "Parserfehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        for (int i = 0; i < size; i++) // i = x, j = y
                        {
                            for (int j = 0; j < size; j++)
                            {
                                double x = xMin + i * stepX;
                                double y = yMin + j * stepY;
                                try
                                {
                                    values[size - j - 1,i] = EvaluateScalarField(expr, x, y);
                                }
                                catch
                                {
                                    values[size - j - 1,i] = double.NaN;
                                }
                            }
                        }

                        string? gradientText = null;
                        if (cbGradient.Checked && px.HasValue && py.HasValue)
                        {
                            Func<double, double, double> funcDelegate = (x, y) => EvaluateScalarField(expr, x, y);
                            var grad = FieldOperations.ComputeGradient2D(funcDelegate, px.Value, py.Value);
                            gradientText = $"(∂f/∂x, ∂f/∂y): \n ({Math.Round(grad[0], 4)}, {Math.Round(grad[1], 4)})";
                        }

                        var plotWindow = new PlotForm(values, xMin, xMax, yMin, yMax, gradientText, cbGradient.Checked);
                        plotWindow.StartPosition = FormStartPosition.CenterScreen;
                        plotWindow.Show();

                    }
                    else
                    {
                        // Ausdruck validieren
                        expr = NormalizeExpression(expr);
                        var testExpr = new Expression(expr);
                        testExpr.Parameters["x"] = 1.0;
                        testExpr.Parameters["y"] = 1.0;
                        testExpr.Parameters["z"] = 1.0;
                        try
                        {
                            testExpr.Evaluate();
                        }
                        catch
                        {
                            MessageBox.Show("Funktion ungültig.", "Fehler");
                            return;
                        }
                        Func<double, double, double, double> func3D = (x, y, z) =>
                            EvaluateScalarField(expr, x, y, z);

                        // Punkt einlesen (optional)
                        double? ppx = null, ppy = null, ppz = null;

                        bool hasZ = !string.IsNullOrWhiteSpace(txtZ.Text);

                        // Fehler, wenn nur teilweise befüllt
                        if ((hasX || hasY || hasZ) && !(hasX && hasY && hasZ))
                        {
                            MessageBox.Show("Bitte entweder alle drei Koordinaten (x, y, z) angeben oder keine.",
                                "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Falls alle drei gesetzt sind, versuche sie zu parsen
                        if (hasX && hasY && hasZ)
                        {
                            if (
                                double.TryParse(txtX.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out xVal) &&
                                double.TryParse(txtY.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out yVal) &&
                                double.TryParse(txtZ.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double zVal))
                            {
                                ppx = xVal;
                                ppy = yVal;
                                ppz = zVal;
                            }
                            else
                            {
                                MessageBox.Show("Bitte gültige Zahlen für x, y und z eingeben.",
                                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }


                        // Gradienten berechnen (falls gewünscht)
                        double zFixed = ppz ?? 0; // Ebene, auf der geplottet wird
                        string? gradientText = null;

                        if (cbGradient.Checked && ppx.HasValue && ppy.HasValue && ppz.HasValue)
                        {
                            var grad = FieldOperations.ComputeGradient3D(func3D, ppx.Value, ppy.Value, ppz.Value);
                            gradientText =
                                $"(∂f/∂x, ∂f/∂y, ∂f/∂z): \n " +
                                $"({Math.Round(grad[0], 4)}, {Math.Round(grad[1], 4)}, {Math.Round(grad[2], 4)})";
                        }

                        // Visualisierung starten
                        var plotWindow = new PlotForm3D(func3D, zFixed, 80, gradientText);
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
            this.Controls.Add(CreateBackButton());

        }

        private void BtnVektorfeld_Click(object sender, EventArgs e)
        {
            bool is3D = cbIs3D.Checked;

            this.Controls.Clear();
            this.BackColor = Color.FromArgb(33, 33, 33);
            this.ForeColor = Color.White;

            if (!is3D)
            {
                Label lblFx = new Label { Text = "Fx(x, y):", Location = new Point(30, 30), AutoSize = true };
                TextBox txtFx = new TextBox { Location = new Point(100, 25), Width = 300 };
                this.Controls.Add(lblFx);
                this.Controls.Add(txtFx);

                Label lblFy = new Label { Text = "Fy(x, y):", Location = new Point(30, 70), AutoSize = true };
                TextBox txtFy = new TextBox { Location = new Point(100, 65), Width = 300 };
                this.Controls.Add(lblFy);
                this.Controls.Add(txtFy);

                Label lblPoint = new Label { Text = "Punkt (x, y):", Location = new Point(30, 110), AutoSize = true };
                TextBox txtX = new TextBox { Location = new Point(130, 105), Width = 60 };
                TextBox txtY = new TextBox { Location = new Point(200, 105), Width = 60 };
                this.Controls.Add(lblPoint);
                this.Controls.Add(txtX);
                this.Controls.Add(txtY);

                CheckBox cbDivergenz = new CheckBox { Text = "Divergenz anzeigen", Location = new Point(30, 150), AutoSize = true, ForeColor = Color.White, BackColor = Color.FromArgb(33, 33, 33) };
                CheckBox cbRotation = new CheckBox { Text = "Rotation anzeigen", Location = new Point(30, 180), AutoSize = true, ForeColor = Color.White, BackColor = Color.FromArgb(33, 33, 33) };
                this.Controls.Add(cbDivergenz);
                this.Controls.Add(cbRotation);

                Button btnVisualisieren = new Button { Text = "Visualisieren", Location = new Point(30, 220), AutoSize = true, BackColor = Color.FromArgb(33, 33, 33), ForeColor = Color.White };
                btnVisualisieren.Size = new Size(120, 30);
                this.Controls.Add(btnVisualisieren);
                this.Controls.Add(CreateBackButton());

                btnVisualisieren.Click += (s, args) =>
                {
                    try
                    {
                        string fxExpr = NormalizeExpression(txtFx.Text.Trim());
                        string fyExpr = NormalizeExpression(txtFy.Text.Trim());

                        if (string.IsNullOrWhiteSpace(fxExpr) || string.IsNullOrWhiteSpace(fyExpr))
                        {
                            MessageBox.Show("Bitte sowohl Fx als auch Fy eingeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

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

                        int size = 25;
                        double xMin = -5, xMax = 5;
                        double yMin = -5, yMax = 5;
                        double stepX = (xMax - xMin) / size;
                        double stepY = (yMax - yMin) / size;

                        double[][,] vectorField = new double[2][,];
                        vectorField[0] = new double[size, size];
                        vectorField[1] = new double[size, size];

                        double[,] divergence = null;
                        double[,] rotation = null;

                        Func<double, double, double[]> vectorFunc = (x, y) =>
                        {
                            double fx = EvaluateScalarField(fxExpr, x, y);
                            double fy = EvaluateScalarField(fyExpr, x, y);
                            return new double[] { fx, fy };
                        };

                        if (cbDivergenz.Checked) divergence = new double[size, size];
                        if (cbRotation.Checked) rotation = new double[size, size];

                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                double x = xMin + i * stepX;
                                double y = yMin + j * stepY;
                                try
                                {
                                    var vec = vectorFunc(x, y);
                                    vectorField[0][i, j] = vec[0];
                                    vectorField[1][i, j] = vec[1];

                                    if (cbDivergenz.Checked)
                                        divergence[i, j] = FieldOperations.ComputeDivergence2D(vectorFunc, x, y);
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
                        MessageBox.Show("Fehler bei der Berechnung:\n" + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
            }
            else
            {
                InitializeVektorfeld3DGUI();
            }
        }
        private void InitializeVektorfeld3DGUI()
        {
            this.Controls.Clear();
            this.BackColor = Color.FromArgb(33, 33, 33);
            this.ForeColor = Color.White;

            // Fx(x,y,z)
            Label lblFx = new Label { Text = "Fx(x, y, z):", Location = new Point(30, 30), AutoSize = true };
            TextBox txtFx = new TextBox { Name = "txtFx3D", Location = new Point(130, 25), Width = 300 };
            this.Controls.Add(lblFx);
            this.Controls.Add(txtFx);

            // Fy(x,y,z)
            Label lblFy = new Label { Text = "Fy(x, y, z):", Location = new Point(30, 70), AutoSize = true };
            TextBox txtFy = new TextBox { Name = "txtFy3D", Location = new Point(130, 65), Width = 300 };
            this.Controls.Add(lblFy);
            this.Controls.Add(txtFy);

            // Fz(x,y,z)
            Label lblFz = new Label { Text = "Fz(x, y, z):", Location = new Point(30, 110), AutoSize = true };
            TextBox txtFz = new TextBox { Name = "txtFz3D", Location = new Point(130, 105), Width = 300 };
            this.Controls.Add(lblFz);
            this.Controls.Add(txtFz);

            // Punkt (x,y,z)
            Label lblPoint = new Label { Text = "Punkt (x, y, z):", Location = new Point(30, 150), AutoSize = true };
            TextBox txtX = new TextBox { Name = "txtX3D", Location = new Point(130, 145), Width = 60 };
            TextBox txtY = new TextBox { Name = "txtY3D", Location = new Point(200, 145), Width = 60 };
            TextBox txtZ = new TextBox { Name = "txtZ3D", Location = new Point(270, 145), Width = 60 };
            this.Controls.Add(lblPoint);
            this.Controls.Add(txtX);
            this.Controls.Add(txtY);
            this.Controls.Add(txtZ);

            // Checkbox Divergenz
            CheckBox cbDivergenz = new CheckBox
            {
                Text = "Divergenz anzeigen",
                Name = "cbDivergenz3D",
                Location = new Point(30, 190),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(33, 33, 33)
            };
            this.Controls.Add(cbDivergenz);

            // Checkbox Rotation
            CheckBox cbRotation = new CheckBox
            {
                Text = "Rotation anzeigen",
                Name = "cbRotation3D",
                Location = new Point(30, 220),
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(33, 33, 33)
            };
            this.Controls.Add(cbRotation);

            // Button
            Button btnVisualisieren = new Button
            {
                Text = "Visualisieren",
                Location = new Point(30, 260),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(33, 33, 33),
                ForeColor = Color.White
            };
            this.Controls.Add(btnVisualisieren);
            this.Controls.Add(CreateBackButton());

            btnVisualisieren.Click += (s, e) =>
            {
                try
                {
                    // Controls holen
                    string fxExpr = NormalizeExpression(((TextBox)this.Controls["txtFx3D"]).Text.Trim());
                    string fyExpr = NormalizeExpression(((TextBox)this.Controls["txtFy3D"]).Text.Trim());
                    string fzExpr = NormalizeExpression(((TextBox)this.Controls["txtFz3D"]).Text.Trim());

                    string xText = ((TextBox)this.Controls["txtX3D"]).Text.Trim();
                    string yText = ((TextBox)this.Controls["txtY3D"]).Text.Trim();
                    string zText = ((TextBox)this.Controls["txtZ3D"]).Text.Trim();

                    bool hasX = !string.IsNullOrWhiteSpace(xText);
                    bool hasY = !string.IsNullOrWhiteSpace(yText);
                    bool hasZ = !string.IsNullOrWhiteSpace(zText);

                    bool showDiv = ((CheckBox)this.Controls["cbDivergenz3D"]).Checked;
                    bool showRot = ((CheckBox)this.Controls["cbRotation3D"]).Checked;

                    // Prüfung: leere oder alle 3 Koordinaten
                    if ((hasX || hasY || hasZ) && !(hasX && hasY && hasZ))
                    {
                        MessageBox.Show("Bitte entweder alle drei Koordinaten x, y, z angeben oder keine.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Ausdruck prüfen
                    try
                    {
                        var t1 = new Expression(fxExpr);
                        var t2 = new Expression(fyExpr);
                        var t3 = new Expression(fzExpr);
                        t1.Parameters["x"] = 1.0;
                        t1.Parameters["y"] = 1.0;
                        t1.Parameters["z"] = 1.0;
                        t2.Parameters["x"] = 1.0;
                        t2.Parameters["y"] = 1.0;
                        t2.Parameters["z"] = 1.0;
                        t3.Parameters["x"] = 1.0;
                        t3.Parameters["y"] = 1.0;
                        t3.Parameters["z"] = 1.0;
                        t1.Evaluate(); t2.Evaluate(); t3.Evaluate();
                    }
                    catch
                    {
                        MessageBox.Show("Mindestens ein Ausdruck ist ungültig.", "Parserfehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Punkt optional einlesen
                    double? px = null, py = null, pz = null;
                    if (hasX && hasY && hasZ)
                    {
                        if (double.TryParse(xText, NumberStyles.Float, CultureInfo.InvariantCulture, out double xVal) &&
                            double.TryParse(yText, NumberStyles.Float, CultureInfo.InvariantCulture, out double yVal) &&
                            double.TryParse(zText, NumberStyles.Float, CultureInfo.InvariantCulture, out double zVal))
                        {
                            px = xVal; py = yVal; pz = zVal;
                        }
                        else
                        {
                            MessageBox.Show("Bitte gültige Werte für x, y, z eingeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Vektorfeldfunktion
                    Func<double, double, double, double[]> vectorFunc = (x, y, z) =>
                    {
                        return new double[]
                        {
                EvaluateScalarField(fxExpr, x, y, z),
                EvaluateScalarField(fyExpr, x, y, z),
                EvaluateScalarField(fzExpr, x, y, z)
                        };
                    };

                    // Feld berechnen
                    int size = 10;
                    double min = -5, max = 5;
                    double step = (max - min) / size;

                    var vectors = new double[3][,,];
                    vectors[0] = new double[size, size, size]; // Fx
                    vectors[1] = new double[size, size, size]; // Fy
                    vectors[2] = new double[size, size, size]; // Fz

                    double[,,] divergence = showDiv ? new double[size, size, size] : null;
                    double[][,,] curl = showRot ? new double[3][,,] : null;
                    if (showRot)
                    {
                        curl[0] = new double[size, size, size]; // curl_x
                        curl[1] = new double[size, size, size]; // curl_y
                        curl[2] = new double[size, size, size]; // curl_z
                    }

                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            for (int k = 0; k < size; k++)
                            {
                                double x = min + i * step;
                                double y = min + j * step;
                                double z = min + k * step;
                                try
                                {
                                    var vec = vectorFunc(x, y, z);
                                    vectors[0][i, j, k] = vec[0];
                                    vectors[1][i, j, k] = vec[1];
                                    vectors[2][i, j, k] = vec[2];

                                    if (showDiv)
                                        divergence[i, j, k] = FieldOperations.ComputeDivergence3D(vectorFunc, x, y, z);

                                    if (showRot)
                                    {
                                        var rot = FieldOperations.ComputeCurl3D(vectorFunc, x, y, z);
                                        curl[0][i, j, k] = rot[0];
                                        curl[1][i, j, k] = rot[1];
                                        curl[2][i, j, k] = rot[2];
                                    }
                                }
                                catch
                                {
                                    vectors[0][i, j, k] = vectors[1][i, j, k] = vectors[2][i, j, k] = double.NaN;
                                    if (showDiv) divergence[i, j, k] = double.NaN;
                                    if (showRot) { curl[0][i, j, k] = curl[1][i, j, k] = curl[2][i, j, k] = double.NaN; }
                                }
                            }
                        }
                    }

                    // Entscheide, ob das InfoPanel angezeigt werden soll
                    bool showInfoPanel = (showDiv || showRot) && px.HasValue && py.HasValue && pz.HasValue;

                    var plotWindow = new VektorPlotForm3D(
                        vectors,
                        -5, 5, -5, 5, -5, 5,
                        showInfoPanel ? divergence : null,
                        showInfoPanel ? curl : null,
                        px, py, pz);

                    plotWindow.StartPosition = FormStartPosition.CenterScreen;
                    plotWindow.Show();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler:\n" + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

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

        private Button CreateBackButton()
        {
            Button btnBack = new Button();
            btnBack.Text = "Zurück";
            btnBack.Size = new Size(100, 30);
            btnBack.BackColor = Color.FromArgb(55, 55, 55);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(this.ClientSize.Width - 120, this.ClientSize.Height - 50);
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnBack.Click += (s, e) => InitializeStartScreen();
            return btnBack;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}