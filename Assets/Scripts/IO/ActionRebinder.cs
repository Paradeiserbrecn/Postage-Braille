using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace IO
{
    public class ActionRebinder
    {
        private GameActions _actions;
        private bool _rebinding = false;
        private UILayer _actionLayer;
        
        
        private InputActionsPanel _inputActionsPanel;
        
        private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;
        //private 
        
        
        public ActionRebinder(GameActions actions)
        {
            _actions = actions;
            
            _inputActionsPanel = GameObject.FindAnyObjectByType<InputActionsPanel>();
            if (_inputActionsPanel == null)
            {
                Debug.LogWarning("No InputActionsPanel found");
            }
        }

        public void ListActions(InputActionMap actionMap)
        {
            //debugging solution
            if (_inputActionsPanel == null)
            {
                return;
            }
            //end
            
            _inputActionsPanel.ClearAll();
            
            List<Focusable> buttons = new List<Focusable>();
            
            foreach (InputAction inputAction in actionMap)
            {
                var button = _inputActionsPanel.AddButton(inputAction, RebindAction);
                buttons.Add(button);
            }
            _actionLayer = new UILayer("ActionLayer", buttons);
            UIManager.Instance.AddLayer(_actionLayer);
        }

        public void RebindAction(InputAction inputAction, FocusableRebindOption button)
        {
            if(!_rebinding)
            {
                _rebinding = true;
                MultimodalInputManager.Instance.DisableInput(MultimodalInputManager.InputType.Navigation);
                button.SetBindingText("Click any button");
                _rebindingOperation =
                    inputAction.PerformInteractiveRebinding().OnComplete(operation => RebindCompleted(inputAction, button)).Start();
            }
        }

        private void RebindCompleted(InputAction inputAction, FocusableRebindOption button)
        {
            Debug.Log("RebindCompleted");
            _rebindingOperation.Dispose();
            button.SetBindingText(inputAction.bindings[0].effectivePath);
            Debug.Log(inputAction.bindings[0].path);
            Debug.Log(inputAction.bindings[0].overridePath);
            Debug.Log(inputAction.bindings[0].effectivePath);
            MultimodalInputManager.Instance.EnableInput(MultimodalInputManager.InputType.Navigation);
            _rebinding = false;
        }
    }
}

