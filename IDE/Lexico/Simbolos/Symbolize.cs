using Compilador_Java.Lexico.Tokens.Table;
using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Extra;
namespace Compilador_Java.Lexico.Simbolos;
public class Symbolize
{
    public static List<Symbol> SymbolTable(List<TokenRow> tokenTable)
    {
        List<Symbol> symbols = new();
        int id = 1;
        foreach (var token in tokenTable)
        {
            string lexema = token.Lexema;
            int? i = 0;
            if (token.Tipo == TokensJava.ID.Name())
            {
                Symbol? s = symbols.Find(x => x.token.Lexema == lexema);
                if (SystemMethods.IDES.ContainsKey(lexema))
                    i = null;
                else if (s != null) { 
                    lexema = $"ID{s.IdToString()}"; 
                    i = null;
                }
                else i = id++;
                token.Lexema = lexema;
                symbols.Add(new(i, token));
            }
            else symbols.Add(new(null, token));
        }
        return symbols;
    }
}