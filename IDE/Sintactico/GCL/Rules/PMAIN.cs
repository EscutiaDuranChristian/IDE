using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class PMain : RuleBase<PMain>
{
    public override void Ejec()
    {
        Expect(TokensJava.MA.Name());
        Expect(TokensJava.ST.Name()); 
        Expect(TokensJava.TM.Name());
        Expect(TokensJava.PMAIN.Name());
        Expect(TokensJava.PA.Name());
        Expect(TokensJava.STR.Name());
        if (Ctx.CurrentToken().Tipo == TokensJava.ID.Name()) { 
            Expect(TokensJava.ID.Name());
            Expect(TokensJava.COA.Name());
            Expect(TokensJava.COC.Name()); 
        }
        else { 
            Expect(TokensJava.COA.Name()); 
            Expect(TokensJava.COC.Name()); 
            Expect(TokensJava.ID.Name()); 
        }
        Expect(TokensJava.PC.Name());
        RaiseDetectValidInst();
        Bloque.Instance!.Ejec();
    }
}