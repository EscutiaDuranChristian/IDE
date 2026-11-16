using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class Inst : RuleBase<Inst>
{
    // INST::=DEC|STAT_B|ASIG|ACC|OBJ
    public override void Ejec()
    { 
        switch (Ctx.CurrentToken().Tipo)
        {
            case "TD":
            case "STR": DecOrObj.Instance!.Ejec(); break;
            case "ID": HandleIDCase(); break;
            case "VTS":
                Exp.Instance!.Ejec();
                Expect(TokensJava.FI.Name());
                RaiseDetectValidInst();
                break;             
            case "PRW": While.Instance!.Ejec(); break;
            case "PRIF": IfEl.Instance!.Ejec(); break;
            case "PRDO": Do.Instance!.Ejec(); break;
            case "PRSW": Switch.Instance!.Ejec(); break;
            case "PRF": For.Instance!.Ejec(); break;
            default: throw Error("una instrucción");
        }
    }
    public void HandleIDCase()
    {
        switch (Ctx.At(1).Tipo)
        {
            case "ID": DecOrObj.Instance!.Ejec(); break;
            case "ODA": Asig.Instance!.Ejec(); break;
            default:
                Exp.Instance!.Ejec();
                Expect(TokensJava.FI.Name());
                RaiseDetectValidInst();
                break;
        }
    }
}