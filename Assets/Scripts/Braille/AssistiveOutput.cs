using System;
using System.Threading.Tasks;
using DavyKager;
using IO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

// Tolk accessibility wrapper

/*  Contains all functionality for screenreader output (braille and/or audio). */

namespace Braille
{
    public class AssistiveOutput : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI visualOutputText;

        private const float fontCharacterWidth = 0.63197183098591549295774647887324f;
        private string _resetTextPos;
        private Action<string, float> _invoke;

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

            _resetTextPos = nameof(ResetTextPos);
            _invoke = Invoke;
        }

        private Vector2 endTextPosX;
        private Vector2 defaultTextPosX;

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

            defaultTextPosX = endTextPosX = visualOutputText.transform.localPosition;
        }

        private void Update()
        {
            MoveAssistiveOutputTextToEndPos();
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
            visualOutputText.text = "";
            Debug.Log("Tolk Output (" + type + "): " + text);

            Tolk.Silence();
            switch (type)
            {
                case OutputType.Braille:
                    visualOutputText.text = "Braille wird nicht vorgelesen.";
                    Tolk.Braille(text);
                    break;
                case OutputType.Speak:
                    visualOutputText.text = text;
                    Tolk.Speak(text);
                    break;
                case OutputType.Both:
                    visualOutputText.text = text;
                    Tolk.Output(text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            visualOutputText.transform.localPosition = defaultTextPosX;
            CalculateOutputTextPos();
        }


        /// <summary>
        /// Calculates the endposition of <see cref="visualOutputText"/> based on _text.text.length, the fontsize and the
        /// <see cref="fontCharacterWidth"/> such that the last character of _text.text is just visible in the TextMeshProUGUI
        ///
        /// If the text is not overflowing its boundaries, <see cref="endTextPosX"/> is set to <see cref="defaultTextPosX"/>
        /// </summary>
        private void CalculateOutputTextPos()
        {
            if (visualOutputText.rectTransform.sizeDelta.x < fontCharacterWidth * visualOutputText.text.Length * visualOutputText.fontSize)
            {
                // Overflowing
                endTextPosX = new Vector2(
                    Math.Abs(visualOutputText.rectTransform.sizeDelta.x) - fontCharacterWidth * visualOutputText.fontSize * visualOutputText.text.Length,
                    visualOutputText.transform.localPosition.y);
            }
            else
            {
                endTextPosX = defaultTextPosX;
            }
        }

        private bool _resettingToDefaultPosition = false;
        private const int TextResetDelay = 2;

        //TODO: Put all the text related stuff in its own mono-behaviour so assistive output doesnt handle the text position
        private void MoveAssistiveOutputTextToEndPos()
        {
            // If we reset to the default position, we wait for _textResetDelay seconds until we reset.
            // we do not have to move the text anymore that time 
            if (_resettingToDefaultPosition) return;
            
            if (Vector2.Distance(visualOutputText.transform.localPosition, endTextPosX) > 0.1f)
            {
                visualOutputText.transform.localPosition = 
                    Vector2.MoveTowards(visualOutputText.transform.localPosition, endTextPosX, 200 * Time.deltaTime);
            }
            else
            {
                _resettingToDefaultPosition = true;
                // This is necessary due to how Invoke is handled within c#
                // Invoke is being stored in _invoke so the Invoke Heading is not being generated and garbage collected each time we reset the textPos
                _invoke(_resetTextPos, TextResetDelay);
            }
        }

        private void ResetTextPos()
        {
            visualOutputText.transform.localPosition = defaultTextPosX;
            _resettingToDefaultPosition = false;
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
