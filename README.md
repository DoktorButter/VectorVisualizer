# VectorVisualizer (SE_25_project)
This repositoury contains the software project we developed as our assignment

---

# Table of Contents
1. [Objective](#objective)
2. [Features](#features)  
3. [Theoretical Basis](#theoretical-basis)
4. [Software Design](#software-design)
5. [LLMs](#LLMs)
6. [Contributors](#contributors)
7. [License](#license)
---

# Objective

The goal of this software is to provide an interactive tool that both visualizes and manually computes the gradient, divergence, and curl of scalar and vector fields in 2D and 3D, in order to make their behavior easier to understand and explore.

---

# Features

-  Interactive visualization of scalar and vector fields
-  Manual implementation of:
  - **Gradient** (∇f)
  - **Divergence** (∇·F)
  - **Curl / Rotation** (∇×F)
-  2D and 3D plotting 
-  Educational explanations and visual demos for each concept

---

#  Theoretical Basis

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

### Planned Extensions
- Step-by-step explanation mode for educational purposes
- Application examples from theoretical physics
- Export functionality for visualizations and field data

---

# LLMs

| Einsatzzweck | Verwendetes Modell | Erfahrung / Bewertung der Nützlichkeit |
| ------------------------------------------ | ---------------------- | ------------------------------------------------------------- |
| Anforderungsanalyse | ChatGPT-4o | funktionierte perfekt |
| Architekturvorschläge / Designideen | ChatGPT-4o | Funktionierte meistens, manchmal wurden GUI und Logik zusammengewürfelt |
| Code-Generierung (z. B. Klassen, Methoden) | ChatGPT-4o |Die grobe Struktur war auf anhieb richtig, die Feinheiten waren jedoch sehr Fehlerhaft |
| Testfallgenerierung (z. B. Unit Tests) | ChatGPT-4o | funktionierte Fehlerfrei |
| Refactoring-Vorschläge | ChatGPT-4o | funktionierte Fehlerfrei, jedoch auch wenig benutzt |
| Code Review | ChatGPT-4o | Fehlerfrei, vor allem wenn es ums erklären des Codes ging |
| Dokumentation (README, API-Doku etc.) | ChatGPT-4o | funktionierte Fehlerfrei |
| Fehlersuche / Debugginghilfe | ChatGPT-4o | Nicht empfehlenswert, hat dauernd Fehler "behoben", indem er Sie durch andere Fehler ersetzt hat. Zum Ende hin wurde dies nicht mehr mit einer LLM gelöst |
| Versionsverwaltung (Git-Strategien) | ChatGPT-4o | funktionierte Fehlerfrei |
| Kommunikation (z. B. Commit Messages) | ChatGPT-4o | es wurde kein LLM für die Kommunikation genutzt |
| Sonstiges | ChatGPT-4o | |

---

# Contributors

In joint development by:

### Mr Seib (DoktorButter): student of applied computer science
 Focus area: Head of software development, assisting in coordination and documentation

### Mr Gläser (PascalGl04): student of geophysics and geoinformatics
 Focus area: assistaning in software development, coordination, tutorial, examples, documentation

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
