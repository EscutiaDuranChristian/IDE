using Compilador_Java.Info_Lenguaje;

namespace Compilador_Java.Sintactico.GCL.Rules;

/// <summary>
/// Decidi llamar EXPC como Term porque queda más claro.
/// Sus reglas de validación, decidí hacerlas subreglas de la clase
/// ya que solo en esta clase se usan, ademas parecen mas validacion de que 
/// termino usar.
/// </summary>
public class Term : RuleBase<Term>
{
    public override void Ejec()
    {
        string c = Ctx.CurrentToken().Tipo;
        if (EXPU(c) || EXP(c) || ACC(c)) 
            return; 
        Val.Instance!.Ejec();
    }
    public bool EXPU(string c)
    {
        if (c == TokensJava.INC.Name() ||
            c == TokensJava.DEC.Name() ||
            c == TokensJava.OPB.Name() ||
            c == TokensJava.OPN.Name())
        {

            Avanzar();
            Val.Instance!.Ejec();
            return true;
        }
        return false;
    }
    public bool EXP(string c)
    {
        if (c == TokensJava.PA.Name())
        {
            Avanzar();
            if(Ctx.CurrentToken().Tipo == TokensJava.TD.Name())
            {
                Avanzar();
                Expect(TokensJava.PC.Name());
                Instance!.Ejec();
                return true;
            }
            Exp.Instance!.Ejec();
            Expect(TokensJava.PC.Name()); 
            return true;
        }
        return false;
    }
    public bool ACC(string c)
    {
        if (c == TokensJava.ID.Name() || c == TokensJava.VTS.Name())
        {
            bool esId = c == TokensJava.ID.Name();
            Avanzar();
            OT();
            if (esId &&
                (Ctx.CurrentToken().Tipo == TokensJava.INC.Name() ||
                 Ctx.CurrentToken().Tipo == TokensJava.DEC.Name()))
                Avanzar(); // x++ / x-- (postfijo)
            return true;
        }
        return false;
    }
    public void OT()
    {
        while (Ctx.CurrentToken().Tipo == TokensJava.CA.Name())
        {
            Avanzar();
            Expect(TokensJava.ID.Name());
            if (Accept(TokensJava.PA.Name()))
            {
                Args.Instance!.Ejec();
                Expect(TokensJava.PC.Name());
            }
        }
    }
}