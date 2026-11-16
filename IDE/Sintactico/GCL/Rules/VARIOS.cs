using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Sintactico.GCL.Context;

namespace Compilador_Java.Sintactico.GCL.Rules;
public class Varios : RuleBase<Varios>
{
    public override void Ejec()
    {
        if (Ctx.At(0).Tipo == TokensJava.MA.Name() &&
            Ctx.At(1).Tipo == TokensJava.ST.Name() && 
            Ctx.At(2).Tipo == TokensJava.TM.Name() && 
            Ctx.At(3).Tipo == TokensJava.PMAIN.Name()) 
            PMain.Instance!.Ejec();
        else if (Ctx.EsMetodo()) Metodo.Instance!.Ejec();
        else DecGlob.Instance!.Ejec();
    }
}