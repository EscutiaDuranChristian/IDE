using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Recopilar_Instrucciones;
namespace Compilador_Java.Sintactico.GCL.Rules;

public class IfEl : RuleBase<IfEl>
{
    // IFEL::=PRIF PA EXPBOO PC LLA INST LLC CLOS ;  CLOS::=PRE LLA INST LLC|
    public override void Ejec()
    {
        Expect(TokensJava.PRIF.Name());
        Expect(TokensJava.PA.Name());
        Exp.Instance!.Ejec(); 
        Expect(TokensJava.PC.Name());
        RaiseDetectValidInst(INE.IF.Name());
        Bloque.Instance!.Ejec();
        if (Accept(TokensJava.PRE.Name()))
        {
            Bloque.Instance!.Ejec();
            RaiseDetectValidInst(INE.ELSE.Name());
        }
    }
}