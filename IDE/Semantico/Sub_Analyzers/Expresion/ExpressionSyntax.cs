using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Lexico.Tokens.Table;

namespace Compilador_Java.Semantico.Analyzers;

internal static class ExpressionSyntax
{
    private static readonly HashSet<string> Operators = new()
    {
        "+", "-", "*", "/", "^", "~", "||", "&&", "<", ">", "<=", ">=", "==", "!="
    };

    public static bool IsOperator(string lexeme) => Operators.Contains(lexeme);

    public static int FindClosingParenthesis(List<TokenRow> tokens, int open, int to)
    {
        int depth = 0;
        for (int index = open; index < to; index++)
        {
            if (tokens[index].Tipo == TokensJava.PA.Name())
                depth++;
            else if (tokens[index].Tipo == TokensJava.PC.Name() && --depth == 0)
                return index;
        }
        return -1;
    }
}
