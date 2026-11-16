using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Recopilar_Instrucciones;
namespace Compilador_Java.Sintactico.GCL.Rules;
public class  For : RuleBase<For>
{
    public override void Ejec()
    {
        Expect(TokensJava.PRF.Name());
        RaiseDetectValidInst();
        Expect(TokensJava.PA.Name());
        RaiseDetectValidInst(INE.START_FOR_PART.Name());
        PrimerBloque();
        Condicion();
        Actualizacion();
        Expect(TokensJava.PC.Name());
        RaiseDetectValidInst(INE.END_FOR_PARTS.Name());
        Bloque.Instance!.Ejec();
    }
    public void PrimerBloque()
    {
        if (Ctx.CurrentToken().Tipo == TokensJava.TD.Name() ||
            Ctx.CurrentToken().Tipo == TokensJava.STR.Name() ||
            (Ctx.CurrentToken().Tipo == TokensJava.ID.Name() &&
            Ctx.At(1).Tipo == TokensJava.ID.Name()))
            DecOrObj.Instance!.Ejec();
        else if (Ctx.CurrentToken().Tipo == TokensJava.ID.Name() &&
            Ctx.At(1).Tipo == TokensJava.ODA.Name())
            Asig.Instance!.Ejec();
        else if (Ctx.CurrentToken().Tipo == TokensJava.FI.Name())
            Avanzar();
        else
        {
            Exp.Instance!.Ejec();
            Expect(TokensJava.FI.Name());
        }
        RaiseDetectValidInst(INE.FOR_PART.Name());
    }
    public void Condicion()
    {
        if (Ctx.CurrentToken().Tipo != TokensJava.FI.Name()) 
            Exp.Instance!.Ejec();
        Expect(TokensJava.FI.Name());
        RaiseDetectValidInst(INE.FOR_PART.Name());
    }
    public void Actualizacion()
    {
        if (Ctx.CurrentToken().Tipo != TokensJava.PC.Name())
        {
            if (Ctx.CurrentToken().Tipo == TokensJava.ID.Name() &&
                Ctx.At(1).Tipo == TokensJava.ODA.Name()) { 
                Avanzar(2); 
                Exp.Instance!.Ejec(); 
            }
            else Exp.Instance!.Ejec();
            RaiseDetectValidInst(INE.FOR_PART.Name());
        }
    }
}