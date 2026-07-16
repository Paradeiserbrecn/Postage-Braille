using Braille;
using Color = UnityEngine.Color;

namespace Settings
{
    public static class GlobalSettings
    {
        public const AssistiveOutput.OutputType standardOutputType = AssistiveOutput.OutputType.Both;
        public const AssistiveOutput.OutputType questionOutputType = AssistiveOutput.OutputType.Braille;
        public const float MinBrailleSize = 30f;
        public const float BaseBrailleSize = 50f;
        public const float MaxBrailleSize = 70f;
        public const float BrailleSizeIncrement = 5f;

        public const float MinDotSize = 0.2f;
        public const float BaseDotSize = 0.7f;
        public const float MaxDotSize = 0.9f;
        public const float DotSizeIncrement = 0.1f;

        public const float MinBrailleSpacing = 0f;
        public const float BaseBrailleSpacing = 5f;
        public const float MaxBrailleSpacing = 50f;
        public const float BrailleSpacingIncrement = 5f;

        public const float MinLineSpacing = 20f;
        public const float BaseLineSpacing = 100f;
        public const float MaxLineSpacing = 150f;
        public const float LineSpacingIncrement = 10f;


        public static float BrailleSize = BaseBrailleSize;
        public static float DotSize = BaseDotSize;
        public static float BrailleSpacing = BaseBrailleSpacing;
        public static float LineSpacing = BaseLineSpacing;
        public static Color BrailleColor = Color.white;
        public static Color HighlightedColor = Color.red;
        public static Color HighlightedButtonColor = Color.brown;
        public static Color TextColor = Color.white;
        public static Color MenuOptionColor = Color.white;
        public static Color SortingBoxColor = Color.white;
        public static Color QuestionBrailleColor = Color.black;
        public static Color QuestionTextColor = Color.black;
        public static Color PackageTextColor = Color.black;
    }
}
