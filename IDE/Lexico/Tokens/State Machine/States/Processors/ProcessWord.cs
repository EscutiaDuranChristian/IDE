using Lexico.Token;
using Compilador_Java.Info_Lenguaje;

namespace Lexico.States;

public class ProcessWord : IState
{
    public static ProcessWord Instance = new();
    public void Process(ref LexerContext ctx, ref int i)
    {
        if (Char.IsLetterOrDigit(ctx.Current) || ctx.Current == '_') 
            ctx.Word += ctx.Current;
        else
        {
            string tipo = TokensJava.ID.Name();
            if (Reservadas.Map.ContainsKey(ctx.Word))
                tipo = Reservadas.Map[ctx.Word];
            ctx.Tokens.AddWord(ctx.Word, tipo);
            ctx.Word = "";
            ctx.State = TokState.Start;
            this.Reprocess(ref i, ctx);
        }
    }
}