# SE_25_project
This repositoury contains the software project we developed as our assignment

## Visualization of Scalar and Vector Fields

This project focuses on the **visualization** and **independent implementation** of key vector calculus operations—**gradient**, **divergence**, and **curl (rotation)**—applied to **scalar** and **vector fields**. It aims to provide both an educational and practical tool for understanding how these mathematical concepts behave in 2D and 3D space.

---

## Table of Contents
1. [Features](#features)  
2. [Concepts Covered](#concepts-covered)  

---

## Features

-  Interactive visualization of scalar and vector fields
-  Manual implementation of:
  - **Gradient** (∇f)
  - **Divergence** (∇·F)
  - **Curl / Rotation** (∇×F)
-  2D and 3D plotting 
-  Educational explanations and visual demos for each concept

---

##  Concepts Covered

###  Scalar Field

A scalar field assigns a single value (a number) to every point in space.

- **Example**:  
  `f(x, y) = x^2 + y^2`  
  This forms a bowl-shaped surface.

- **Applications**:
  - Temperature distribution
  - Elevation/topographic maps
  - Potential energy fields

---

###  Vector Field

A vector field assigns a vector (magnitude and direction) to each point.

- **Example**:  
  `F(x, y) = (-y, x)`  
  This creates circular flow around the origin.

- **Applications**:
  - Fluid dynamics
  - Magnetic/electric fields
  - Wind/force maps

---

###  Gradient (∇f)

The gradient of a scalar field points in the direction of steepest ascent.

- **Definition**:  
  `∇f = [∂f/∂x, ∂f/∂y]`

- **Example**:  
  For `f(x, y) = x^2 + y^2`  
  → `∇f = [2x, 2y]`

- **Applications**:
  - Image edge detection
  - Physics and mechanics
  - Machine learning (gradient descent)

---

###  Divergence (∇·F)

Divergence measures how much a vector field spreads out from a point.

- **Definition**:  
  `div F = ∂F_x/∂x + ∂F_y/∂y`

- **Interpretation**:
  - Positive: source (outflow)
  - Negative: sink (inflow)

- **Applications**:
  - Fluid dynamics
  - Electromagnetism
  - Continuity equations

---

###  Curl / Rotation (∇×F)

Curl measures the rotational tendency of a vector field at a point (in 2D, it's a scalar).

- **Definition (2D)**:  
  `curl F = ∂F_y/∂x - ∂F_x/∂y`

- **Interpretation**:
  - Positive: counter-clockwise spin
  - Negative: clockwise spin
  - Zero: conservative field

- **Applications**:
  - Fluid vorticity
  - Electromagnetic induction
  - Rotational physics

---
