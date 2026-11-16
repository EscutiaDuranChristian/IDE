namespace Compilador_Java.Sintactico.Result;

public class SyntaxResult
{
    public bool Ok { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Position { get; set; }
    public override string ToString() => Message;
}