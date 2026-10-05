using System;
using System.Threading.Tasks;
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
            IOEventManager.DefaultOutput += OnDefaultOutput;
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
            IOEventManager.AssistiveOutput -= Output;
            Tolk.Unload();
        }

        /// <summary>
        /// Enables Output being specified as action in focusableMenuButtons, outputs both braille and speech if available.
        /// </summary>
        /// <param name="text"></param>
        public void OnDefaultOutput(string text)
        {
            Output(text);
        }

        // <summary>
        // Output text by using the connected screen reader.
        // </summary>
        // <param name="text">The text to output.</param>
        // <param name="type">If the text is supposed to be output in speech, in braille, or both.</param>
        public void Output(string text, OutputType type = OutputType.Both)
        {
            Debug.Log("Tolk Output (" + type + "): " + text);

            bool success = false;

            switch (type)
            {
                case OutputType.Braille:
                    Tolk.Silence();
                    success = Tolk.Braille(text);
                    break;
                case OutputType.Speak:
                    Tolk.Silence();
                    success = Tolk.Speak(text);
                    break;
                default:
                    success = Tolk.Output(text);
                    break;
            }

            //if (!success)
            //    Debug.LogWarning("Failed to output text via Tolk.");
        }

        private static readonly float TimePerCharacter = 0.08f;

        /// <summary>
        /// Waits <see cref="TimePerCharacter"/> seconds per character of the given string before invoking the action
        /// If <see cref="Tolk"/> does not detect a screen reader, it will immediately invoke the action instead
        /// </summary>
        /// <param name="action">The action to be executed after assistive output has finished speaking</param>
        /// <param name="text">The text that is being spoken, needed to calculate speaking time for <see cref="Tolk"/></param>
        public static async Task WaitForSpeakingToFinish(Action action, string text)
        {
            if (Tolk.DetectScreenReader() == null)
            {
                action?.Invoke();
                return;
            }

            await Task.Delay(TimeSpan.FromSeconds(TimePerCharacter * text.Length));
            action?.Invoke();
        }
    }
}
