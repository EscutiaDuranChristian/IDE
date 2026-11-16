using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Recopilar_Instrucciones;

namespace Compilador_Java.Sintactico.GCL.Rules;
public class Switch : RuleBase<Switch>
{
    public override void Ejec()
    {
        Expect(TokensJava.PRSW.Name());
        Expect(TokensJava.PA.Name());
        Val.Instance!.Ejec();
        Expect(TokensJava.PC.Name());
        RaiseDetectValidInst();
        Expect(TokensJava.LLA.Name());
        RaiseDetectValidInst(INE.CASES_BEGIN.Name());
        CASE();
        C_DEF();
        Expect(TokensJava.LLC.Name());
        RaiseDetectValidInst(INE.CASES_END.Name());
    }
    public void CASE()
    {
        while (Ctx.CurrentToken().Tipo == TokensJava.CS.Name())                             
        {
            Avanzar();
            Val.Instance!.Ejec();
            Expect(TokensJava.DOP.Name());
            RaiseDetectValidInst(INE.CASE.Name());
            Cuerpo.Instance!.Ejec();
            Break.Instance!.Ejec();
        }
    }
    public void C_DEF()
    {
        if (Accept(TokensJava.DF.Name()))                               
        {
            Expect(TokensJava.DOP.Name());
            RaiseDetectValidInst(INE.CASE_DEFAULT.Name());
            Cuerpo.Instance!.Ejec(); 
            Break.Instance!.Ejec();
        }
    }
}