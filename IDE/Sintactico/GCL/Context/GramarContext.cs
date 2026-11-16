using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Lexico.Simbolos;
using Compilador_Java.Lexico.Tokens.Table;

namespace Compilador_Java.Sintactico.GCL.Context;
public class GramarContext
{
    public List<Symbol> SymbolTable = new();
    public int CurrentTokenIndex { get; private set; } = 0;

    public GramarContext(List<Symbol> t) => SymbolTable = t;

    public TokenRow CurrentToken() => At(0);
    public Symbol CurrentSymbol() => SymbolTable[CurrentTokenIndex];

    public TokenRow At(int k)
    {
        if (k + CurrentTokenIndex < SymbolTable.Count &&
            k + CurrentTokenIndex >= 0)
            return SymbolTable[k + CurrentTokenIndex].token;
        else return Reservadas.EOF;
    }
    public bool Eof => CurrentTokenIndex >= SymbolTable.Count;
    public void Advance(int s=1) => CurrentTokenIndex+=s;
}