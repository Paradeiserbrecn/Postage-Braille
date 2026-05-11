using UnityEngine;
using UnityEngine.InputSystem;

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

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) UIManager.Instance.HighlightPreviousGridTextObject();
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) UIManager.Instance.HighlightNextGridTextObject();
        if (Keyboard.current.enterKey.wasPressedThisFrame) SelectOption();
    }

    private void SelectOption(int index)
    {
        var selected = QuestionManager.Instance.currentOptions[index];
        GameManager.Instance.SubmitAnswer(selected);
    }

    private void SelectOption()
    {
        GameManager.Instance.SubmitAnswer(UIManager.Instance.HighlightedGridTextObject.text);
    }
}