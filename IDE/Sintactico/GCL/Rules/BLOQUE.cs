using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Recopilar_Instrucciones;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class Bloque : RuleBase<Bloque>
{
    public override void Ejec()
    {
        Expect(TokensJava.LLA.Name());
        RaiseDetectValidInst(INE.BLOQUE_BEGIN.Name());
        Cuerpo.Instance!.Ejec();
        Expect(TokensJava.LLC.Name());
        RaiseDetectValidInst(INE.BLOQUE_END.Name());
    }
}