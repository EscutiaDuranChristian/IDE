using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class Type : RuleBase<Type>
{
    public override void Ejec()
    {
        if (Ctx.CurrentToken().Tipo == TokensJava.TM.Name())
            Avanzar(); 
        else Ty.Instance!.Ejec();
    }
}