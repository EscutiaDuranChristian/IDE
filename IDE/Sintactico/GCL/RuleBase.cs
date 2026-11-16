using Compilador_Java.Info_Lenguaje;
using Compilador_Java.Lexico.Tokens.Table;
using Compilador_Java.Sintactico.GCL.Context;
using Compilador_Java.Sintactico.Result;

namespace Compilador_Java.Sintactico.GCL;
public abstract class RuleBase
{
    public static GramarContext Ctx { get; private set; } = new(new());
    public static void SetContext(GramarContext ctx) => Ctx = ctx;
    public static event Action<TokenRow>? OnAcceptToken;
    public static event Action<string>? OnDetectValidInst;
    protected string classname = "";
    protected string successMessage() => $"{classname} Detectado!"; 
    protected void RaiseAcceptToken(TokenRow tk) =>
        OnAcceptToken?.Invoke(tk);
    protected void RaiseDetectValidInst(string msg = "") =>
        OnDetectValidInst?.Invoke(msg == "" ? successMessage() : msg);
    public static void Clear()
    {
        OnAcceptToken = null;
        OnDetectValidInst = null;
    }
}

public abstract class RuleBase<T> : RuleBase where T : RuleBase<T>
{
    public static T? Instance;

    public RuleBase()
    {
        if (Instance == null) Instance = (T)this;
        classname = typeof(T).Name;
    }

    protected static SyntaxException Error(string esperado)
    {
        int i_token = Ctx.CurrentTokenIndex;
        string encontrado = Ctx.Eof
            ? "fin de la tabla"
            : $"'{Ctx.CurrentToken().Lexema}' ({Ctx.CurrentToken().Tipo})";

        return new SyntaxException(
            $"Error sintáctico en el token #{i_token}: " +
            $"se esperaba {esperado} pero se encontró {encontrado}.", i_token);
    }

    protected void Expect(string tipo)
    {
        if (Ctx.CurrentToken().Tipo != tipo) 
            throw Error(tipo);
        Avanzar();
    }

    protected bool Accept(string tipo)
    {
        if (Ctx.CurrentToken().Tipo != tipo) 
            return false;
        Avanzar();
        return true;
    }

    protected void Avanzar(int s=1)
    {
        if (Ctx.CurrentToken().Tipo == TokensJava.ID.Name() &&
            Ctx.CurrentSymbol().ID != null)
        {
            string id = Ctx.CurrentSymbol().ID.ToString()!;
            if (Ctx.CurrentSymbol().ID < 10)
                id = '0' + id;
            id = "ID" + id;
            Ctx.CurrentToken().Lexema = id;
        }
        RaiseAcceptToken(Ctx.CurrentToken());
        Ctx.Advance(s);
    }
    public abstract void Ejec();
}