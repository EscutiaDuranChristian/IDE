using Compilador_Java.Lexico.Tokens.Table;
using Compilador_Java.Info_Lenguaje;

namespace Lexico.States;
public interface IState
{
    void Process(ref LexerContext ctx, ref int i);
}

public static class IStateExtention
{
    public static void Reprocess(this IState state, ref int i, LexerContext ctx)
    {
        i--;
        ctx.Update = false;
    }
    public static void AddToken(this List<TokenRow> l, string word)
    {
        if (Reservadas.Map.ContainsKey(word))
            l.Add(new(word, Reservadas.Map[word]));
    }
    public static void AddWord(this List<TokenRow> l, string word, string t = "ID") =>
        l.Add(new(word, t));
}