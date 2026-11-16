using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Recopilar_Instrucciones;

namespace Compilador_Java.Semantico.Analyzers;
public class ClassAnalyzer
{
    public static string Run(Instruccion ins)
    {
        int i = 1;
        if(ins.Tokens.Count() == 3) i = 2;
        string name = ins.Tokens[i].Lexema;
        SystemMethods.IDES.Add(name, new(name, new()));
        return ins.Tokens[i].Lexema;
    }
}