using Compilador_Java.Lexico.Tokens.Table;
namespace Compilador_Java.Lexico.Simbolos;
public class Symbol
{
    public int? ID { get; private set; }
    public TokenRow token;
    public string Lexena => token.Lexema;
    public string Tipo => token.Tipo;

    public Symbol(int? id, TokenRow symbol)
    {
        token = symbol;
        ID = id;
    }
    public override string ToString()
    {
        string id = "";
        if (ID != null)
            id = ID >= 10 ? ID + "" : "0" + ID;
        else id = "NAN";
        return $"{id}\t\t{token.ToString()}";
    }
}