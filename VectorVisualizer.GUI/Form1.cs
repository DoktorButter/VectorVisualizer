using VectorVisualizer.MathLib;

namespace VectorVisualizer.GUI;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        MessageBox.Show("Form1 wurde geladen!");
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Func<double, double, double> scalar2D = (x, y) => x * x + y * y;

        double x = 1.0;
        double y = 2.0;

        var grad = FieldOperations.ComputeGradient2D(scalar2D, x, y);

        // Ergebnis anzeigen
        label1.Text = $"∇f({x}, {y}) = [{grad[0]:F4}, {grad[1]:F4}]";
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }
}
