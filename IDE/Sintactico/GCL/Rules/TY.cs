using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;

public class Ty : RuleBase<Ty>
{
    public override void Ejec()
    {
        if (!Ctx.CurrentToken().IsTY()) 
            throw Error("un tipo (ID, TD o STR)");
        Avanzar();
    }
}