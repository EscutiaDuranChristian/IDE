using Compilador_Java.Info_Lenguaje;

namespace Compilador_Java.Sintactico.GCL.Rules;

public class Args : RuleBase<Args>
{
    public override void Ejec()
    {
        if (Ctx.CurrentToken().Tipo == TokensJava.PC.Name())
            return;
        Exp.Instance!.Ejec();
        while (Accept(TokensJava.CO.Name())) Exp.Instance!.Ejec();
    }
}