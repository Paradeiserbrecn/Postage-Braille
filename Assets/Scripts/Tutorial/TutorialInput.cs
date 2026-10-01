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
            Actions.TutorialNavigation.SwitchUILayer.started += OnSwitchUILayer;
            Actions.TutorialNavigation.next.started += OnNext;
            Actions.TutorialNavigation.prev.started += OnPrev;
            Actions.TutorialNavigation.confirm.started += OnConfirm;
            Actions.TutorialNavigation.Enable();
        }

        public override void Disable()
        {
            Actions.TutorialNavigation.SwitchUILayer.started -= OnSwitchUILayer;
            Actions.TutorialNavigation.next.started -= OnNext;
            Actions.TutorialNavigation.prev.started -= OnPrev;
            Actions.TutorialNavigation.confirm.started -= OnConfirm;
            Actions.TutorialNavigation.Disable();
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
