using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IO
{
    public class InputManager : MonoBehaviour
    {
        void Update()
        {
            if (GameManager.Instance.currentState != GameManager.GameState.WaitingForInput)
                return;

            if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectOption(0);
            if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectOption(1);
            if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectOption(2);
            if (Keyboard.current.digit4Key.wasPressedThisFrame) SelectOption(3);
            if (Keyboard.current.digit5Key.wasPressedThisFrame) SelectOption(4);
            if (Keyboard.current.digit6Key.wasPressedThisFrame) SelectOption(5);

            if (Keyboard.current.leftArrowKey.wasPressedThisFrame) UIManager.Instance.HighlightPreviousOption();
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame) UIManager.Instance.HighlightNextOption();
            if (Keyboard.current.enterKey.wasPressedThisFrame) SelectOption();
            
            if (Keyboard.current.tabKey.wasPressedThisFrame) UIManager.Instance.SwitchLayer();
        }

        private void SelectOption(int index)
        {
            var selected = UIManager.Instance.CurrentOptions[index];
            selected.ConfirmAction();
        }

        private void SelectOption()
        {
            UIManager.Instance.CurrentlyFocusedOption.ConfirmAction();
        }
    }
}