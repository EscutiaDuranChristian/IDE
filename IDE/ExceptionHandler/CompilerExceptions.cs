namespace Compilador_Java.Errores;
public class CompilerException
{
    public static readonly Dictionary<string, string> exception =
        new()
        {
            {"BAD ID", "ID solo puede empezar con letras o guines bajos" },
            {"NO TOKEN", "No pudimos reconocer el token ingresado:" },
            {"BAD CHAR", "No puedes generar un char de mas de un caracter: " },
            {"BAD VALUE", "Este valor resultó ser invalido: " },
            {"INVALID USE OF TYPE", "Este valor es un tipo, lo cual no esta permitido en este contexto"},
            {"BAD SOURCE", "El código fuente no es válido." },
            { "INVALID SYNTAX", "La sintaxis del codigo no es valida: " },
            { "SEMANTIC ERROR", "Error semántico encontrado: " }
        };
    private static List<(string, string)> compilation { get; } = new();
    public static void AddException(string ex, string val = "")
    {
        if (exception.ContainsKey(ex))
        {
            string msg = exception[ex] + val;
            compilation.Add((ex, msg));
        }
    }
    public static List<(string, string)> GetExceptions() => new(compilation);
    public static void Clear() => compilation.Clear();
    public static bool HasExceptions() => compilation.Count > 0;
}