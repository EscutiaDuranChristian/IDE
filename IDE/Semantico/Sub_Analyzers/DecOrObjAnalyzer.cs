using Compilador_Java.Errores;
using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Recopilar_Instrucciones;

namespace Compilador_Java.Semantico.Analyzers;
public class DecOrObjAnalyzer
{
    public static void Run(Instruccion curr, Instruccion next, string parent)
    {
        int tdId = curr.Tokens[0].IsTY() ? 0 : 1;
        string name = curr.Tokens[tdId+1].Lexema;
        var declaredType = curr.Tokens[tdId].TD2VAL();
        string td = declaredType?.Name() ?? curr.Tokens[0].Lexema;
        //Tipo de dato no encontrado
        if (declaredType == null && !SystemMethods.IDES.ContainsKey(td))
        {
            CompilerException.AddException("SEMANTIC ERROR",
                                           $"No se encontró ningún objeto o tipo de dato llamado {td}");
            return;
        }
        //Dejar sin asignar una variable local
        if (curr.Tokens.Count == 2 && !next.Message.Contains("DecGlob"))
        {
            CompilerException.AddException("SEMANTIC ERROR", "Declaración de variable local necesita un valor");
            return;
        }
        //Error de tipos duplicados
        if (SystemMethods.IDES.ContainsKey(name))
        {
            CompilerException.AddException(
                "SEMANTIC ERROR",
                $"La variable '{name}' ya fue declarada");
            return;
        }
        //Variable guerfana
        if (!SystemMethods.IDES.TryGetValue(parent, out var scope))
        {
            CompilerException.AddException(
                "SEMANTIC ERROR",
                $"No se encontró el ámbito '{parent}' para declarar la variable '{name}'");
            return;
        }

        scope.Relaciones.Add(name);
        SystemMethods.IDES.Add(name, new(td, new()));
        LinkVTSMethods.Link(td, name);

        if (curr.Tokens.Count > 2 && curr.Tokens[2].Tipo == TokensJava.ODA.Name())
        {
            var target = declaredType;
            if (target == null) return;

            var result = ExpressionTypeResolver.Resolve(curr.Tokens);
            if (result == null) return;

            if (!ExpressionTypeResolver.CanAssign(target.Value, result.Value))
                CompilerException.AddException("SEMANTIC ERROR",
                    $"No se puede asignar {result.Value.Name()} a {target.Value.Name()}");
        }
    }
}