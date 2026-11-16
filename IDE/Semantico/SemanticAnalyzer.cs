using Compilador_Java.Recopilar_Instrucciones;
using Compilador_Java.Semantico.Analyzers;
using DI = Compilador_Java.Recopilar_Instrucciones.DetectorInstrucciones;

namespace Compilador_Java.Semantico;
public class SemanticAnalyzer
{
    public static void Start()
    {
        var instrucciones = DI.Instance!.Instrucciones;
        string classname = "", parent = "";

        for(int i = 0; i < instrucciones.Count; i++)
        {
            if (instrucciones[i].Message.Contains("Clase"))
                parent = classname = ClassAnalyzer.Run(instrucciones[i]);
            else if (instrucciones[i].Message.Contains("Metodo"))
                parent = MethodAnalyzer.Run(instrucciones[i], classname);
            else if (instrucciones[i].Message.Contains("DecOrObj"))
                DecOrObjAnalyzer.Run(instrucciones[i], instrucciones[i+1], parent);
            else if (instrucciones[i].Message == INE.METHOD_RETURN.Name())
                ReturnAnalyzer.Run(instrucciones[i], parent);
            else if (instrucciones[i].Message == INE.CONTENT_END.Name() ||
                     instrucciones[i].Message == INE.BLOQUE_END.Name() ||
                     instrucciones[i].Message == INE.METHOD_END.Name())
                parent = classname;
        }
    }
}