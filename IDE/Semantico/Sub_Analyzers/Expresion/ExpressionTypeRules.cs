using Compilador_Java.Errores;
using Compilador_Java.Info_Lenguaje;

namespace Compilador_Java.Semantico.Analyzers;

internal static class ExpressionTypeRules
{
    public static bool CanAssign(TokensJava target, TokensJava source)
    {
        if (target == source) return true;

        return target switch
        {
            TokensJava.VTD => source is TokensJava.VTE or TokensJava.VTF or TokensJava.VTC,
            TokensJava.VTF => source is TokensJava.VTE or TokensJava.VTC,
            TokensJava.VTE => source is TokensJava.VTC,
            _ => false
        };
    }

    public static bool CanCast(TokensJava source, TokensJava target) =>
        source == target || (TokenHelper.NumericTypes.Contains(source) && 
                             TokenHelper.NumericTypes.Contains(target));

    public static bool IsValueType(TokensJava type) => TokenHelper.ValueTypes.Contains(type);

    public static bool TryGetValueType(string typeName, out TokensJava type) =>
        Enum.TryParse(typeName, out type) && IsValueType(type);

    public static TokensJava? Combine(List<TokensJava> operands, List<string> operators)
    {
        if (operands.Count == 0) return Error("No se encontró ningún operando");

        if (operators.Any(op => OperatorCompatibility.Accept(op, TokensJava.VTB.Name()) ||
                                op is "==" or "!="))
            return TokensJava.VTB;

        if (operands.Count == 1) return operands[0];

        if (operands.Contains(TokensJava.VTB))
            return Error("No se pueden aplicar operadores aritméticos a un booleano");

        if (operands.Contains(TokensJava.VTS))
        {
            string? invalidStringOperator = operators.FirstOrDefault(
                op => !OperatorCompatibility.Accept(op, TokensJava.VTS.Name()));
            return invalidStringOperator == null
                ? TokensJava.VTS
                : Error($"El operador '{invalidStringOperator}' no es válido con cadenas");
        }

        string? invalidNumericOperator = operators.FirstOrDefault(
            op => !OperatorCompatibility.Accept(op, TokensJava.VTE.Name()));

        if (invalidNumericOperator != null)
            return Error($"Operador '{invalidNumericOperator}' no válido en la expresión");

        if (operands.Contains(TokensJava.VTD)) return TokensJava.VTD;
        if (operands.Contains(TokensJava.VTF)) return TokensJava.VTF;
        if (operands.Contains(TokensJava.VTE)) return TokensJava.VTE;

        return TokensJava.VTE;
    }

    private static TokensJava? Error(string message)
    {
        CompilerException.AddException("SEMANTIC ERROR", message);
        return null;
    }
}
