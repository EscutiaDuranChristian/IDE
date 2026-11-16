namespace Compilador_Java.Sintactico.Result;

public class SyntaxException : Exception
{
    public int Position { get; }
    public SyntaxException(string msg, int pos) : 
        base(msg) { Position = pos; }
}

