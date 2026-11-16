using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class Asig : RuleBase<Asig>
{
    public override void Ejec()
    {
        Expect(TokensJava.ID.Name()); 
        Expect(TokensJava.ODA.Name()); 
        Exp.Instance!.Ejec(); 
        Expect(TokensJava.FI.Name());
        RaiseDetectValidInst();
    }
}