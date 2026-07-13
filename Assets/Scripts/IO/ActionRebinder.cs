using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;
using Braille;

namespace IO
{
    public class ActionRebinder
    {
        private GameActions _actions;
        private bool _rebinding = false;
        private UILayer _actionLayer;
        
        
        private InputActionsPanel _inputActionsPanel;
        
        private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;
        
        public ActionRebinder(GameActions actions) 
        {
            _actions = actions;
            
            _inputActionsPanel = GameObject.FindAnyObjectByType<InputActionsPanel>();
            if (_inputActionsPanel == null)
            {
                Debug.LogWarning("No InputActionsPanel found");
            }
        }

        //when we have implemented scene changes InputActionsPanel should be specified using this method instead of the find in the constructor
        public void SpecifyInputActionsPanel(InputActionsPanel panel)
        {
            if (_inputActionsPanel == null)
            {
                Debug.LogWarning("InputActionsPanel is null");
                return;
            }
            _inputActionsPanel = panel;
        }

        public void ListActions(InputActionMap actionMap)
        {
            if (_inputActionsPanel == null)
            {
                Debug.LogWarning("no InputActionsPanel found");
                return;
            }
            
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
                IOEventManager.AssistiveOutput("Neuen Knopf drücken", AssistiveOutput.OutputType.Both);
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
            MultimodalInputManager.Instance.EnableInput(MultimodalInputManager.InputType.Navigation);
            _rebinding = false;
            IOEventManager.AssistiveOutput("Neuer knopf ist " + inputAction.bindings[0].effectivePath, AssistiveOutput.OutputType.Both);
        }
    }
}

