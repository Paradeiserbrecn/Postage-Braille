using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Braille;
using DavyKager;
using IO;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace Tutorial
{
    /// <summary>
    /// Controls user input during the tutorial
    /// </summary>
    public class TutorialInput : AbstractInput
    {
        /// <summary>
        /// Hold which keys are allowed to be pressed in each <see cref="TutorialFlow"/> State
        /// </summary>
        [SuppressMessage("Serialization", "UAC1009:Unsupported collection type for serialization")]
        private static Dictionary<TutorialFlow.State, TutorialKeymap> Keymaps = new()
        {
            { TutorialFlow.State.Start, new TutorialKeymap(false, false, true, true) },
            { TutorialFlow.State.PickAnswer, new TutorialKeymap(true, true, false, true) },
            { TutorialFlow.State.AfterAnswerPicked, new TutorialKeymap(false, false, true, true) },
            { TutorialFlow.State.FirstSwitchPackButton, new TutorialKeymap(false, false, false, true) },
            { TutorialFlow.State.PackagePickerMenuNavigation, new TutorialKeymap(true, true, false, false) },
            { TutorialFlow.State.OnSelectedUnit, new TutorialKeymap(false, false, true, true) },
            { TutorialFlow.State.AfterUnitPicked, new TutorialKeymap(true, true, true, true) },
            { TutorialFlow.State.AfterPackageScreen, new TutorialKeymap(false, false, true, false) },
            { TutorialFlow.State.GameMenuNavigation, new TutorialKeymap(true, true, false, true) },
            { TutorialFlow.State.SettingsMenuIntroduction, new TutorialKeymap(true, true, false, true) },
            { TutorialFlow.State.SettingsMenuNavigation, new TutorialKeymap(true, true, false, true) },
        };

        public static TutorialKeymap CurrentStateKeymap =>
            TutorialFlow.Instance == null ? null : Keymaps[TutorialFlow.Instance.currentState];

        public TutorialInput(GameActions gameActions) : base(gameActions)
        {
            this.Actions = gameActions;
        }

        public override void Enable()
        {
            Actions.TutorialNavigation.SwitchUILayer.started += OnSwitchUILayer;
            Actions.TutorialNavigation.next.started += OnNext;
            Actions.TutorialNavigation.prev.started += OnPrev;
            Actions.TutorialNavigation.confirm.started += OnConfirm;
            Actions.TutorialNavigation.escape.started += OnEscape;
            Actions.TutorialNavigation.Enable();
        }

        public override void Disable()
        {
            Actions.TutorialNavigation.SwitchUILayer.started -= OnSwitchUILayer;
            Actions.TutorialNavigation.next.started -= OnNext;
            Actions.TutorialNavigation.prev.started -= OnPrev;
            Actions.TutorialNavigation.confirm.started -= OnConfirm;
            Actions.TutorialNavigation.escape.started -= OnEscape;
            Actions.TutorialNavigation.Disable();
        }

        private void OnNext(InputAction.CallbackContext context)
        {
            if (CurrentStateKeymap?.Right == true)
            {
                SceneControl.CurrentUI.HighlightNextOption();
            }
            else
            {
                TutorialOutputs.InvalidKeyPressOutput();
            }
        }

        private void OnPrev(InputAction.CallbackContext context)
        {
            if (CurrentStateKeymap?.Left == true)
            {
                SceneControl.CurrentUI.HighlightPreviousOption();
            }
            else
            {
                TutorialOutputs.InvalidKeyPressOutput();
            }
        }

        private void OnConfirm(InputAction.CallbackContext context)
        {
            if (CurrentStateKeymap?.Enter == true)
            {
                SceneControl.CurrentUI.CurrentlyFocusedOption.ConfirmAction();
            }
            else
            {
                TutorialOutputs.InvalidKeyPressOutput();
            }
        }

        private void OnSwitchUILayer(InputAction.CallbackContext context)
        {
            if (CurrentStateKeymap?.Tab == true)
            {
                SceneControl.CurrentUI.SwitchLayer();
            }
            else
            {
                TutorialOutputs.InvalidKeyPressOutput();
            }
        }

        private void OnEscape(InputAction.CallbackContext context)
        {
            var outputText = TutorialOutputs.SkipTutorial();
            MultimodalInputManager.Instance?.DisableInput(MultimodalInputManager.InputType.Tutorial);
            AssistiveOutput.WaitForSpeakingToFinish(() => SceneManager.LoadScene("Scenes/LetterSortingScene"),
                outputText);
        }
    }
}
