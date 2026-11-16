using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class While : RuleBase<While>
{
    public override void Ejec()
    {
        Expect(TokensJava.PRW.Name());
        Expect(TokensJava.PA.Name()); 
        Exp.Instance!.Ejec();
        Expect(TokensJava.PC.Name());
        RaiseDetectValidInst();
        Bloque.Instance!.Ejec();
    }
}