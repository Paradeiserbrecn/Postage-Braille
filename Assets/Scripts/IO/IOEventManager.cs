using System;
using Braille;
using UnityEngine;

namespace IO
{
    public class IOEventManager:MonoBehaviour
    {
        public static Action<string, AssistiveOutput.OutputType> AssistiveOutput;
        public static Action<string> DefaultOutput;
        public static Action BrailleSizeChanged;
        public static Action DotSizeChanged;
        public static Action BrailleSpacingChanged;
        public static Action LineSpacingChanged;
        public static Action BrailleColorChanged;
        public static void InvokeAssistiveOutput(string s, AssistiveOutput.OutputType oType) => AssistiveOutput.Invoke(s, oType);

        public static void InvokeDefaultOutput(string s) => DefaultOutput.Invoke(s);
        public static void InvokeBrailleSizeChanged() => BrailleSizeChanged.Invoke();
        public static void InvokeDotSizeChanged() => DotSizeChanged.Invoke();
        public static void InvokeBrailleSpacingChanged() => BrailleSpacingChanged.Invoke();
        public static void InvokeLineSpacingChanged() => LineSpacingChanged.Invoke();
        public static void InvokeBrailleColorChanged() => BrailleColorChanged.Invoke();
    }
}
