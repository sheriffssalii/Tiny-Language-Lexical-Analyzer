using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Compiler_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("Lexeme", "Lexeme");
            dataGridView1.Columns.Add("Token", "Token Type");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            string input = textBox1.Text;

            string keywordPattern = @"\b(int|float|string|read|write|repeat|until|if|elseif|else|then|return|endl|end|main)\b";
            string identifierPattern = @"[a-zA-Z][a-zA-Z0-9]*";
            string numberPattern = @"[0-9]+(\.[0-9]+)?";
            string stringPattern = "\"[^\"]*\"";
            string commentPattern = @"/\*[\s\S]*?\*/";
            string assignPattern = @":=";
            string arithPattern = @"[\+\-\*/]";
            string condPattern = @"(<|>|=|<>)";
            string boolPattern = @"(&&|\|\|)";
            string symbolPattern = @"[;,(){}]";

            string masterPattern =
                commentPattern + "|" +
                stringPattern + "|" +
                numberPattern + "|" +
                assignPattern + "|" +
                boolPattern + "|" +
                condPattern + "|" +
                arithPattern + "|" +
                symbolPattern + "|" +
                identifierPattern;

            MatchCollection matches = Regex.Matches(input, masterPattern);

            foreach (Match m in matches)
            {
                string lexeme = m.Value;
                string tokenType = "";

                if (Regex.IsMatch(lexeme, keywordPattern))
                    tokenType = "KEYWORD";

                else if (Regex.IsMatch(lexeme, numberPattern))
                    tokenType = "NUMBER";

                else if (Regex.IsMatch(lexeme, stringPattern))
                    tokenType = "STRING";

                else if (Regex.IsMatch(lexeme, commentPattern))
                    tokenType = "COMMENT";

                else if (Regex.IsMatch(lexeme, assignPattern))
                    tokenType = "ASSIGN_OP";

                else if (Regex.IsMatch(lexeme, arithPattern))
                    tokenType = "ARITH_OP";

                else if (Regex.IsMatch(lexeme, condPattern))
                    tokenType = "COND_OP";

                else if (Regex.IsMatch(lexeme, boolPattern))
                    tokenType = "BOOL_OP";

                else if (Regex.IsMatch(lexeme, symbolPattern))
                    tokenType = "SYMBOL";

                else if (Regex.IsMatch(lexeme, identifierPattern))
                    tokenType = "IDENTIFIER";

                else
                    tokenType = "ERROR";

                dataGridView1.Rows.Add(lexeme, tokenType);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


