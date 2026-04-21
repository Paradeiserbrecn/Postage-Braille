using UnityEngine;

public static class GlobalSettings
{
    public static float BrailleSize = 0.6f;
    public static float DotSize = 0.7f;
    public static Color BrailleColor = Color.white;
    public static Language Language = Language.German;
}

public enum Language
{
    German
}