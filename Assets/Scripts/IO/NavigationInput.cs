using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IO
{
    public class NavigationInput: AbstractInput
    {
        public override void Enable()
        {
            Actions.Navigation.answer0.started += OnAnswer0;
            Actions.Navigation.answer1.started += OnAnswer1;
            Actions.Navigation.answer2.started += OnAnswer2;
            Actions.Navigation.answer3.started += OnAnswer3;
            Actions.Navigation.answer4.started += OnAnswer4;
            Actions.Navigation.answer5.started += OnAnswer5;
            Actions.Navigation.SwitchUILayer.started += OnSwitchUILayer;
            Actions.Navigation.next.started += OnNext;
            Actions.Navigation.prev.started += OnPrev;
            Actions.Navigation.confirm.started += OnConfirm;
            Actions.Navigation.Enable();
        }
        public override void Disable()
        {
            Actions.Navigation.answer0.started -= OnAnswer0;
            Actions.Navigation.answer1.started -= OnAnswer1;
            Actions.Navigation.answer2.started -= OnAnswer2;
            Actions.Navigation.answer3.started -= OnAnswer3;
            Actions.Navigation.answer4.started -= OnAnswer4;
            Actions.Navigation.answer5.started -= OnAnswer5;
            Actions.Navigation.SwitchUILayer.started -= OnSwitchUILayer;
            Actions.Navigation.next.started -= OnNext;
            Actions.Navigation.prev.started -= OnPrev;
            
            Actions.Navigation.confirm.started -= OnConfirm;
            Actions.Navigation.Disable();
        }
        private void OnAnswer0(InputAction.CallbackContext context) {SelectOption(0);}
        private void OnAnswer1(InputAction.CallbackContext context) {SelectOption(1);}
        private void OnAnswer2(InputAction.CallbackContext context) {SelectOption(2);}
        private void OnAnswer3(InputAction.CallbackContext context) {SelectOption(3);}
        private void OnAnswer4(InputAction.CallbackContext context) {SelectOption(4);}
        private void OnAnswer5(InputAction.CallbackContext context) {SelectOption(5);}
        private void OnNext(InputAction.CallbackContext context) { UIManager.Instance.HighlightNextOption(); }
        private void OnPrev(InputAction.CallbackContext context) { UIManager.Instance.HighlightPreviousOption(); }
        private void OnConfirm(InputAction.CallbackContext context) { SelectOption();}
        private void SelectOption(int index)
        {
            var selected = UIManager.Instance.CurrentOptions[index];
            selected.ConfirmAction();
        }
        private void SelectOption()
        {
            UIManager.Instance.CurrentlyFocusedOption.ConfirmAction();
        }
        
        private void OnSwitchUILayer(InputAction.CallbackContext context)
        {
            UIManager.Instance.SwitchLayer();
        }
    }
}