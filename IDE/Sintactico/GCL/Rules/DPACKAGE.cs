using Compilador_Java.Info_Lenguaje;

namespace Compilador_Java.Sintactico.GCL.Rules;

public class DPackage : RuleBase<DPackage>
{
    public override void Ejec()
    {
        if (Ctx.CurrentToken().Tipo == TokensJava.PCK.Name())
        {
            Expect(TokensJava.PCK.Name());
            Expect(TokensJava.ID.Name());
            Expect(TokensJava.FI.Name());
            RaiseDetectValidInst();
        }
    }
}