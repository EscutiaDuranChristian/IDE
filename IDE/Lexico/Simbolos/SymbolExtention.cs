using Compilador_Java.Lexico.Simbolos;

namespace Compilador_Java.Extra;
public static class SymbolExtention
{
    public static string IdToString(this Symbol s)
    {
        if (s.ID == null) return "  ";
        string id = s.ID.ToString()!;
        if(s.ID < 10) id = "0" + id;
        return id;
    }
}