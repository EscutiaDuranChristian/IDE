using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Recopilar_Instrucciones;

namespace Compilador_Java.Semantico.Analyzers;

public class MethodAnalyzer
{
    public static string Run(Instruccion instruccion, string classname)
    {
        //Info Method
        var tokens = instruccion.Tokens;
        int parametersStart = tokens.FindIndex(token => token.Tipo == TokensJava.PA.Name());
        string idMethod = tokens.Take(parametersStart)
            .First(token => token.Tipo == TokensJava.ID.Name()).Lexema;
        var returnToken = tokens.Take(parametersStart)
            .First(token => token.Tipo == TokensJava.TM.Name() || token.IsTY());
        string returnType = returnToken.Tipo == TokensJava.TM.Name()
            ? TokensJava.TM.Name()
            : returnToken.TD2VAL()?.Name() ?? returnToken.Lexema;

        //Parameters
        var parameterTypes = new List<string>();
        var parameters = new List<(string Name, string Type)>();
        int parametersEnd = tokens.FindIndex(parametersStart + 1,
            token => token.Tipo == TokensJava.PC.Name());

        for (int index = parametersStart + 1; index < parametersEnd;)
        {
            if (tokens[index].Tipo == TokensJava.CO.Name())
            {
                index++;
                continue;
            }

            var typeToken = tokens[index];
            if (!typeToken.IsTY() || index + 1 >= parametersEnd ||
                tokens[index + 1].Tipo != TokensJava.ID.Name())
            {
                index++;
                continue;
            }

            string parameterType = typeToken.TD2VAL()?.Name() ?? typeToken.Lexema;
            string parameterName = tokens[index + 1].Lexema;
            parameterTypes.Add(parameterType);
            parameters.Add((parameterName, parameterType));
            index += 2;
        }

        SystemMethods.IDES.Add(idMethod,
            new Method(returnType, new(), parameterTypes));
        SystemMethods.IDES[classname].Relaciones.Add(idMethod);

        foreach (var (parameterName, parameterType) in parameters)
        {
            SystemMethods.IDES[idMethod].Relaciones.Add(parameterName);
            SystemMethods.IDES.TryAdd(parameterName, new(parameterType, new()));
        }

        return idMethod;
    }
}
