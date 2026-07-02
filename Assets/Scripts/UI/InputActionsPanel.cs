using System;
using System.Collections.Generic;
using IO;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InputActionsPanel : MonoBehaviour
{
    [SerializeField] private GameObject rebindButtonPrefab;
    [SerializeField] private GameObject scrollRectContent;
    [SerializeField] private ScrollRect scrollRect;
    private readonly List<FocusableRebindOption> _rebindButtons = new List<FocusableRebindOption>();

    public FocusableRebindOption AddButton(InputAction inputAction, Action<InputAction, FocusableRebindOption> action) 
    {
        var newButton = Instantiate(rebindButtonPrefab, scrollRectContent.transform).GetComponent<FocusableRebindOption>();
        newButton.SetActionName(inputAction.name);
        newButton.SetBindingText(inputAction.bindings[0].effectivePath);
        newButton.InputAction = inputAction;
        newButton.inputActionsPanel = this;
        _rebindButtons.Add(newButton);
        return newButton;
    }

    public void ScrollTo(RectTransform targetRectTransform)
    {
        Canvas.ForceUpdateCanvases();
        Vector2 viewportLocalPosition = scrollRect.viewport.localPosition;
        
        Vector2 targetLocalPosition = targetRectTransform.localPosition;
        
        Vector2 newTargetLocalPosition = new Vector2(
            0 - (viewportLocalPosition.x + targetLocalPosition.x),
            Math.Min(0 - (viewportLocalPosition.y + targetLocalPosition.y) + (scrollRect.viewport.rect.height/2) - (targetRectTransform.rect.height * 1.3f),
            viewportLocalPosition.y + scrollRect.viewport.rect.height - targetRectTransform.rect.height/2.5f)
            );
        
        scrollRectContent.transform.localPosition = newTargetLocalPosition;
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
