using IO;
using UnityEngine;

namespace Settings
{
    public class BrailleSettings : MonoBehaviour
    {
        private BrailleControls _controls;

        private void Awake()
        {
            _controls = new BrailleControls();
        }

        public void OnEnable()
        {
            _controls.Enable();
        }

        public void OnDisable()
        {
            _controls.Disable();
        }

        public void Update()
        {
            if (_controls.BrailleSettings.BrailleSizeUp.triggered)
            {
                GlobalSettings.BrailleSize = System.Math.Clamp(GlobalSettings.BrailleSize + GlobalSettings.BrailleSizeIncrement, GlobalSettings.MinBrailleSize , GlobalSettings.MaxBrailleSize);
                IOEventManager.InvokeBrailleSizeChanged();
            }
        
            if (_controls.BrailleSettings.BrailleSizeDown.triggered)
            {
                GlobalSettings.BrailleSize = System.Math.Clamp(GlobalSettings.BrailleSize - GlobalSettings.BrailleSizeIncrement, GlobalSettings.MinBrailleSize , GlobalSettings.MaxBrailleSize);
                IOEventManager.InvokeBrailleSizeChanged();
            }

            if (_controls.BrailleSettings.DotSizeUp.triggered)
            {
                GlobalSettings.DotSize = System.Math.Clamp(GlobalSettings.DotSize + GlobalSettings.DotSizeIncrement, GlobalSettings.MinDotSize , GlobalSettings.MaxDotSize);
                IOEventManager.InvokeDotSizeChanged();
            }

            if (_controls.BrailleSettings.DotSizeDown.triggered)
            {
                GlobalSettings.DotSize = System.Math.Clamp(GlobalSettings.DotSize - GlobalSettings.DotSizeIncrement, GlobalSettings.MinDotSize , GlobalSettings.MaxDotSize);
                IOEventManager.InvokeDotSizeChanged();
            }
        
            if (_controls.BrailleSettings.BrailleSpacingUp.triggered)
            {
                GlobalSettings.BrailleSpacing = System.Math.Clamp(GlobalSettings.BrailleSpacing + GlobalSettings.BrailleSpacingIncrement, GlobalSettings.MinBrailleSpacing , GlobalSettings.MaxBrailleSpacing);
                IOEventManager.InvokeBrailleSpacingChanged();
            }
        
            if (_controls.BrailleSettings.BrailleSpacingDown.triggered)
            {
                GlobalSettings.BrailleSpacing = System.Math.Clamp(GlobalSettings.BrailleSpacing - GlobalSettings.BrailleSpacingIncrement, GlobalSettings.MinBrailleSpacing , GlobalSettings.MaxBrailleSpacing);
                IOEventManager.InvokeBrailleSpacingChanged();
            }
        
            if (_controls.BrailleSettings.LineSpacingUp.triggered)
            {
                GlobalSettings.LineSpacing = System.Math.Clamp(GlobalSettings.LineSpacing + GlobalSettings.LineSpacingIncrement, GlobalSettings.MinLineSpacing , GlobalSettings.MaxLineSpacing);
                IOEventManager.InvokeLineSpacingChanged();
            }

            if (_controls.BrailleSettings.LineSpacingDown.triggered)
            {
                GlobalSettings.LineSpacing = System.Math.Clamp(GlobalSettings.LineSpacing - GlobalSettings.LineSpacingIncrement, GlobalSettings.MinLineSpacing , GlobalSettings.MaxLineSpacing);
                IOEventManager.InvokeLineSpacingChanged();
            }
        }
    }
}
