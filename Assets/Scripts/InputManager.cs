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
    }

    void SelectOption(int index)
    {
        string selected = QuestionManager.Instance.currentOptions[index];
        GameManager.Instance.SubmitAnswer(selected);
    }
}