namespace Lexico.Token;
public enum TokState
{
    Start,      // No estoy construyendo nada
    Word,       // Identificador o palabra reservada
    Number,
    String,
    Char,
    Comment
}