namespace Compilador_Java.Info_Lenguaje;

public class OperatorCompatibility
{
    public static bool Accept(string op, string type)
    {
        if (type == "VTS") return op == "+";
        if (type == "VTE") return new List<string>{ "+", "-", "/", "*", "^", "~" }
                                  .Contains(op);
        if(type == "VTB") return new List<string> { "||", "&&", "<", ">", "<=", ">=" }
                                  .Contains(op);
        return false;
    }
}