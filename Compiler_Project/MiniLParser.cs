using System;
using System.Collections.Generic;

namespace Compiler_Project
{
    public class MiniLParser
    {
        private List<Token> tokens;
        private int index = 0;
        private Token currentToken;

        public MiniLParser(List<Token> tokens)
        {
            this.tokens = tokens;
            if (tokens.Count > 0)
                currentToken = tokens[index];
        }

        private void Match(string expected)
        {
            if (index < tokens.Count && (currentToken.Value == expected || currentToken.Type == expected))
            {
                index++;
                if (index < tokens.Count)
                    currentToken = tokens[index];
            }
            else
            {
                throw new Exception($"Expected '{expected}' but found '{currentToken?.Value}'");
            }
        }

        public void ParseProgram()
        {
            ParseStatements();
        }

        private void ParseStatements()
        {
            ParseStatement();
            if (index < tokens.Count && currentToken.Value == ";")
            {
                Match(";");
                if (index < tokens.Count && currentToken.Value != "}" && currentToken.Value != "until")
                    ParseStatements();
            }
        }

        private void ParseStatement()
        {
            if (index >= tokens.Count) return;

            if (currentToken.Value == "num" || currentToken.Value == "text")
                ParseDeclaration();
            else if (currentToken.Type == "IDENTIFIER")
                ParseAssignment();
            else if (currentToken.Value == "check")
                ParseCheck();
            else if (currentToken.Value == "repeat")
                ParseRepeat();
            else if (currentToken.Value == "{")
                ParseBlock();
            else
                throw new Exception($"A statement cannot start with '{currentToken.Value}' ({currentToken.Type})");
        }

        private void ParseDeclaration()
        {
            if (currentToken.Value == "num") Match("num");
            else Match("text");
            ParseAssignment();
        }

        private void ParseAssignment()
        {
            if (currentToken.Type != "IDENTIFIER")
                throw new Exception($"Cannot assign to '{currentToken.Value}'. Expected an Identifier.");
            Match("IDENTIFIER");
            Match(":=");
            ParseExpression();
        }

        private void ParseCheck()
        {
            Match("check");
            Match("(");
            ParseCondition();
            Match(")");
            ParseBlock();
            if (index < tokens.Count && currentToken.Value == "otherwise")
            {
                Match("otherwise");
                ParseBlock();
            }
        }

        private void ParseRepeat()
        {
            Match("repeat");
            ParseBlock();
            Match("until");
            Match("(");
            ParseCondition();
            Match(")");
        }

        private void ParseBlock()
        {
            if (currentToken.Value == "{")
            {
                Match("{");
                ParseStatements();
                Match("}");
            }
            else
            {
                ParseStatement();
            }
        }

        private void ParseCondition()
        {
            ParseExpression();
            if (currentToken.Value == "<" || currentToken.Value == ">" ||
                currentToken.Value == "==" || currentToken.Value == "!=")
                Match(currentToken.Value);
            else
                throw new Exception($"Expected a relational operator (<, >, ==, !=) but found '{currentToken?.Value}'");
            ParseExpression();
        }

        private void ParseExpression()
        {
            ParseTerm();
            while (index < tokens.Count && (currentToken.Value == "+" || currentToken.Value == "-"))
            {
                Match(currentToken.Value);
                ParseTerm();
            }
        }

        private void ParseTerm()
        {
            ParseFactor();
            while (index < tokens.Count && (currentToken.Value == "*" || currentToken.Value == "/"))
            {
                Match(currentToken.Value);
                ParseFactor();
            }
        }

        private void ParseFactor()
        {
            if (currentToken.Type == "IDENTIFIER")
                Match("IDENTIFIER");
            else if (currentToken.Type == "NUMBER")
                Match("NUMBER");
            else if (currentToken.Value == "(")
            {
                Match("(");
                ParseExpression();
                Match(")");
            }
            else
                throw new Exception($"Expected an Identifier or Number but found '{currentToken?.Value}'");
        }
    }
}