using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class Exp : RuleBase<Exp>
{
    public override void Ejec()
    {
        Term.Instance!.Ejec();
        while (Ctx.CurrentToken().Tipo == TokensJava.OPA.Name()  || 
               Ctx.CurrentToken().Tipo == TokensJava.OPR.Name()  ||
               Ctx.CurrentToken().Tipo == TokensJava.OPL.Name()  || 
               Ctx.CurrentToken().Tipo == TokensJava.XOR.Name())
        {
            Avanzar();
            Term.Instance!.Ejec();
        }
    }
}