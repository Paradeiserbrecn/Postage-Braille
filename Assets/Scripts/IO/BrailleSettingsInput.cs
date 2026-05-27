using Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IO
{
    public class BrailleSettingsInput : AbstractInput
    {
        private BrailleControls _controls = new();

        public override void Enable()
        {
            _controls.BrailleSettings.BrailleSizeUp.started += OnBrailleSizeUp;
            _controls.BrailleSettings.BrailleSizeDown.started += OnBrailleSizeDown;
            _controls.BrailleSettings.DotSizeUp.started += OnDotSizeUp;
            _controls.BrailleSettings.DotSizeDown.started += OnDotSizeDown;
            _controls.BrailleSettings.BrailleSpacingUp.started += OnBrailleSpacingUp;
            _controls.BrailleSettings.BrailleSpacingDown.started += OnBrailleSpacingDown;
            _controls.BrailleSettings.LineSpacingUp.started += OnLineSpacingUp;
            _controls.BrailleSettings.LineSpacingDown.started += OnLineSpacingDown;
            _controls.BrailleSettings.Enable();
        }

        public override void Disable()
        {
            _controls.BrailleSettings.BrailleSizeUp.started -= OnBrailleSizeUp;
            _controls.BrailleSettings.BrailleSizeDown.started -= OnBrailleSizeDown;
            _controls.BrailleSettings.DotSizeUp.started -= OnDotSizeUp;
            _controls.BrailleSettings.DotSizeDown.started -= OnDotSizeDown;
            _controls.BrailleSettings.BrailleSpacingUp.started -= OnBrailleSpacingUp;
            _controls.BrailleSettings.BrailleSpacingDown.started -= OnBrailleSpacingDown;
            _controls.BrailleSettings.LineSpacingUp.started -= OnLineSpacingUp;
            _controls.BrailleSettings.LineSpacingDown.started -= OnLineSpacingDown;
            _controls.BrailleSettings.Disable();
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
