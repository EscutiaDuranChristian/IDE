using Compilador_Java.Errores;
using Lexico.Token;
using Compilador_Java.Info_Lenguaje;

namespace Lexico.States;
public class ProcessNumber : IState
{
    public static ProcessNumber Instance => new();
    private static void CompleteDec(ref LexerContext ctx)
    {
        if (ctx.Word == "") return;
        if (ctx.Tokens.Count > 0 && ctx.Tokens.Last().Tipo == "CA")
        {
            ctx.Tokens.RemoveAt(ctx.Tokens.Count - 1);
            ctx.Word = "0." + ctx.Word;
        }
        else if (ctx.Word.Last() == '.')
            ctx.Word = ctx.Word + "0";
    }
    public void Process(ref LexerContext ctx, ref int i)
    {
        if (Char.IsDigit(ctx.Current)) ctx.Word += ctx.Current;
        else if (ctx.Current == '.')
        {
            if (ctx.Word.Contains('.'))
                CompilerException.AddException("BAD VALUE");
            ctx.Word += ctx.Current;
        }
        else if (TokenHelper.HasPosfix(ctx.Current)) ctx.Word += ctx.Current;
        else if (Char.IsLetter(ctx.Current) || ctx.Current == '_')
            CompilerException.AddException("BAD ID");
        else
        {
            string tipo = TokensJava.VTE.Name();
            CompleteDec(ref ctx);
            if (ctx.Word.Contains('.')) tipo = TokensJava.VTD.Name();
            if (TokenHelper.HasPosfix(ctx.Word.Last()))
                tipo = TokenHelper.Posfix[ctx.Word.Last()];
            ctx.Tokens.AddWord(ctx.Word, tipo);
            ctx.Word = "";
            ctx.State = TokState.Start;
            this.Reprocess(ref i, ctx);
        }
    }
}