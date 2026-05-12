using System.Drawing;
using Color = UnityEngine.Color;

public static class GlobalSettings
{
    
    
    public const float MinBrailleSize = 30f;
    public const float BaseBrailleSize = 50f;
    public const float MaxBrailleSize = 70f;
    public const float BrailleSizeIncrement = 5f;
    
    public const float MinDotSize = 0.2f;
    public const float BaseDotSize = 0.7f;
    public const float MaxDotSize = 0.9f;
    public const float DotSizeIncrement = 0.1f;
    
    public const float MinBrailleSpacing = 0f;
    public const float BaseBrailleSpacing = 50f;
    public const float MaxBrailleSpacing = 100f;
    public const float BrailleSpacingIncrement = 5f;
    
    public const float MinLineSpacing = 20f;
    public const float BaseLineSpacing = 100f;
    public const float MaxLineSpacing = 150f;
    public const float LineSpacingIncrement = 10f;
    
    
        
        
    public static float BrailleSize = 50f;
    public static float DotSize = 0.7f;
    public static float BrailleSpacing = 50f;
    public static float LineSpacing = 100f;
    public static Color BrailleColor = Color.white;
    public static Color HighlightedColor = Color.red;
    public static Color TextColor = Color.white;
}