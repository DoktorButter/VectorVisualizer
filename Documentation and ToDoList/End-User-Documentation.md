# Software Documentation (concept, under development)

- Software: VectorVisualizer
- Version:  x.x
- Date:     10. Jul 2025

---

# Table of Contents

1. [Objective](#objective)
2. [Features](#features)  
3. [Theoretical Basis](#theoretical-basis)
4. [Requirements](#requirements)
5. [Software Design](#software-design)
6. [Examples](#examples)
7. [API documentation](#API-documentation)
8. [License](#license)


---

# Objective

The aim of this software is to provide an interactive tool that both visualizes
and calculates the gradient, divergence and curvature of scalar and vector fields.
The resulting field is output and displayed.

---

# Features

-  Input scalar field or vector field
  - **Gradient** (∇f)
  - **Divergence** (∇·F)
  - **Curl / Rotation** (∇×F)
-  2D and 3D plotting of the input and output fields
  
---

# Theoretical Basis

###  Scalar Field

A scalar field assigns a single value (a scalar) to every point in space.

- **Example**:  
  `f(x, y) = x^2 + y^2`  
  This forms a bowl-shaped surface.

---

###  Vector Field

A vector field assigns a vector (magnitude and direction) to each point.

- **Example**:  
  `F(x, y) = (-y, x)`  
  This creates circular flow around the origin.

---

###  Gradient (∇f)

The gradient of a scalar field points in the direction of steepest ascent.

- **Definition**:  
  `∇f = [∂f/∂x, ∂f/∂y]`

- **Example**:  
  For `f(x, y) = x^2 + y^2`  
  → `∇f = [2x, 2y]`

  In electrostatics, the negative gradient of the potential U results in the electric vector field E

---

###  Divergence (∇·F)

Divergence measures how much a vector field spreads out from a point.

- **Definition**:  
  `div F = ∂F_x/∂x + ∂F_y/∂y`

- **Interpretation**:
  The divergence is a measure of how strongly vectors diverge, so sources and sinks in vector fields can be identified
  - Positive: source (outflow)
  - Negative: sink (inflow)

---

###  Curl / Rotation (∇×F)

Curl measures the rotational tendency of a vector field at a point (in 2D, it's a scalar).

- **Definition (2D)**:  
  `curl F = ∂F_y/∂x - ∂F_x/∂y`

- **Interpretation**:
  - Positive: counter-clockwise spin
  - Negative: clockwise spin
  - Zero: conservative field

---

# Requirements

---

# Software Design

This software is organized into two main components: a mathematics library and a graphical user interface (GUI).

---

### Math Library (MathLib)
This library is responsible for the implementation of the following operations:

- Gradient Calculator
- Divergence Calculator
- Curl Calculator

The gradient calculator takes a scalar, the divergence and curl calculator take vector fields as input and compute the result in 2D or 3D space.

---

### Graphical User Interface (GUI)
The GUI provides the front-end interface. It is responsible for:

- Input of scalar or vector fields
- Selection of the operation
- Visualization in 2D and 3D
- Interactive features such as zooming, rotating, and parameter adjustment
  
---

# Examples

---

# API documentation

---

# License

MIT License

Copyright (c) 2025 Kevin Seib, Pascal Gläser

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

text source: https://choosealicense.com/licenses/mit/

---


