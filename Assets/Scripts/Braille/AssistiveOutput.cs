using System;
using DavyKager;
using IO;
using UnityEngine;

// Tolk accessibility wrapper

/*  Contains all functionality for screenreader output (braille and/or audio). */

namespace Braille
{
    public class AssistiveOutput : MonoBehaviour
    {
        public enum OutputType
        {
            Braille,
            Speak,
            Both
        }

        private void Awake()
        {
            IOEventManager.AssistiveOutput += Output;
        }

        public void Start()
        {
            Tolk.Load();
            Debug.Log("Querying for the active screen reader driver ...");

            var reader = Tolk.DetectScreenReader();
            if (reader != null)
                Debug.Log("The active screen reader driver is: " + reader + ".");
            else
                Debug.Log("None of the supported screen readers is running.");

            if (Tolk.HasSpeech())
                Debug.Log("This screen reader driver supports speech.");

            if (Tolk.HasBraille())
                Debug.Log("This screen reader driver supports braille.");
        }

        public void OnDestroy()
        {
            Tolk.Unload();
        }

        // <summary>
        // Output text by using the connected screen reader.
        // </summary>
        // <param name="text">The text to output.</param>
        // <param name="type">If the text is supposed to be output in speech, in braille, or both.</param>
        public void Output(string text, OutputType type = OutputType.Both)
        {
            // Debug.Log("Tolk Output (" + type + "): " + text);

            bool success = false;

            switch (type)
            {
                case OutputType.Braille:
                    success = Tolk.Braille(text);
                    break;
                case OutputType.Speak:
                    success = Tolk.Speak(text);
                    break;
                default:
                    success = Tolk.Output(text);
                    break;
            }

            //if (!success)
            //    Debug.LogWarning("Failed to output text via Tolk.");
        }
    }
}