using IO;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    public class TutorialInput : AbstractInput
    {
        public TutorialInput(GameActions gameActions) : base(gameActions)
        {
            this.Actions = gameActions;
        }

        public override void Enable()
        {
            Actions.Navigation.SwitchUILayer.started += OnSwitchUILayer;
            Actions.Navigation.next.started += OnNext;
            Actions.Navigation.prev.started += OnPrev;
            Actions.Navigation.confirm.started += OnConfirm;
            Actions.Navigation.Enable();
        }

        public override void Disable()
        {
            Actions.Navigation.SwitchUILayer.started -= OnSwitchUILayer;
            Actions.Navigation.next.started -= OnNext;
            Actions.Navigation.prev.started -= OnPrev;
            Actions.Navigation.confirm.started -= OnConfirm;
            Actions.Navigation.Disable();
        }

        private void OnNext(InputAction.CallbackContext context)
        {
            if (Tutorial.CurrentStateKeymap?.Right == true)
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
            if (Tutorial.CurrentStateKeymap?.Left == true)
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
            if (Tutorial.CurrentStateKeymap?.Enter == true)
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
            if (Tutorial.CurrentStateKeymap?.Tab == true)
            {
                SceneControl.CurrentUI.SwitchLayer();
            }
            else
            {
                TutorialOutputs.InvalidKeyPressOutput();
            }
        }
    }
}
