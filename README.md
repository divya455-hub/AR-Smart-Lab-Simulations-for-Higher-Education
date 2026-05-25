# Augmented Reality Based Smart Lab Simulations for Higher Education

An immersive Augmented Reality (AR) application built with Unity and AR Foundation designed to enhance the laboratory learning experience for first-year college engineering and science students. This project addresses traditional lab challenges like limited access, equipment shortages, high maintenance costs, and safety risks by providing a safe, resource-efficient digital alternative.

---

## 📺 Project Demo & Visuals
* 🔗 **[Click Here to Watch the Full Demo Video](https://drive.google.com/file/d/1YW0b1w5m-KRvxUY__VCEwv4auEHlycrl/view?usp=sharing)**
<img width="500" height="500" alt="Screenshot 2026-02-24 102623" src="https://github.com/user-attachments/assets/2f08931b-4477-4da8-9589-687727b6ab2f" />
<img width="500" height="500" alt="Screenshot 2026-02-22 214729" src="https://github.com/user-attachments/assets/432a8341-0277-48f7-821b-90a535adef84" />
<img width="500" height="500" alt="Screenshot 2026-02-24 105037" src="https://github.com/user-attachments/assets/3a547202-3ff2-40ee-b360-81304baabdfa" />




---

## 🔬 Core Experiment Modules

### 🔹 Physics Module
* **Viscosity Measurement:** Measures the coefficient of viscosity using Poiseuille’s method by simulating liquid flow through a virtual capillary tube.
* **Simple Pendulum Analysis:** Real-time calculation of time period and acceleration due to gravity ($g$) by dynamically varying the pendulum length.
* **Laser Grating Experiment:** Visualizes light diffraction and interference fringe patterns by adjusting grating lines.
* **UJT (Unijunction Transistor) & PN Junction Diode:** Virtual circuit interaction to vary emitter voltage, trace I-V characteristic curves, and identify negative resistance regions.
* **Optical Fiber Demonstration:** Simulates light propagation and total internal reflection properties.

### 🧪 Chemistry Module
* **Water Hardness Analysis (EDTA Test):** Features real-time chemical reaction color changes from wine red to blue at the endpoint, displaying hardness values in ppm.
* **Iron Estimation:** Simulates potassium thiocyanate reactions to form red-colored complexes, calculating iron concentrations automatically.
* **Acid-Base Titration:** Interactive volumetric analysis with visual color cues.
* **Alkalinity & Chloride Content Estimation:** Full step-by-step interactive titration workflows.

---

## 📊 Key Highlights & Results
* **Accurate Simulations:** Replicates laboratory procedures using physical colliders, real-time math calculations, sliders, UI feedback, and custom animations.
* **Mobile-First Accessibility:** Designed to run offline on standard smartphones without requiring internet access or expensive equipment.
* **Enhanced Learning Outcomes:** Delivers realistic visual cues and instant graphical outputs that significantly improve student engagement and retention.

---

## 🛠️ System Specifications

### 💻 Software Architecture
* **Game Engine:** Unity (2022.3.9f1)
* **AR Framework:** AR Foundation (5.x), Google ARCore XR Plug-in Management
* **IDE:** Visual Studio 2022
* **Language:** C#
* **Version Control:** Git with Git Large File Storage (LFS)
* **Target Build Toolchain:** Android SDK & NDK, JDK (Java Development Kit)

### ⚙️ Minimum Hardware Requirements
* **Development Machine:** AMD Ryzen 5 / Intel Core i5 | 16 GB RAM minimum (32 GB recommended) | 512 GB SSD.
* **Testing Mobile Device:** Android 8.0 (Oreo) or later | ARCore supported smartphone | 4 GB RAM | 12 MP Camera.

---

## 📁 Repository Clean Architecture
This repository contains strictly clean metadata configurations, omitting bloated library files:
* `Assets/` — Custom scenes, 3D models, textures, animations, and C# interaction scripts.
* `Packages/` — Package manifests managing AR Foundation dependencies.
* `ProjectSettings/` — Device input configurations, physics, and graphics layer settings.

---


