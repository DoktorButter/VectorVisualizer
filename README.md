# VectorVisualizer (SE_25_projekt)

Dieses Repository enthält das Softwareprojekt, das wir im Rahmen unserer Aufgabenstellung entwickelt haben.
Doxygen Doc: elaborate-frangollo-1581ce.netlify.app

---

# Inhaltsverzeichnis

1. [Zielsetzung](#zielsetzung)  
2. [Funktionen](#funktionen)  
3. [Theoretische Grundlagen](#theoretische-grundlagen)  
4. [Software-Design](#software-design)
5. [Anleitung zur Nutzun](#anleitung-zur-nutzung)
6. [LLMs](#llms)  
7. [Mitwirkende](#mitwirkende)
8. [Lizens](#lizens)

---

# Zielsetzung

Ziel dieser Software ist es, ein interaktives Werkzeug bereitzustellen, das sowohl die Visualisierung als auch die manuelle Berechnung von Gradient, Divergenz und Rotation (Curl) von Skalar- und Vektorfeldern in 2D und 3D ermöglicht – um ihr Verhalten verständlicher zu machen.

---

# Funktionen

- Interaktive Visualisierung von Skalar- und Vektorfeldern  
- Manuelle Implementierung von:
  - **Gradient** (∇f)
  - **Divergenz** (∇·F)
  - **Rotation / Curl** (∇×F)
- 2D- und 3D-Darstellung

---

# Theoretische Grundlagen

### Skalarfeld

Ein Skalarfeld ordnet jedem Punkt im Raum einen einzelnen Wert (Skalar) zu.

- **Beispiel**:  
  `f(x, y) = x^2 + y^2`  
  → ergibt eine schüsselförmige Oberfläche.

---

### Vektorfeld

Ein Vektorfeld weist jedem Punkt einen Vektor (Betrag und Richtung) zu.

- **Beispiel**:  
  `F(x, y) = (-y, x)`  
  → ergibt einen kreisförmigen Fluss um den Ursprung.

---

### Gradient (∇f)

Der Gradient eines Skalarfelds zeigt in die Richtung des stärksten Anstiegs.

- **Definition**:  
  `∇f = [∂f/∂x, ∂f/∂y]`

- **Beispiel**:  
  Für `f(x, y) = x^2 + y^2`  
  → `∇f = [2x, 2y]`

  In der Elektrostatik ergibt der negative Gradient des Potentials U das elektrische Feld E.

---

### Divergenz (∇·F)

Die Divergenz misst, wie stark ein Vektorfeld aus einem Punkt heraus strömt.

- **Definition**:  
  `div F = ∂F_x/∂x + ∂F_y/∂y`

- **Interpretation**:
  - Positiv: Quelle (Ausfluss)
  - Negativ: Senke (Einfluss)

---

### Rotation / Curl (∇×F)

Die Rotation beschreibt das Wirbelverhalten eines Vektorfeldes an einem Punkt (in 2D ist es ein Skalar).

- **Definition (2D)**:  
  `curl F = ∂F_y/∂x - ∂F_x/∂y`

- **Interpretation**:
  - Positiv: Drehung gegen den Uhrzeigersinn
  - Negativ: Drehung im Uhrzeigersinn
  - Null: konservatives Feld

---

# Software-Design

Diese Software besteht aus zwei Hauptkomponenten: einer Mathematikbibliothek und einer grafischen Benutzeroberfläche (GUI).

---

### Mathematik-Bibliothek (MathLib)

Diese Bibliothek enthält die Implementierung der folgenden Operationen:

- Gradientenberechnung  
- Divergenzberechnung  
- Rotationsberechnung

Der Gradientenrechner verwendet Skalarfelder, Divergenz und Rotation nutzen Vektorfelder – jeweils für 2D und 3D.

---

### Grafische Benutzeroberfläche (GUI)

Die GUI dient als Frontend und ermöglicht:

- Eingabe von Skalar- oder Vektorfeldern
- Auswahl gewünschter Operationen
- Visualisierung in 2D oder 3D
- Interaktive Bedienung (Zoom & Drehung)

---


# Anleitung zur Nutzung

## 1. Startbildschirm

Nach dem Start der Anwendung öffnet sich ein Auswahlfenster mit folgenden Optionen:

- **„Skalarfeld“** → Zur Eingabe und Visualisierung von Funktionen wie `f(x, y)` oder `f(x, y, z)`
- **„Vektorfeld“** → Zur Eingabe von `Fx(x, y)` und `Fy(x, y)` zur Darstellung eines 2D-Vektorfelds
- Zusätzlich kann über die Checkbox „**Ist die Funktion 3D?**“ zwischen 2D- und 3D-Modus umgeschaltet werden.

---

## Gültige mathematische Funktionen:

| Ausdruck            | Beispiel               |
|---------------------|------------------------|
| `+`, `-`, `*`, `/`  | `x*y + 3/z`            |
| `^`                 | `x^2`, `z^3`           |
| `Sqrt(x)`           | `Sqrt(x^2 + y^2)`      |
| `Sin(x)`            | `Sin(x + y)`           |
| `Cos(x)`            | `Cos(x)`               |
| `Tan(x)`            | `Tan(y)`               |
| `Exp(x)`            | `Exp(x + y)`           |
| `Log(x)`            | `Log(x^2)`             |
| `Abs(x)`            | `Abs(x - y)`           |
- Bei Funktionen wie **S**in -> den ersten Buchstaben **immer** groß schreiben
- **Multiplikation muss explizit angegeben werden**: `2x`  → `2*x`  
- Verwende **nur x, y [, z]** – keine anderen Variablen!
- Bei Syntaxfehlern oder nicht definierbaren Ausdrücken erfolgt eine Fehlermeldung.

## 2. Skalarfelder eingeben

- **2D-Beispiel**:  
  `f(x, y) = Sin(x) * y + x^2`

- **3D-Beispiel**:  
  `f(x, y, z) = x*y + z^2`

---

## 3. Vektorfelder eingeben

Gib die zwei Komponenten `Fx(x, y)` und `Fy(x, y)` separat ein. Beispiele:

- **Beispiel 1 (Rotation um Ursprung)**  
  - `Fx = -y`  
  - `Fy = x`  

- **Beispiel 2 (konservatives Feld)**  
  - `Fx = x`  
  - `Fy = y`

Optional kannst du einen Punkt `(x, y)` angeben, um Divergenz und Rotation an dieser Stelle zu berechnen und anzeigen zu lassen.

---

## 4. Visualisierung starten

- Klicke auf **„Visualisieren“**, um das Feld als Heatmap (Skalar) oder Pfeildiagramm (Vektor) darzustellen.
- Falls ein Punkt angegeben wurde, wird der **Gradient** (bei Skalarfeldern), die **Divergenz** oder **Rotation** (bei Vektorfeldern) zusätzlich berechnet und in einem Infofeld angezeigt.
- Außerdem wird zusätzlich eine interaktive Ansicht (Zoomen, Drehen) ermöglicht.

---

## 5. Fehlerbehandlung

- Bei fehlerhaften Eingaben erfolgt eine Rückmeldung (z. B. ungültige Funktion oder Punkt).
- Numerische Berechnungen (Gradient, Divergenz, Curl) erfolgen mit zentralem Differenzenquotienten.

---

# LLMs

| Einsatzzweck                       | Verwendetes Modell | Erfahrung / Bewertung |
|-----------------------------------|--------------------|------------------------|
| Anforderungsanalyse               | ChatGPT-4o         | funktionierte perfekt |
| Architektur / Designideen         | ChatGPT-4o         | oft hilfreich, aber manchmal unklar getrennt zwischen Logik & UI, oft überflüssige Inhalte |
| Codegenerierung                   | ChatGPT-4o         | Struktur gut, Details oft fehlerhaft |
| Testfallgenerierung               | ChatGPT-4o         | funktionierte fehlerfrei |
| Refactoring-Vorschläge            | ChatGPT-4o         | fehlerfrei, aber selten genutzt |
| Code Review                       | ChatGPT-4o         | sehr hilfreich zum Verstehen |
| Dokumentation (README etc.)       | ChatGPT-4o         | funktionierte gut, besonders gut, wenn ChatGPT eine Konzeptdatei gegeben wird |
| Fehlersuche / Debugginghilfe      | ChatGPT-4o         | nicht empfehlenswert, Fehler wurden durch neue ersetzt |
| Versionsverwaltung / Git          | ChatGPT-4o         | funktionierte fehlerfrei |
| Kommunikation (z. B. Committexte) | ChatGPT-4o         | nicht verwendet |
| Sonstiges                         | ChatGPT-4o         | – |

---

# Mitwirkende

In gemeinsamer Entwicklung durch:

### Kevin Seib (DoktorButter):  
Student der Angewandten Informatik  
→ Verantwortlich für die Entwicklung

### Pascal Gläser (PascalGl04):  
Student der Geophysik & Geoinformatik  
→ Verantwortlich für Koordination, Dokumentation, Beispiele und Tutorial. 
→ Unterstützung bei der Entwicklung

---
# Lizens

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
