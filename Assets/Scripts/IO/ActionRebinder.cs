using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;
using Braille;

namespace IO
{
    public class ActionRebinder:MonoBehaviour
    {
        private bool _rebinding = false;
        private UILayer _actionLayer;
        
        private InputActionsPanel _inputActionsPanel;
        
        private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

        private int _actionLayerIndex = -1;
        
        private Dictionary<string, InputActionMap> _rebindableActionMaps = new();


        public static ActionRebinder Instance;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _rebindableActionMaps.Add("Navigation", MultimodalInputManager.Instance.Actions.Navigation);
            _rebindableActionMaps.Add("BrailleSettings", MultimodalInputManager.Instance.Actions.BrailleSettings);
            _rebindableActionMaps.Add("PerkinsBrailler", MultimodalInputManager.Instance.Actions.PerkinsBrailer);
        }

        public void SpecifyInputActionsPanel(InputActionsPanel panel)
        {
            _inputActionsPanel = panel;
        }

        public void ListActions(String actionMapName)
        {
            if (!_rebindableActionMaps.ContainsKey(actionMapName))
            {
                Debug.LogError($"ActionMap {actionMapName} not found");
                return;
            }
            ListActions(_rebindableActionMaps[actionMapName]);
        }

        /// <summary>
        /// Adds a FocusableRebindOption for each action in the passed InputActionMap. 
        /// </summary>
        /// <param name="actionMap"></param>
        public void ListActions(InputActionMap actionMap)
        {
            if (_inputActionsPanel == null)
            {
                Debug.LogWarning("no InputActionsPanel found");
                return;
            }

            if (actionMap == null)
            {
                Debug.LogWarning("ActionMap is null");
                return;
            }
            
            _inputActionsPanel.ClearAll();
            
            List<Focusable> buttons = new List<Focusable>();
            
            foreach (InputAction inputAction in actionMap)
            {
                var button = _inputActionsPanel.AddButton(inputAction, RebindAction);
                buttons.Add(button);
            }
            if (_actionLayerIndex >= 0) SceneControl.Instance.settingsUI.RemoveLayer(_actionLayerIndex);
            _actionLayer = new UILayer("ActionLayer", buttons);
            _actionLayerIndex = SceneControl.Instance.settingsUI.AddLayer(_actionLayer);
            
            Canvas.ForceUpdateCanvases();
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

        public void ClearLayer()
        {
            _inputActionsPanel.ClearAll();
            if (_actionLayerIndex >= 0) SceneControl.Instance.settingsUI.RemoveLayer(_actionLayerIndex);
            _actionLayerIndex = -1;
        }
    }
}

