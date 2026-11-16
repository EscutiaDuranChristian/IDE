using Compilador_Java.Lexico.Tokens.Table;
using Compilador_Java.Errores;
using Lexico.States;
using Lexico.Token;

namespace Compilador_Java.Lexico.Tokens;
public static class Tokenizer
{
    public static List<TokenRow>? TokenTable(string source)
    {
        LexerContext ctx = new();
        for (int i = 0; i < source.Length; i++)
        {
            ctx.Current = source[i];
            switch (ctx.State)
            {
                case TokState.Start:
                    ProcessStart.Instance.Process(ref ctx, ref i);
                    break;

                case TokState.Word:
                    ProcessWord.Instance.Process(ref ctx, ref i);
                    break;

                case TokState.Number:
                    ProcessNumber.Instance.Process(ref ctx, ref i);
                    break;

                case TokState.String:
                    ProcessStr.Instance.Process(ref ctx, ref i);
                    break;

                case TokState.Char:
                    ProcessChar.Instance.Process(ref ctx, ref i);
                    break;
                case TokState.Comment:
                    ProcessComent.Instance.Process(ref ctx, ref i);
                    break;
            }
            if(ctx.Update) ctx.Previous = ctx.Current;
            ctx.Update = true;
        }

        if (CompilerException.GetExceptions().Count > 0)
            return null;
        return ctx.Tokens;
    }
}