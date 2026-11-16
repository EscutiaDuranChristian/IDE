using Compilador_Java.Errores;
using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Lexico.Tokens.Table;

namespace Compilador_Java.Semantico.Analyzers;

internal static class ExpressionEvaluator
{
    public static TokensJava? Evaluate(List<TokenRow> expression) =>
        EvaluateRange(expression, 0, expression.Count);
    /// <summary>
    /// Evalua la expresion en un rango, si todo sale bien retorna el valor dominante
    /// </summary>
    /// <param name="tokens"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private static TokensJava? EvaluateRange(List<TokenRow> tokens, int from, int to)
    {
        var operands = new List<TokensJava>();
        var operators = new List<string>();
        TokensJava? pendingCast = null;
        int index = from;

        bool AddOperand(TokensJava type)
        {
            //Determinamos si se necesita castear
            if (pendingCast is TokensJava castType)
            {
                if (!ExpressionTypeRules.CanCast(type, castType))
                {
                    Error($"No se puede castear {type.Name()} a {castType.Name()}");
                    return false;
                }

                type = castType;
                pendingCast = null;
            }

            operands.Add(type);
            return true;
        }

        while (index < to)
        {
            TokenRow token = tokens[index];

            if (IsCast(tokens, index, to, out var castType)) //Controlar casteo
            {
                pendingCast = castType;
                index += 3;
            }
            else if (token.Tipo == TokensJava.PA.Name()) 
            {
                //Encontramos el enclosing parentesis para poder saltarnos a ese indice al terminar
                //la validación recursiva
                int close = ExpressionSyntax.FindClosingParenthesis(tokens, index, to);
                if (close < 0) return Error("Paréntesis sin cerrar");

                var innerType = EvaluateRange(tokens, index + 1, close);
                if (innerType == null || !AddOperand(innerType.Value)) return null;
                index = close + 1;
            }
            else if (token.Tipo == TokensJava.ID.Name()) //Controlar accesos
            {
                //Manejar acceso a algo que no existe
                if (!SystemMethods.IDES.TryGetValue(token.Lexema, out var symbol))
                    return Error($"No se encontró el identificador '{token.Lexema}'");

                bool isCall = index + 1 < to && tokens[index + 1].Tipo == TokensJava.PA.Name();
                if (isCall)
                {
                    int close = ExpressionSyntax.FindClosingParenthesis(tokens, index + 1, to);
                    if (close < 0) return Error("Paréntesis sin cerrar en la llamada");

                    bool continuesMemberAccess = close + 1 < to &&
                                                 tokens[close + 1].Tipo == TokensJava.CA.Name();
                    if (!continuesMemberAccess &&
                        ExpressionTypeRules.TryGetValueType(symbol.Type, out var returnType) &&
                        !AddOperand(returnType))
                        return null;

                    index = close + 1;
                }
                else
                {
                    bool continuesMemberAccess = index + 1 < to &&
                                                 tokens[index + 1].Tipo == TokensJava.CA.Name();
                    if (!continuesMemberAccess &&
                        ExpressionTypeRules.TryGetValueType(symbol.Type, out var valueType) &&
                        !AddOperand(valueType))
                        return null;
                    index++;
                }
            }
            else if (ExpressionSyntax.IsOperator(token.Lexema))
            {
                operators.Add(token.Lexema);
                index++;
            }
            else if (Enum.TryParse<TokensJava>(token.Tipo, out var literalType) &&
                     ExpressionTypeRules.IsValueType(literalType)) //Extra
            {
                bool continuesMemberAccess = literalType == TokensJava.VTS && index + 1 < to &&
                                             tokens[index + 1].Tipo == TokensJava.CA.Name();
                if (!continuesMemberAccess && !AddOperand(literalType)) return null;
                index++;
            }
            else index++;
        }

        if (pendingCast != null) return Error("Casteo sin operando");
        return ExpressionTypeRules.Combine(operands, operators);
    }

    /// <summary>
    /// Valida que la estructura tenga la estructura: (char)
    /// </summary>
    /// <param name="tokens"></param>
    /// <param name="index"></param>
    /// <param name="to"></param>
    /// <param name="castType">El casteo que vas a realizar</param>
    /// <returns></returns>
    private static bool IsCast(List<TokenRow> tokens, int index, int to, out TokensJava castType)
    {
        castType = default;

        if (tokens[index].Tipo != TokensJava.PA.Name() || 
            index + 2 >= to ||
            tokens[index + 1].TD2VAL() is not TokensJava resolvedType ||
            tokens[index + 2].Tipo != TokensJava.PC.Name())
            return false;

        castType = resolvedType;
        return true;
    }

    private static TokensJava? Error(string message)
    {
        CompilerException.AddException("SEMANTIC ERROR", message);
        return null;
    }
}
