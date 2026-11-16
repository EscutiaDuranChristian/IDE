using Compilador_Java.Lexico.Tokens.Table;
using Lexico.Token;

namespace Lexico.States;
public class LexerContext
{
    public TokState State = TokState.Start;
    public string Word = "";
    public List<TokenRow> Tokens = new();
    public char Current;
    public char Previous;
    public bool Update;
}