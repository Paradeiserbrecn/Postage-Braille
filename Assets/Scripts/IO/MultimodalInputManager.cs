using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Tutorial;
using UnityEngine.SceneManagement;

namespace IO
{
    public class MultimodalInputManager : MonoBehaviour
    {
        /// <summary>
        /// Defines supported general use Inputs
        /// </summary>
        public enum InputType
        {
            /// <summary>
            /// InputType includes toggling between UILayers and Focusable objects in those layers but also Directly selecting answers trough number keys
            /// </summary>
            Navigation,

            /// <summary>
            /// InputType handles changes how Braille Characters are displayed on screen via the FKeys
            /// </summary>
            BrailleSettings,

            /// <summary>
            /// InputType handles how controls are being managed while in the tutorial
            /// </summary>
            Tutorial,

            /// <summary>
            /// Resets all key bindings (In separate InputActionMap disable rebinding of this button to prevent a user soft locking themselves)
            /// </summary>
            Reset,
        }


        /// <summary>
        /// Defines supported TextInput Methods
        /// </summary>
        public enum TextInputType
        {
            /// <summary>
            /// InputType used by the perkins brailler simulator
            /// </summary>
            Perkins,

            /// <summary>
            /// InputType used for standard Keyboard Input
            /// </summary>
            Keyboard
        }

        private const string tutorialSceneName = "Tutorial";
        private const string gameSceneName = "LetterSortingScene";

        public static MultimodalInputManager Instance;

        [Header("DEBUG")] [SerializeField] private InputHandledUITextObject defaultTextbox;


        private InputHandledUITextObject _currentTextbox;
        private Dictionary<InputType, AbstractInput> _inputs = new();
        private Dictionary<TextInputType, AbstractTextInput> _textInputs = new();
        public GameActions Actions { get; private set; }

        private void Awake()
        {
            Actions = new GameActions();
            Instance = this;

            ActionRebinder.LoadRebinds();


            //TODO: This currently lead to a strange bug, where if we switch to the game scene after finishing the tutorial,
            // we have both inputmodes active and we focus, submit and do everything twice 
            Debug.Log(SceneManager.GetActiveScene().name);


            // Check whether to activate the normal navigation input type or limit actions in the tutorial
            _inputs[InputType.Navigation] = new NavigationInput(Actions);
            _inputs[InputType.Tutorial] = new TutorialInput(Actions);
            
            
            switch (SceneManager.GetActiveScene().name)
            {
                case tutorialSceneName: EnableInput(InputType.Tutorial); break;
                case gameSceneName: EnableInput(InputType.Navigation); break;
                default: EnableInput(InputType.Navigation); break;
            }

            _inputs[InputType.Reset] = new ResetInput(Actions);
            EnableInput(InputType.Reset);

            _inputs[InputType.BrailleSettings] = new BrailleSettingsInput(Actions);
            EnableInput(InputType.BrailleSettings);


            _textInputs[TextInputType.Perkins] = new PerkinsTextInput(Actions);
            _textInputs[TextInputType.Keyboard] = new KeyboardTextInput(Actions);


            if (defaultTextbox != null)
            {
                EnableTextInput(TextInputType.Keyboard, defaultTextbox);
            }
        }

        /// <summary>
        /// Enables specified TextInputType 
        /// </summary> 
        /// <param name="inputType"> InputType to enable </param>
        public void EnableInput(InputType inputType)
        {
            _inputs[inputType].Enable();
        }

        /// <summary>
        /// Disables specified TextInputType 
        /// </summary> 
        /// <param name="inputType"> InputType to disable </param>
        public void DisableInput(InputType inputType)
        {
            _inputs[inputType].Disable();
        }

        /// <summary>
        /// Enables specified TextInputType 
        /// </summary>
        /// <param name="textInputType"> TextInputType to enable </param>
        /// <param name="textBox"> InputHandledUITextObject that should receive the Inputs </param>
        public void EnableTextInput(TextInputType textInputType, InputHandledUITextObject textBox)
        {
            DisableTextInput();

            _textInputs[textInputType].Textbox = textBox;
            _textInputs[textInputType].Enable();
            _currentTextbox = textBox;

            var notifier = _currentTextbox.destroyDisableNotifier;
            if (notifier == null)
            {
                notifier = _currentTextbox.destroyDisableNotifier;
            }

            notifier.Destroyed += DisableTextInput;
            notifier.Disabled += DisableTextInput;
        }

        /// <summary>
        /// Disables all TextInputTypes 
        /// </summary>
        public void DisableTextInput()
        {
            foreach (var input in _textInputs.Keys)
            {
                _textInputs[input].Disable();
            }

            if (_currentTextbox != null)
            {
                var notifier = _currentTextbox.destroyDisableNotifier;
                if (notifier != null)
                {
                    notifier.Destroyed -= DisableTextInput;
                    notifier.Disabled -= DisableTextInput;
                }

                _currentTextbox = null;
            }
        }

        private void OnDestroy()
        {
            foreach (var input in _inputs.Values) input?.Disable();

            foreach (var input in _textInputs.Values) input?.Disable();

            Actions?.Disable();
            Actions?.Dispose();

            _inputs.Clear();
            _textInputs.Clear();

            if (Instance == this)
                Instance = null;
        }
    }
}
