using Compilador_Java.Errores;
using Compilador_Java.Info_Lenguaje;
using Lexico.Token;

namespace Lexico.States;

public class ProcessChar : IState
{
    public static ProcessChar Instance => new();

    public void Process(ref LexerContext ctx, ref int i)
    {
        ctx.Word += ctx.Current;
        if (ctx.Current == '\'')
        {
            if (ctx.Word.Length > 3){
                CompilerException.AddException("BAD CHAR");
                return;
            }
            ctx.Tokens.AddWord(ctx.Word, TokensJava.VTC.Name());
            ctx.Word = "";
            ctx.State = TokState.Start;
        }
    }
}