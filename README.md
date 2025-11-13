# 🏃‍♂️ Projeto QR Run — Sistema de Confirmação de Corrida

## 📋 Sobre o Projeto

O **QR Run** é um sistema simples e funcional criado para grupos de corrida.  
A ideia é facilitar o **registro de presença dos corredores** em eventos, utilizando um **QR Code** que leva diretamente para uma **landing page** onde o participante se cadastra informando **nome e CPF** para confirmar sua presença.

---

## 🚀 Como Funciona

1. O organizador gera um **QR Code** a partir do link hospedado (ex: GitHub Pages ou Azure).  
2. O participante aponta a câmera do celular para o QR Code.  
3. Ele é redirecionado para uma **página web (landing page)**.  
4. Na página, preenche **nome** e **CPF**.  
5. O sistema envia os dados para o **back-end em .NET**, que registra a confirmação.

---

## 🧩 Tecnologias Utilizadas

### 💻 Back-end
- **.NET 8 / ASP.NET Core**
- **C#**
- **Controllers REST**
- Armazenamento local (sem banco de dados externo)
- Hospedagem via GitHub Pages / Azure (link gerado para o QR Code)

### 🎨 Front-end
- **HTML5**
- **CSS3**
- Página responsiva e leve
- Layout simples e intuitivo para facilitar o registro rápido
