using System;

public class IOEventManager
{
    public static Action<string, AssistiveOutput.OutputType> AssistiveOutput;
    
    public static void InvokeAssistiveOutput(string s, AssistiveOutput.OutputType oType) => AssistiveOutput.Invoke(s, oType);
}