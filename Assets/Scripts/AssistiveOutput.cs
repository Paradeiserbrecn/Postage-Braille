using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DavyKager; // Tolk accessibility wrapper

/*  Contains all functionality for screenreader output (braille and/or audio). */

public class AssistiveOutput : MonoBehaviour
{
    public enum OutputType
    {
        BRAILLE,
        SPEAK,
        BOTH
    }

    void Start()
    {
        Tolk.Load();
        Debug.Log("Querying for the active screen reader driver ...");

        string name = Tolk.DetectScreenReader();
        if (name != null)
            Debug.Log("The active screen reader driver is: " + name + ".");
        else
            Debug.Log("None of the supported screen readers is running.");

        if (Tolk.HasSpeech())
            Debug.Log("This screen reader driver supports speech.");

        if (Tolk.HasBraille())
            Debug.Log("This screen reader driver supports braille.");

        IOEventManager.AssistiveOutput += Output;
    }

    void OnDestroy()
    {
        Tolk.Unload();
    }

    // <summary>
    // Output text by using the connected screen reader.
    // </summary>
    // <param name="text">The text to output.</param>
    // <param name="type">If the text is supposed to be output in speech, in braille, or both.</param>
    public void Output(string text, OutputType type)
    {
        Debug.Log("Tolk Output (" + type + "): " + text);

        bool success = false;

        switch(type)
        {
            case OutputType.BRAILLE:
                success = Tolk.Braille(text);
                break;
            case OutputType.SPEAK:
                success = Tolk.Speak(text);
                Debug.Log("trying to speak");
                break;
            default:
                success = Tolk.Output(text);
                break;
        }

        if (!success)
            Debug.Log("Failed to output text via Tolk.");
    }

}
