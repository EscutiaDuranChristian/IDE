using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class Param : RuleBase<Param>
{
    public override void Ejec()
    {
        Ty.Instance!.Ejec(); 
        Expect(TokensJava.ID.Name()); 
    }
}