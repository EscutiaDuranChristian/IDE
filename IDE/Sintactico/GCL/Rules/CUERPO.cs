using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class Cuerpo : RuleBase<Cuerpo>
{
    public override void Ejec()
    {
        while (Ctx.CurrentToken().EmpiezaInst()) 
            Inst.Instance!.Ejec();
    }
}