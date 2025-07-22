using System;
using Xunit;
using VectorVisualizer.MathLib;

namespace VectorVisualizer.xUnitTest
{
    public class FieldOperationsTests
    {
        [Fact]
        public void ComputeGradient2D_ReturnsExpected()
        {
            Func<double, double, double> f = (x, y) => x * y;
            var grad = FieldOperations.ComputeGradient2D(f, 2.0, 3.0);
            Assert.InRange(grad[0], 2.99, 3.01); // ∂f/∂x = y = 3
            Assert.InRange(grad[1], 1.99, 2.01); // ∂f/∂y = x = 2
        }

        [Fact]
        public void ComputeGradient3D_ReturnsExpected()
        {
            Func<double, double, double, double> f = (x, y, z) => x * y + z;
            var grad = FieldOperations.ComputeGradient3D(f, 2.0, 3.0, 1.0);
            Assert.InRange(grad[0], 2.99, 3.01); // ∂f/∂x = y = 3
            Assert.InRange(grad[1], 1.99, 2.01); // ∂f/∂y = x = 2
            Assert.InRange(grad[2], 0.99, 1.01); // ∂f/∂z = 1
        }

        [Fact]
        public void ComputeDivergence2D_ReturnsExpected()
        {
            Func<double, double, double[]> F = (x, y) => new double[] { x * x, y * y };
            double div = FieldOperations.ComputeDivergence2D(F, 2.0, 3.0);
            // ∂F₁/∂x = 2x = 4, ∂F₂/∂y = 2y = 6 ⇒ Divergenz = 10
            Assert.InRange(div, 9.9, 10.1);
        }

        [Fact]
        public void ComputeDivergence3D_ReturnsExpected()
        {
            Func<double, double, double, double[]> F = (x, y, z) => new double[] { x * x, y * y, z * z };
            double div = FieldOperations.ComputeDivergence3D(F, 2.0, 3.0, 4.0);
            // ∂F₁/∂x = 4, ∂F₂/∂y = 6, ∂F₃/∂z = 8 ⇒ Divergenz = 18
            Assert.InRange(div, 17.9, 18.1);
        }

        [Fact]
        public void ComputeCurl2D_ReturnsExpected()
        {
            Func<double, double, double[]> F = (x, y) => new double[] { -y, x };
            double curl = FieldOperations.ComputeCurl2D(F, 1.0, 2.0);
            // ∇×F = ∂F₂/∂x - ∂F₁/∂y = 1 - (-1) = 2
            Assert.InRange(curl, 1.99, 2.01);
        }

        [Fact]
        public void ComputeCurl3D_ReturnsExpected()
        {
            Func<double, double, double, double[]> F = (x, y, z) => new double[]
            {
                y * z, // F₁
                z * x, // F₂
                x * y  // F₃
            };

            var curl = FieldOperations.ComputeCurl3D(F, 1.0, 2.0, 3.0);

            // ∂F₃/∂y = x = 1, ∂F₂/∂z = x = 1 → curl₁ = 1 - 1 = 0
            // ∂F₁/∂z = y = 2, ∂F₃/∂x = y = 2 → curl₂ = 2 - 2 = 0
            // ∂F₂/∂x = z = 3, ∂F₁/∂y = z = 3 → curl₃ = 3 - 3 = 0
            Assert.InRange(curl[0], -0.01, 0.01);
            Assert.InRange(curl[1], -0.01, 0.01);
            Assert.InRange(curl[2], -0.01, 0.01);
        }
    }
}
