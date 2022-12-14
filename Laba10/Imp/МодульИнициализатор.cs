using System.Runtime.CompilerServices;
using HarmonyLib;

namespace Laba10.Imp;

public static class МодульИнициализации
{
    [ModuleInitializer]
    public static void Инициализировать()
    {
        var harmony = new Harmony("КОД ПИСАЛ НИКИТА ЖУРО");
        harmony.PatchAll();
    }
}