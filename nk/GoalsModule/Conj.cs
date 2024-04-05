﻿
using static nk.LoggerModule;

namespace nk;


public static partial class GoalsModule
{
    // PUBLIC
   
    public static Goal Conj(params Goal[] gs) // p 177
    {
        return (Situation s) =>
        {
            LogDebug($"Conj({s}, g*{gs.Length})");

            return gs.Length switch
            {
                0 => Succ()(s),
                1 => gs[0](s),
                _ => Conj(gs[1..])(s).MapInf(gs[0])
            };
        };
    }
}