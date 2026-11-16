namespace Compilador_Java.Recopilar_Instrucciones;
public enum INE
{
    DO_WHILE,
    CONTENT_BEGIN,
    CONTENT_END,
    CASES_BEGIN,
    CASE,
    CASE_DEFAULT,
    CASES_END,
    BLOQUE_BEGIN,
    BLOQUE_END,
    IF,
    ELSE,
    START_FOR_PART,
    FOR_PART,
    END_FOR_PARTS,
    METHOD_START,
    METHOD_RETURN,
    METHOD_END
}
public static class InstNameEnumHelper
{
    public static string Name(this INE istn)
    {
        return istn.ToString();
    }
}