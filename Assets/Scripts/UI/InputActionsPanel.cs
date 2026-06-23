using System;
using System.Collections.Generic;
using IO;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionsPanel : MonoBehaviour
{
    [SerializeField] private GameObject rebindButtonPrefab;
    [SerializeField] private GameObject ScrollRectContent;
    private List<FocusableRebindOption> _rebindButtons = new List<FocusableRebindOption>();

    public FocusableRebindOption AddButton(InputAction inputAction, Action<InputAction, FocusableRebindOption> action) 
    {
        var newButton = Instantiate(rebindButtonPrefab, ScrollRectContent.transform).GetComponent<FocusableRebindOption>();
        newButton.SetActionName(inputAction.name);
        newButton.SetBindingText(inputAction.bindings[0].effectivePath);
        newButton.InputAction = inputAction;
        return newButton;
    }

    public void ClearAll()
    {
        foreach (var button in _rebindButtons)
        {
            Destroy(button.gameObject);
        }
        _rebindButtons.Clear();
    }
}
