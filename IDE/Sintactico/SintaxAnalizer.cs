using Compilador_Java.Sintactico.Result;
using Compilador_Java.Lexico.Simbolos;
using Compilador_Java.Sintactico.GCL.Rules;
using Compilador_Java.Sintactico.GCL;
using Compilador_Java.Errores;

namespace Compilador_Java.Sintactico;

public static class SyntaxAnalyzer
{
    public static SyntaxResult Analizar(List<Symbol> tabla)
    {
        if (Java.Instance == null) InicializarGramatica.Start();
        RuleBase<Java>.SetContext(new(tabla));
        try
        {
            Java.Instance!.Ejec();
            return new SyntaxResult { Ok = true, Position = -1, Message = "Sintaxis correcta." };
        }
        catch (SyntaxException e)
        {
            CompilerException.AddException("INVALID SYNTAX", e.Message);
            return new SyntaxResult { Ok = false, Position = e.Position, Message = e.Message };
        }
    }
}