using Compilador_Java.Lexico.Tokens.Table;

namespace Compilador_Java.Info_Lenguaje;
public static class Reservadas
{
    public static readonly Dictionary<string, string> Map = new()
    {
        {"public", TokensJava.MA.Name()},
        {"private", TokensJava.MA.Name()},
        {"protected", TokensJava.MA.Name()},

        {"class", TokensJava.CL.Name()},

        {"void", TokensJava.TM.Name()},

        {"int", TokensJava.TD.Name()},
        {"double", TokensJava.TD.Name()},
        {"boolean", TokensJava.TD.Name()},
        {"float", TokensJava.TD.Name()},
        {"long", TokensJava.TD.Name()},
        {"short", TokensJava.TD.Name()},
        {"byte", TokensJava.TD.Name()},
        {"char",TokensJava.TD.Name()},

        {"static",TokensJava.ST.Name()},

        {"if",TokensJava.PRIF.Name()},
        {"else",TokensJava.PRE.Name()},
        {"for",TokensJava.PRF.Name()},
        {"while",TokensJava.PRW.Name()},
        {"switch",TokensJava.PRSW.Name()},

        {"return",TokensJava.RTN.Name()},

        {"import",TokensJava.IMP.Name()},

        {"try", TokensJava.ISE.Name()},
        {"catch",TokensJava.CE.Name()},

        {"true", TokensJava.VTB.Name()},
        {"false", TokensJava.VTB.Name()},

        {"String",TokensJava.STR.Name()},

        {"main",TokensJava.PMAIN.Name()},

        {",", TokensJava.CO.Name()},

        {"++", TokensJava.INC.Name() },
        { "--", TokensJava.DEC.Name() },

        { "~", TokensJava.OPB.Name() },
        { "^", TokensJava.XOR.Name() },
        { "!", TokensJava.OPN.Name() },

        { "<", TokensJava.OPR.Name() },
        { ">", TokensJava.OPR.Name() },
        { "==", TokensJava.OPR.Name() },

        { "||", TokensJava.OPL.Name() },
        { "&&", TokensJava.OPL.Name() },

        { "/", TokensJava.OPA.Name() },
        { "*", TokensJava.OPA.Name() },
        { "+", TokensJava.OPA.Name() },
        { "-", TokensJava.OPA.Name() },
        { "%", TokensJava.OPA.Name() },

        { "=", TokensJava.ODA.Name() },
        
        { ";", TokensJava.FI.Name()},
        
        { "{", TokensJava.LLA.Name() },
        { "}", TokensJava.LLC.Name() },

        { "(", TokensJava.PA.Name() },
        { ")", TokensJava.PC.Name() },

        { "[", TokensJava.COA.Name()},
        { "]", TokensJava.COC.Name()},

        { "case", TokensJava.CS.Name() },

        { "break", TokensJava.BR.Name() },

        { "new", TokensJava.NW.Name() },

        { "default", TokensJava.DF.Name() },

        { ".", TokensJava.CA.Name() },
        { ":", TokensJava.DOP.Name() },

        { "do", TokensJava.PRDO.Name() },

        {"package", TokensJava.PCK.Name() }
    };

    public static TokenRow EOF = new("\0", TokensJava.EOF.Name());
}