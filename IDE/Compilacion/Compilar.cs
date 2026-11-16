using Compilador_Java.Depuracion;
using Compilador_Java.Lexico.Simbolos;
using Compilador_Java.Lexico.Tokens;
using Compilador_Java.Errores;
using Compilador_Java.Sintactico;
using DI = Compilador_Java.Recopilar_Instrucciones.DetectorInstrucciones;
using Compilador_Java.Semantico;
using Compilador_Java.Recopilar_Instrucciones;
namespace Compilador_Java.Compilacion;
public class Compilar
{
    public static (bool, List<Symbol>?, List<Instruccion>?) CompilarCodigo(string source)
    {
        new DI();
        Depurar.Limpiar(ref source);
        if (!Depurar.ValidarEsCodigoJava(source))
        {
            CompilerException.AddException("BAD SOURCE");
            return (false, null, null);
        }
        var tokens = Tokenizer.TokenTable(source);
        if(tokens == null) return (false, null, null);
        var symbols = Symbolize.SymbolTable(tokens);
        var retSymbols = new List<Symbol>(symbols);
        SyntaxAnalyzer.Analizar(symbols);
        if (CompilerException.HasExceptions()) return (false, retSymbols, null);
        SemanticAnalyzer.Start();
        var instructions = DI.Instance!.Instrucciones;
        if (CompilerException.HasExceptions())
            return (false, retSymbols, instructions);
        return (true, retSymbols, instructions);
    }
}