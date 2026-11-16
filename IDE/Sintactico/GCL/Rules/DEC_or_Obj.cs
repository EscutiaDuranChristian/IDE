using Compilador_Java.Info_Lenguaje;

namespace Compilador_Java.Sintactico.GCL.Rules;
public class DecOrObj : RuleBase<DecOrObj>
{
    public override void Ejec()
    {
        bool tipoId = Ctx.CurrentToken().Tipo == TokensJava.ID.Name();
        Ty.Instance!.Ejec(); 
        Expect(TokensJava.ID.Name());
        if (Accept(TokensJava.FI.Name())){ RaiseDetectValidInst(); return; }
        Expect(TokensJava.ODA.Name());
        if (Ctx.CurrentToken().Tipo == TokensJava.NW.Name())
        {
            if (!tipoId) throw Error("una expresión (NW solo aplica a tipos ID)");
            Avanzar();
            Expect(TokensJava.ID.Name()); 
            Expect(TokensJava.PA.Name());
            Args.Instance!.Ejec(); 
            Expect(TokensJava.PC.Name());
            Expect(TokensJava.FI.Name());
            RaiseDetectValidInst();
            return;
        }
        Exp.Instance!.Ejec();
        Expect(TokensJava.FI.Name());
        RaiseDetectValidInst();
    }
}