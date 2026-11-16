namespace Compilador_Java.Info_Lenguaje;
public class SystemMethods
{
    public static Dictionary<string, Container> IDES { get; private set; } = new()
        {
            { "System", new(TokensJava.TM.Name(), new(){ "java" }) },
             { "java", new(TokensJava.TM.Name(), new(){ "out" }) },
             { "out", new(TokensJava.TM.Name(), new(){ "print", "println" }) },
             { "println", new Method(TokensJava.TM.Name(), new(), new(){"ANY"}) },
             { "print", new Method(TokensJava.TM.Name(), new(), new(){ "ANY" }) },
             {"in", new("in", new())},
             {"Scanner", new Method("Scanner",
                                    new(){ "nextInt", "nextDouble", "nextLine" },
                                    new(){"in"})},
             {"nextInt", new Method(TokensJava.VTE.Name(), new(), new())},
             {"nextDouble",new Method(TokensJava.VTD.Name(), new(), new())},
             {"nextLine",new Method(TokensJava.VTS.Name(), new(), new())},
             {"Math", new(TokensJava.TM.Name(), new(){ "random" })},
             { "random" , new Method(TokensJava.VTD.Name(), new(), new()) },
             { "substring", new Method(TokensJava.VTS.Name(), new(), new(){"VTE", "VTE"}) },
             { "charAt", new Method(TokensJava.VTC.Name(), new(), new(){"VTE"}) }
        };
    public static void Restart()
    {
        IDES = new()
        {
            { "System", new(TokensJava.TM.Name(), new(){ "java" }) },
             { "java", new(TokensJava.TM.Name(), new(){ "out" }) },
             { "out", new(TokensJava.TM.Name(), new(){ "print", "println" }) },
             { "println", new Method(TokensJava.TM.Name(), new(), new(){"ANY"}) },
             { "print", new Method(TokensJava.TM.Name(), new(), new(){ "ANY" }) },
             {"in", new("in", new())},
             {"Scanner", new Method("Scanner",
                                    new(){ "nextInt", "nextDouble", "nextLine" },
                                    new(){"in"})},
             {"nextInt", new Method(TokensJava.VTE.Name(), new(), new())},
             {"nextDouble",new Method(TokensJava.VTD.Name(), new(), new())},
             {"nextLine",new Method(TokensJava.VTS.Name(), new(), new())},
             {"Math", new(TokensJava.TM.Name(), new(){ "random" })},
             { "random" , new Method(TokensJava.VTD.Name(), new(), new()) },
             { "substring", new Method(TokensJava.VTS.Name(), new(), new(){"VTE", "VTE"}) },
             { "charAt", new Method(TokensJava.VTC.Name(), new(), new(){"VTE"}) }
        };
    }
}
public class Container
{
    //VTE VTF VTS TM
    protected string valType = TokensJava.VTE.Name();
    public string Type => valType;
    public HashSet<string> Relaciones = new();
    public bool Validar(string val) => val == valType || valType == "ANY";
    public bool CA(string rel) => Relaciones.Contains(rel);
    public Container(string valType,List<string> relaciones)
    {
        foreach (var relacion in relaciones)
            Relaciones.Add(relacion);
        this.valType = valType;
    }
    public override string ToString()
    {
        return $"Type: {valType}\n"+string.Join("\n", Relaciones);
    }
}

public class Method : Container
{
    public Method(string v, List<string> r, List<string> @params): base(v, r) {
        Params = @params;
    }
    protected List<string> Params = new();
    public bool Validar(List<string> @params)
    {
        if (@params.Count == 0 || Params.Count == 0) 
            return @params.Count == Params.Count;
        foreach (var param in Params)
        {
            if (param != @params[0] && param != "ANY")
                return false;
            @params.RemoveAt(0);
        }
        return true;
    }
}