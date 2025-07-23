# VectorVisualizer in C#

---

##  1. Project Setup
- [X] C# project in Visual Studio

---

##  2. Grid structur
- [X] **Scalar Field**:
  - Class `ScalarField`:
    - Grid structure (e.g., 2D array)
    - Resolution / grid size
- [X] **Vector Field**:
  - Class `VectorField`:
    - Two or three 2D arrays (x,y,z)
---

##  3. Mathematical Operators
- [X] **Gradient of a scalar field**
  - Method in `ScalarField`: `VectorField ComputeGradient()`
  - (∂f/∂x, ∂f/∂y, ∂f/∂z)
- [X] **Divergence of a vector field**
  - Method in `VectorField`: `ScalarField ComputeDivergence()`
  - ∂Vx/∂x + ∂Vy/∂y + ∂Vz/∂z
- [X] **Curl of a vector field**
  - Method in `VectorField`: `VectorField ComputeCurl()`
  - In 3D: (∂Vz/∂y - ∂Vy/∂z, ∂Vx/∂z - ∂Vz/∂x, ∂Vy/∂x - ∂Vx/∂y)
  - In 2D: pseudo-scalar ∂Vy/∂x - ∂Vx/∂y or 3D curl with Vz = 0

---

##  4. Data Input 
- [X] Load data manually
  - Grid of scalar values or vector components (Vx, Vy, Vz)
- [X] Validate input / Error handling:
  - Matching grid sizes
  - Numeric parsing correct

---

##  5. Operator Application Logic
- [X] Class `FieldProcessor`:
  - Accepts a field and operator
  - Returns a computed result field (scalar or vector)

---

##  6. Visualization / GUI
- [X]
  - Input: Load scalar or vector field
  - Operator selection (ComboBox)
  - "Compute" button
  - Result display (2D/3D plot)
- [X] GUI logic:
  - OnClick: Load input -> apply operator -> show result
- [X] **Scalar Field**:
  - Heatmap / color map
  - Optional contour lines
- [X] **Vector Field**:
  - Arrow plots


---

##  7. Error Handling
- [X] Input validation (grid size, invalid values, empty fields)
- [X] Error messages -> GUI

---

##  8. Testing
- [X] Test cases with known results
- [X] GUI testing with edge cases

---

##  9. Tutorial
- [X] API documentation
- [X] Tutorial with presentation of the project, the possibilities and examples
- [X] general documentation

---

##  Optional ?
- [X] Full 3D field
- [ ] Procress bar for long calculations
- [X] all possible calc at ones
- [ ] Magnitude color vectors
- [X] Tooltips or help for operator explanations (in Readme)
      
---

