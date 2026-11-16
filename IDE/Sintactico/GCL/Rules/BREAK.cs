using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class Break : RuleBase<Break>
{
    public override void Ejec()
    {
        if (Accept(TokensJava.BR.Name())) 
            Expect(TokensJava.FI.Name());
        RaiseDetectValidInst();
    }
}