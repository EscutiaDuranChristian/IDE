using Compilador_Java.Info_Lenguaje;
using System.Runtime.CompilerServices;

namespace Compilador_Java.Sintactico.GCL.Rules;
public class Val : RuleBase<Val>
{
    public override void Ejec()
    {
        string c = Ctx.CurrentToken().Tipo;
        if (TokenHelper.IsVAL(c)) {
            Avanzar();
            return; 
        }
        throw Error("un valor (VTE, VTD, VTS, VTB, ID, VTF o VTC)");
    }
    
}