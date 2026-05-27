using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IO
{
    public class NavigationInput: AbstractInput
    {
        public override void Enable()
        {
            _actions.Navigation.answer0.started += OnAnswer0;
            _actions.Navigation.answer1.started += OnAnswer1;
            _actions.Navigation.answer2.started += OnAnswer2;
            _actions.Navigation.answer3.started += OnAnswer3;
            _actions.Navigation.answer4.started += OnAnswer4;
            _actions.Navigation.answer5.started += OnAnswer5;
            _actions.Navigation.next.started += OnNext;
            _actions.Navigation.prev.started += OnPrev;
            _actions.Navigation.confirm.started += OnConfirm;
            _actions.Navigation.Enable();
        }
        public override void Disable()
        {
            _actions.Navigation.answer0.started -= OnAnswer0;
            _actions.Navigation.answer1.started -= OnAnswer1;
            _actions.Navigation.answer2.started -= OnAnswer2;
            _actions.Navigation.answer3.started -= OnAnswer3;
            _actions.Navigation.answer4.started -= OnAnswer4;
            _actions.Navigation.answer5.started -= OnAnswer5;
            _actions.Navigation.next.started -= OnNext;
            _actions.Navigation.prev.started -= OnPrev;
            _actions.Navigation.confirm.started -= OnConfirm;
            _actions.Navigation.Disable();
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
            var selected = UIManager.Instance.Options[index];
            selected.ConfirmAction();
        }
        private void SelectOption()
        {
            UIManager.Instance.HighlightedOption.ConfirmAction();
        }
    }
}