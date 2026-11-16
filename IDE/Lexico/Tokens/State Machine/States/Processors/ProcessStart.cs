using Compilador_Java.Errores;
using Lexico.Token;
using Compilador_Java.Info_Lenguaje;
namespace Lexico.States;
public class ProcessStart : IState
{
    public static ProcessStart Instance => new();
    public void Process(ref LexerContext ctx, ref int i)
    {
        if (new List<char>() { '\n', '\r', '\t', ' '}.Contains(ctx.Current))
            return;
        if ($"{ctx.Previous}{ctx.Current}" == "//" || 
            $"{ctx.Previous}{ctx.Current}" == "/*")
        {
            ctx.Tokens.RemoveAt(ctx.Tokens.Count - 1);
            ctx.Word += $"{ctx.Previous}{ctx.Current}";
            ctx.Previous = ' ';
            ctx.Update = false;
            ctx.State = TokState.Comment;
        }
        else if (Reservadas.Map.ContainsKey($"{ctx.Previous}{ctx.Current}"))
        {
            ctx.Tokens.RemoveAt(ctx.Tokens.Count - 1);
            ctx.Tokens.AddToken($"{ctx.Previous}{ctx.Current}");
            ctx.Previous = ' ';
            ctx.Update = false;
        }
        else  if (Reservadas.Map.ContainsKey(ctx.Current + ""))
            ctx.Tokens.AddToken(ctx.Current + "");
        else
        {
            if (Char.IsLetter(ctx.Current) || ctx.Current == '_')
                ctx.State = TokState.Word;
            else if (Char.IsDigit(ctx.Current)) ctx.State = TokState.Number;
            else if (ctx.Current == '"') ctx.State = TokState.String;
            else if (ctx.Current == '\'') ctx.State = TokState.Char;
            else CompilerException.AddException("NO TOKEN");
            ctx.Word += ctx.Current;
        }
    }
}