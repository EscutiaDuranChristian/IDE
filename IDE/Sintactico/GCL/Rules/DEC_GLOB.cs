using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Rules;

public class DecGlob : RuleBase<DecGlob>
{
    public override void Ejec()
    {
        Accept(TokensJava.MA.Name());
        DecOrObj.Instance!.Ejec();
        RaiseDetectValidInst();
    }
}