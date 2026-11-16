using Compilador_Java.Errores;
using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Lexico.Tokens.Table;

namespace Compilador_Java.Semantico.Analyzers;

public static class ExpressionTypeResolver
{
    public static bool CanAssign(TokensJava target, TokensJava source) =>
        ExpressionTypeRules.CanAssign(target, source);

    public static TokensJava? Resolve(List<TokenRow> tokens)
    {
        //Sacar el indice del = o RTN
        int markerIndex = tokens.FindIndex(token =>
            token.Tipo == TokensJava.ODA.Name() || token.Tipo == TokensJava.RTN.Name());
        if (markerIndex < 0) return null;

        //Obtenemos el tipo de marcador si es = o rtn
        string marker = tokens[markerIndex].Tipo;
        //obtenemos una nueva lista a partir de el indice + 1
        var expression = tokens.Skip(markerIndex + 1)
                               .TakeWhile(token => token.Tipo != TokensJava.FI.Name())
                               .ToList();

        if (expression.Count == 0)
        {
            string markerText = marker == TokensJava.ODA.Name() ? "'='" : "'return'";
            CompilerException.AddException("SEMANTIC ERROR", $"Expresión vacía después de {markerText}");
            return null;
        }

        return ExpressionEvaluator.Evaluate(expression);
    }
}
