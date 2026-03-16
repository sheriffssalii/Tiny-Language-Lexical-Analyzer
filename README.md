# 🔍 Tiny Language — Lexical Analyzer (Scanner)

> Phase 1 of the CS321 Compiler Design & Theory course  
> El-Shorouk Academy — Faculty of Computer Science

---

## 📖 About

This project implements a **Lexical Analyzer (Scanner)** for the **Tiny Language**. The scanner reads Tiny Language source code and breaks it down into tokens — the basic building blocks that a compiler works with.

Each token consists of:
- **Lexeme** — the actual text found in the code
- **Token Type** — its category (e.g., `KEYWORD`, `NUMBER`, `IDENTIFIER`)

---

## 👥 Team Members

| # | Full Name | Academic ID |
|---|-----------|-------------|
| 1 | Sherif Ali Mohamed | 324250019 |
| 2 | Sara Farwiez Mohamed | 324260108 |
| 3 | Shahd Mostafa Gadallah | 324240209 |

- **Team Leader:** Sherif Ali Mohamed 
- **Teaching Assistant:** Rania Ahmed  
- **Course:** CS321 – Compiler Design & Theory

---

## 🛠️ Built With

| Tool | Details |
|------|---------|
| Language | C# |
| IDE | Microsoft Visual Studio |
| UI Framework | Windows Forms (.NET) |
| Approach | Regular Expressions (`System.Text.RegularExpressions`) |

---

## ⚙️ How It Works

The scanner uses a **master Regex pattern** that combines all token patterns with the `|` (OR) operator. It calls `Regex.Matches()` once on the entire input and classifies each match into its token type.

### Pattern Priority Order
1. Comments `/* ... */`
2. String literals `" ... "`
3. Float numbers (e.g. `5.3`)
4. Integer numbers (e.g. `10`)
5. Assignment operator `:=`
6. Not-equal operator `<>`
7. Boolean operators `&&` `||`
8. Single-char operators `+ - * / < > =`
9. Symbols `; , ( ) { }`
10. Identifiers & Keywords
11. Error — any unrecognized character

> Keywords and identifiers share the same Regex pattern. The scanner first matches any word as an identifier, then checks against the keyword list to reclassify if needed.

---

## 🏷️ Token Types

| Token Type | Examples |
|------------|---------|
| `KEYWORD` | `int`, `if`, `write`, `repeat`, `return` |
| `IDENTIFIER` | `x`, `counter`, `val` |
| `NUMBER` | `123`, `0.23`, `5.3` |
| `STRING` | `"Hello"`, `"world"` |
| `COMMENT` | `/* a comment */` |
| `ASSIGN_OP` | `:=` |
| `ARITH_OP` | `+`, `-`, `*`, `/` |
| `COND_OP` | `<`, `>`, `=`, `<>` |
| `BOOL_OP` | `&&`, `\|\|` |
| `SYMBOL` | `;`, `,`, `(`, `)`, `{`, `}` |
| `ERROR` | `@`, `#`, `$` |

---

## 🖥️ Application UI

The application is a **Windows Forms** app with:
- **TextBox (multiline)** — where the user types or pastes Tiny Language source code
- **Button (Run)** — starts the scanning process
- **DataGridView** — displays the token table: `Lexeme | Token Type`

---

## 🧪 Sample Input & Output

**Input:**
```
int x;
read x;
/* check value */
if x > 0 then
  write x;
end
return 0;
```

**Output:**

| # | Lexeme | Token Type |
|---|--------|------------|
| 1 | `int` | KEYWORD |
| 2 | `x` | IDENTIFIER |
| 3 | `;` | SYMBOL |
| 4 | `read` | KEYWORD |
| 5 | `x` | IDENTIFIER |
| 6 | `;` | SYMBOL |
| 7 | `/* check value */` | COMMENT |
| 8 | `if` | KEYWORD |
| 9 | `x` | IDENTIFIER |
| 10 | `>` | COND_OP |
| 11 | `0` | NUMBER |
| 12 | `then` | KEYWORD |
| 13 | `write` | KEYWORD |
| 14 | `x` | IDENTIFIER |
| 15 | `;` | SYMBOL |
| 16 | `end` | KEYWORD |
| 17 | `return` | KEYWORD |
| 18 | `0` | NUMBER |
| 19 | `;` | SYMBOL |

---

## 📁 Project Structure

```
Tiny-Language-Lexical-Analyzer/
│
├── Compiler_Project/
│   ├── Form1.cs              # Main scanner logic
│   ├── Form1.Designer.cs     # UI layout
│   ├── *.csproj              # Project file
│   └── ...
│
├── Compiler_Project.sln      # Visual Studio solution file
├── .gitignore
└── README.md
```

---

## 🚀 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/sheriffssalii/Tiny-Language-Lexical-Analyzer.git
   ```
2. Open `Compiler_Project.sln` in **Visual Studio**
3. Build and run the project (`F5`)
4. Type or paste Tiny Language code into the text box
5. Click **Run** to see the token table

---

## 📚 Grammar Rules Covered

The scanner covers all **31 grammar rules** of the Tiny Language, including:
- Variable declarations, assignments, and expressions
- `if / elseif / else` conditional statements
- `repeat / until` loops
- `read` and `write` statements
- Function declarations and calls
- Block comments and string literals
- Full program structure with `main()`
