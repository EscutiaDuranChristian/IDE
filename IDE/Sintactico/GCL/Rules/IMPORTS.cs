using Compilador_Java.Info_Lenguaje;

namespace Compilador_Java.Sintactico.GCL.Rules;
public class Imports : RuleBase<Imports>
{
    public override void Ejec()
    {
        while (Ctx.CurrentToken().Tipo == TokensJava.IMP.Name())
        {
            Expect(TokensJava.IMP.Name());
            Expect(TokensJava.ID.Name());
            while(Accept(TokensJava.CA.Name()))
                Expect(TokensJava.ID.Name());
            Expect(TokensJava.FI.Name());
            RaiseDetectValidInst();
        }
    }
}