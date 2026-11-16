using Compilador_Java.Errores;
using System.Text.RegularExpressions;

namespace Compilador_Java.Depuracion;

public static class Depurar
{
    public static bool ValidarEsCodigoJava(string source)
    {
        return Regex.IsMatch(source, @"class\s+(\w+|_\w*)") &&
               Regex.IsMatch(source,
        @"public\s+static\s+void\s+main\s*\(\s*"+
        @"String\s*(\[\]\s*\w+|\w+\s*\[\])\)");
    }
    public static void Limpiar(ref string source)
    {
        source = source.Replace(@";+", ";");
        string resultado = string.Join(
                Environment.NewLine,
                source
                    .Replace("<br>", "")
                    .Split('\n')
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
            );
        source = resultado;
    }
}