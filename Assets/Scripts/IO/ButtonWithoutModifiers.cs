using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using System;

namespace IO
{
    
    #if UNITY_EDITOR
    [InitializeOnLoad]
    #endif
    
    [DisplayStringFormat("{Key}+{AndNotKey}")]
    public class ButtonWithoutModifier : InputBindingComposite<float>{
        
        [InputControl(layout = "Button")]
        public int Key;
        [InputControl(layout = "Button")]
        public int AndNotKey;
        
        
        
        public override float ReadValue(ref InputBindingCompositeContext context)
        {
            var keyValue = context.ReadValue<float>(Key);
            var andNotKeyValue = context.ReadValue<float>(AndNotKey);
            
            return context.ReadValueAsButton(Key) &&
                   !context.ReadValueAsButton(AndNotKey)
                ? 1f
                : 0f;
        }
        
        public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
        {
            return ReadValue(ref context);
        }

        static ButtonWithoutModifier()
        {
            InputSystem.RegisterBindingComposite<ButtonWithoutModifier>();
        }
        
        [RuntimeInitializeOnLoadMethod]
        static void Init(){}
    }
}
