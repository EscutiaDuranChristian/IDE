using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Recopilar_Instrucciones;

namespace Compilador_Java.Sintactico.GCL.Rules;

public class  Clase : RuleBase<Clase>
{
    public override void Ejec()
    {
        Accept(TokensJava.MA.Name());
        Expect(TokensJava.CL.Name());
        Expect(TokensJava.ID.Name());
        RaiseDetectValidInst();
        Expect(TokensJava.LLA.Name());
        RaiseDetectValidInst(INE.CONTENT_BEGIN.Name());
        while (!Ctx.Eof && Ctx.CurrentToken().Tipo != TokensJava.LLC.Name())
            Varios.Instance!.Ejec();
        Expect(TokensJava.LLC.Name());
        RaiseDetectValidInst(INE.CONTENT_END.Name());
    }
}