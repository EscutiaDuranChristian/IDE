using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Recopilar_Instrucciones;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class Do : RuleBase<Do>
{
    public override void Ejec()
    {
        Expect(TokensJava.PRDO.Name());
        RaiseDetectValidInst();
        Bloque.Instance!.Ejec();
        Expect(TokensJava.PRW.Name()); 
        Expect(TokensJava.PA.Name()); 
        RaiseDetectValidInst(INE.DO_WHILE.Name());
        Exp.Instance!.Ejec(); 
        Expect(TokensJava.PC.Name());
        Expect(TokensJava.FI.Name());
    }
}