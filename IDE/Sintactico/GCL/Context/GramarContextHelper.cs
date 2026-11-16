using Compilador_Java.Info_Lenguaje;
namespace Compilador_Java.Sintactico.GCL.Context;
public static class GramarContextHelper
{
    public static bool EsMetodo(this GramarContext ctx)
    {
        int k = 0;
        if (ctx.At(k).Tipo == TokensJava.MA.Name()) 
            k++;
        if (ctx.At(k).Tipo == TokensJava.TM.Name() || 
            ctx.At(k).IsTY()) k++;
        else return false;
        return ctx.At(k).Tipo == TokensJava.ID.Name() && 
               ctx.At(k + 1).Tipo == TokensJava.PA.Name();
    }
}