using Lexico.Token;
using Compilador_Java.Info_Lenguaje;

namespace Lexico.States;

public class ProcessStr : IState
{
    public static ProcessStr Instance => new();

    public void Process(ref LexerContext ctx, ref int i)
    {
        ctx.Word += ctx.Current;
        if (ctx.Current == '"')
        {
            ctx.Tokens.AddWord(ctx.Word, TokensJava.VTS.Name());
            ctx.Word = "";
            ctx.State = TokState.Start;
        }
    }
}