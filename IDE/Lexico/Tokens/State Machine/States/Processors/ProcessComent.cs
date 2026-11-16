using Lexico.Token;
namespace Lexico.States;

public class ProcessComent : IState
{
    public static ProcessComent Instance = new();
    public void Process(ref LexerContext ctx, ref int i)
    {
        ctx.Word += ctx.Current;
        if (ctx.Current == '\n' && ctx.Word.Substring(0, 2) != "/*" ||
            $"{ctx.Previous}{ctx.Current}" == "*/")
        {
            ctx.State = TokState.Start;
            ctx.Word = "";
        }
    }
}