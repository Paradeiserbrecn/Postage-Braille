using Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IO
{
    public class BrailleSettingsInput : AbstractInput
    {
        public override void Enable()
        {
            Actions.BrailleSettings.BrailleSizeUp.started += OnBrailleSizeUp;
            Actions.BrailleSettings.BrailleSizeDown.started += OnBrailleSizeDown;
            Actions.BrailleSettings.DotSizeUp.started += OnDotSizeUp;
            Actions.BrailleSettings.DotSizeDown.started += OnDotSizeDown;
            Actions.BrailleSettings.BrailleSpacingUp.started += OnBrailleSpacingUp;
            Actions.BrailleSettings.BrailleSpacingDown.started += OnBrailleSpacingDown;
            Actions.BrailleSettings.LineSpacingUp.started += OnLineSpacingUp;
            Actions.BrailleSettings.LineSpacingDown.started += OnLineSpacingDown;
            Actions.BrailleSettings.Enable();
        }

        public override void Disable()
        {
            Actions.BrailleSettings.BrailleSizeUp.started -= OnBrailleSizeUp;
            Actions.BrailleSettings.BrailleSizeDown.started -= OnBrailleSizeDown;
            Actions.BrailleSettings.DotSizeUp.started -= OnDotSizeUp;
            Actions.BrailleSettings.DotSizeDown.started -= OnDotSizeDown;
            Actions.BrailleSettings.BrailleSpacingUp.started -= OnBrailleSpacingUp;
            Actions.BrailleSettings.BrailleSpacingDown.started -= OnBrailleSpacingDown;
            Actions.BrailleSettings.LineSpacingUp.started -= OnLineSpacingUp;
            Actions.BrailleSettings.LineSpacingDown.started -= OnLineSpacingDown;
            Actions.BrailleSettings.Disable();
        }


        private void OnBrailleSizeUp(InputAction.CallbackContext context)
        {
            GlobalSettings.BrailleSize = System.Math.Clamp(GlobalSettings.BrailleSize + GlobalSettings.BrailleSizeIncrement, GlobalSettings.MinBrailleSize , GlobalSettings.MaxBrailleSize);
            IOEventManager.InvokeBrailleSizeChanged();
        }

        private void OnBrailleSizeDown(InputAction.CallbackContext context)
        {
            GlobalSettings.BrailleSize = System.Math.Clamp(GlobalSettings.BrailleSize - GlobalSettings.BrailleSizeIncrement, GlobalSettings.MinBrailleSize , GlobalSettings.MaxBrailleSize);
            IOEventManager.InvokeBrailleSizeChanged();
        }

        private void OnDotSizeUp(InputAction.CallbackContext context)
        {
            GlobalSettings.DotSize = System.Math.Clamp(GlobalSettings.DotSize + GlobalSettings.DotSizeIncrement, GlobalSettings.MinDotSize , GlobalSettings.MaxDotSize);
            IOEventManager.InvokeDotSizeChanged();
        }

        private void OnDotSizeDown(InputAction.CallbackContext context)
        {
            GlobalSettings.DotSize = System.Math.Clamp(GlobalSettings.DotSize - GlobalSettings.DotSizeIncrement, GlobalSettings.MinDotSize , GlobalSettings.MaxDotSize);
            IOEventManager.InvokeDotSizeChanged();
        }

        private void OnBrailleSpacingUp(InputAction.CallbackContext context)
        {
            GlobalSettings.BrailleSpacing = System.Math.Clamp(GlobalSettings.BrailleSpacing + GlobalSettings.BrailleSpacingIncrement, GlobalSettings.MinBrailleSpacing , GlobalSettings.MaxBrailleSpacing);
            IOEventManager.InvokeBrailleSpacingChanged();
        }

        private void OnBrailleSpacingDown(InputAction.CallbackContext context)
        {
            GlobalSettings.BrailleSpacing = System.Math.Clamp(GlobalSettings.BrailleSpacing - GlobalSettings.BrailleSpacingIncrement, GlobalSettings.MinBrailleSpacing , GlobalSettings.MaxBrailleSpacing);
            IOEventManager.InvokeBrailleSpacingChanged();
        }

        private void OnLineSpacingUp(InputAction.CallbackContext context)
        {
            GlobalSettings.LineSpacing = System.Math.Clamp(GlobalSettings.LineSpacing + GlobalSettings.LineSpacingIncrement, GlobalSettings.MinLineSpacing , GlobalSettings.MaxLineSpacing);
            IOEventManager.InvokeLineSpacingChanged();
        }

        private void OnLineSpacingDown(InputAction.CallbackContext context)
        {
            GlobalSettings.LineSpacing = System.Math.Clamp(GlobalSettings.LineSpacing - GlobalSettings.LineSpacingIncrement, GlobalSettings.MinLineSpacing , GlobalSettings.MaxLineSpacing);
            IOEventManager.InvokeLineSpacingChanged();
        }
    }
}
