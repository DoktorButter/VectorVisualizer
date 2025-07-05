# VectorVisualizer in C#

---

##  1. Project Setup
- [ ] C# project in Visual Studio
- [ ] maybe NuGet packages:
  - `MathNet.Numerics`
  - for example `OxyPlot` or `HelixToolkit` for 2D/3D visualization
 

---

##  2. Grid structur
- [ ] **Scalar Field**:
  - Class `ScalarField`:
    - Grid structure (e.g., 2D array)
    - Resolution / grid size
- [ ] **Vector Field**:
  - Class `VectorField`:
    - Two or three 2D arrays (for x-, y-, and z-components)
    - Same grid structure as `ScalarField`
- [ ] Shared `FieldGrid` object for grid description (origin, spacing, dimensions) ?

---

##  3. Mathematical Operators
- [ ] **Gradient of a scalar field**
  - Method in `ScalarField`: `VectorField ComputeGradient()`
  - Use central differences (∂f/∂x, ∂f/∂y, ∂f/∂z)
- [ ] **Divergence of a vector field**
  - Method in `VectorField`: `ScalarField ComputeDivergence()`
  - ∂Vx/∂x + ∂Vy/∂y + ∂Vz/∂z
- [ ] **Curl of a vector field**
  - Method in `VectorField`: `VectorField ComputeCurl()`
  - In 3D: (∂Vz/∂y - ∂Vy/∂z, ∂Vx/∂z - ∂Vz/∂x, ∂Vy/∂x - ∂Vx/∂y)
  - In 2D: pseudo-scalar ∂Vy/∂x - ∂Vx/∂y or 3D curl with Vz = 0

---

##  4. Data Input 
- [ ] Load data manually or from file:
  - CSV / JSON / TXT / Excel...
  - Grid of scalar values or vector components (e.g., Vx, Vy, Vz)
- [ ] Validate input / Error handling:
  - Matching grid sizes
  - Numeric parsing correct

---

##  5. Operator Application Logic
- [ ] Class `FieldProcessor`:
  - Accepts a field and operator
  - Returns a computed result field (scalar or vector)
- [ ] maybe Enum `OperatorType { Gradient, Divergence, Curl }`

---

##  6. Visualization / GUI
### a) GUI Design
- [ ] maybe WPF or WinForms:
  - Input: Load scalar or vector field
  - Operator selection (ComboBox)
  - "Compute" button
  - Result display (2D/3D plot)
- [ ] GUI logic:
  - OnClick: Load input -> apply operator -> show result

### b) Field Rendering
- [ ] **Scalar Field**:
  - Heatmap / color map
  - Optional contour lines
- [ ] **Vector Field**:
  - Arrow plots
  - Color-coded magnitude
- [ ] For simple 2D:
  - `ScottPlot` or `OxyPlot` ?
- [ ] For 3D (optional):
  - `HelixToolkit.WPF` ?

---

##  7. Error Handling
- [ ] Input validation (grid size, invalid values, empty fields)
- [ ] Error messages -> GUI
- [ ] Tooltips or help for operator explanations

---
##  8. Testing
- [ ] Unit tests for all operators
- [ ] Test cases with known results:
  - e.g., f(x, y) = x² + y² → ∇f = (2x, 2y)
  - V(x, y) = (-y, x) → curl ≠ 0, divergence = 0
- [ ] GUI testing with standard and edge cases

---

##  Optional ?
- [ ] Full 3D field
- [ ] Procress bar for long calculations
- [ ] Symbolic field input (e.g., f(x, y) = sin(x)*cos(y))
- [ ] Custom operator combinations

---

