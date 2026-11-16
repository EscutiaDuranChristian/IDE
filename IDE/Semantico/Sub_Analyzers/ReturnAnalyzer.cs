using Compilador_Java.Errores;
using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Lexico.Tokens.Table;
using Compilador_Java.Recopilar_Instrucciones;

namespace Compilador_Java.Semantico.Analyzers;

public static class ReturnAnalyzer
{
    public static void Run(Instruccion instruction, string methodName)
    {
        if (!SystemMethods.IDES.TryGetValue(methodName, out var method))
        {
            CompilerException.AddException("SEMANTIC ERROR",
                $"No se encontró el método '{methodName}' para validar su retorno");
            return;
        }

        int returnIndex = instruction.Tokens.FindIndex(
            token => token.Tipo == TokensJava.RTN.Name());
        if (returnIndex < 0)
        {
            CompilerException.AddException("SEMANTIC ERROR",
                "No se encontró la palabra clave return en la instrucción de retorno");
            return;
        }

        bool hasExpression = instruction.Tokens
            .Skip(returnIndex + 1)
            .TakeWhile(token => token.Tipo != TokensJava.FI.Name())
            .Any();

        if (!hasExpression)
        {
            if (method.Type != TokensJava.TM.Name())
                CompilerException.AddException("SEMANTIC ERROR",
                    $"El método '{methodName}' debe retornar un valor de tipo {method.Type}");
            return;
        }

        if (method.Type == TokensJava.TM.Name())
        {
            CompilerException.AddException("SEMANTIC ERROR",
                $"El método void '{methodName}' no puede retornar un valor");
            return;
        }

        if (!Enum.TryParse<TokensJava>(method.Type, out var expectedType))
        {
            CompilerException.AddException("SEMANTIC ERROR",
                $"No se puede validar el tipo de retorno '{method.Type}' del método '{methodName}'");
            return;
        }

        var actualType = ExpressionTypeResolver.Resolve(instruction.Tokens);
        if (actualType == null) return;

        if (!ExpressionTypeResolver.CanAssign(expectedType, actualType.Value))
            CompilerException.AddException("SEMANTIC ERROR",
                $"El método '{methodName}' debe retornar {expectedType.Name()}, pero la expresión es {actualType.Value.Name()}");
    }
}
