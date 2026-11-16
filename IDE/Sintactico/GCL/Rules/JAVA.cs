namespace Compilador_Java.Sintactico.GCL.Rules;

public class Java : RuleBase<Java>
{
    public override void Ejec()
    {
        DPackage.Instance!.Ejec();
        Imports.Instance!.Ejec();
        Clase.Instance!.Ejec();
        if (!Ctx.Eof) throw Error("fin de archivo");
    }
}