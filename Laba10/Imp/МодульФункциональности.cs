using HarmonyLib;

namespace Laba10.Imp;

[HarmonyPatch(typeof(Программа), nameof(Программа.Main))]
public class МодульФункциональности
{
    public static bool Prefix(string[] args)
    {
        МодульЛабыСамыйГлавный.ПрограммаРаботайДон();
        return false;
    }
}