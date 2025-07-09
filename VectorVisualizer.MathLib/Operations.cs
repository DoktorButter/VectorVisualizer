namespace VectorVisualizer.MathLib
{
    public static class FieldOperations
    {
        private const double h = 1e-5; // Schrittweite

        // Annäherung des Gradienten eines 2D-Skalarfeldes an einem Punkt (x, y)
        public static double[] ComputeGradient2D(Func<double, double, double> scalarField, double x, double y)
        {
            double dx = (scalarField(x + h, y) - scalarField(x - h, y)) / (2 * h); // Änderung in x-Richtung
            double dy = (scalarField(x, y + h) - scalarField(x, y - h)) / (2 * h); // Änderung in y-Richtung
            return new double[] { dx, dy };
        }

        // Annäherung des Gradienten eines 3D-Skalarfeldes an einem Punkt (x, y, z)
        public static double[] ComputeGradient3D(Func<double, double, double, double> scalarField, double x, double y, double z)
        {
            double dx = (scalarField(x + h, y, z) - scalarField(x - h, y, z)) / (2 * h); // Änderung in x-Richtung
            double dy = (scalarField(x, y + h, z) - scalarField(x, y - h, z)) / (2 * h); // Änderung in y-Richtung
            double dz = (scalarField(x, y, z + h) - scalarField(x, y, z - h)) / (2 * h); // Änderung in z-Richtung
            return new double[] { dx, dy, dz };
        }

        // Annäherung der Divergenz eines 2D-Vektorfeldes
        public static double ComputeDivergence2D(Func<double, double, double[]> vectorField, double x, double y)
        {
            double dF1_dx = (vectorField(x + h, y)[0] - vectorField(x - h, y)[0]) / (2 * h); // Ableitung der x-Komponente nach x
            double dF2_dy = (vectorField(x, y + h)[1] - vectorField(x, y - h)[1]) / (2 * h); // Ableitung der y-Komponente nach y
            return dF1_dx + dF2_dy;
        }

        // Annäherung der Divergenz eines 3D-Vektorfeldes
        public static double ComputeDivergence3D(Func<double, double, double, double[]> vectorField, double x, double y, double z)
        {
            double dF1_dx = (vectorField(x + h, y, z)[0] - vectorField(x - h, y, z)[0]) / (2 * h); // Ableitung von x
            double dF2_dy = (vectorField(x, y + h, z)[1] - vectorField(x, y - h, z)[1]) / (2 * h); // Ableitung von y
            double dF3_dz = (vectorField(x, y, z + h)[2] - vectorField(x, y, z - h)[2]) / (2 * h); // Ableitung von z
            return dF1_dx + dF2_dy + dF3_dz;
        }

        // Annäherung der Rotation (Curl) eines 2D-Vektorfeldes.
        public static double ComputeCurl2D(Func<double, double, double[]> vectorField, double x, double y)
        {
            double dF2_dx = (vectorField(x + h, y)[1] - vectorField(x - h, y)[1]) / (2 * h); // Ableitung der y-Komponente nach x
            double dF1_dy = (vectorField(x, y + h)[0] - vectorField(x, y - h)[0]) / (2 * h); // Ableitung der x-Komponente nach y
            return dF2_dx - dF1_dy; // Differenz ergibt Rotationsstärke (nur z-Komponente)
        }

        // Annäherung der Rotation (Curl) eines 3D-Vektorfeldes.
        public static double[] ComputeCurl3D(Func<double, double, double, double[]> vectorField, double x, double y, double z)
        {
            // x-Komponente misst Rotation um die x-Achse (verursacht durch y- und z-Komponenten)
            double dF3_dy = (vectorField(x, y + h, z)[2] - vectorField(x, y - h, z)[2]) / (2 * h);
            double dF2_dz = (vectorField(x, y, z + h)[1] - vectorField(x, y, z - h)[1]) / (2 * h);
            double curl_x = dF3_dy - dF2_dz;

            // y-Komponente misst Rotation um die y-Achse
            double dF1_dz = (vectorField(x, y, z + h)[0] - vectorField(x, y, z - h)[0]) / (2 * h);
            double dF3_dx = (vectorField(x + h, y, z)[2] - vectorField(x - h, y, z)[2]) / (2 * h);
            double curl_y = dF1_dz - dF3_dx;

            // z-Komponente misst Rotation um die z-Achse
            double dF2_dx = (vectorField(x + h, y, z)[1] - vectorField(x - h, y, z)[1]) / (2 * h);
            double dF1_dy = (vectorField(x, y + h, z)[0] - vectorField(x, y - h, z)[0]) / (2 * h);
            double curl_z = dF2_dx - dF1_dy;

            return new double[] { curl_x, curl_y, curl_z };
        }
    }
}
