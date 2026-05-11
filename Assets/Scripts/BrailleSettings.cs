using UnityEngine;

public class BrailleSettings : MonoBehaviour
{
    private BrailleControls controls;

    void Awake()
    {
        controls = new BrailleControls();
    }
    
    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
    void Update()
    {
        if (controls.BrailleSettings.BrailleSizeUp.triggered)
        {
            GlobalSettings.BrailleSize = System.Math.Clamp(GlobalSettings.BrailleSize + GlobalSettings.BrailleSizeIncrement, GlobalSettings.MinBrailleSize , GlobalSettings.MaxBrailleSize);
            IOEventManager.InvokeBrailleSizeChanged();
            Debug.Log("InvokeBrailleSizeChanged to: " + GlobalSettings.BrailleSize);
        }
        
        if (controls.BrailleSettings.BrailleSizeDown.triggered)
        {
            GlobalSettings.BrailleSize = System.Math.Clamp(GlobalSettings.BrailleSize - GlobalSettings.BrailleSizeIncrement, GlobalSettings.MinBrailleSize , GlobalSettings.MaxBrailleSize);
            IOEventManager.InvokeBrailleSizeChanged();
            Debug.Log("InvokeBrailleSizeChanged to: " + GlobalSettings.BrailleSize);
        }

        if (controls.BrailleSettings.DotSizeUp.triggered)
        {
            GlobalSettings.DotSize = System.Math.Clamp(GlobalSettings.DotSize + GlobalSettings.DotSizeIncrement, GlobalSettings.MinDotSize , GlobalSettings.MaxDotSize);
            IOEventManager.InvokeDotSizeChanged();
            Debug.Log("InvokeDotSizeChanged to: " + GlobalSettings.DotSize);
        }

        if (controls.BrailleSettings.DotSizeDown.triggered)
        {
            GlobalSettings.DotSize = System.Math.Clamp(GlobalSettings.DotSize - GlobalSettings.DotSizeIncrement, GlobalSettings.MinDotSize , GlobalSettings.MaxDotSize);
            IOEventManager.InvokeDotSizeChanged();
            Debug.Log("InvokeDotSizeChanged to: " + GlobalSettings.DotSize);
        }
        
        if (controls.BrailleSettings.BrailleSpacingUp.triggered)
        {
            GlobalSettings.BrailleSpacing = System.Math.Clamp(GlobalSettings.BrailleSpacing + GlobalSettings.BrailleSpacingIncrement, GlobalSettings.MinBrailleSpacing , GlobalSettings.MaxBrailleSpacing);
            IOEventManager.InvokeBrailleSpacingChanged();
            Debug.Log("InvokeBrailleSpacingChanged to: " + GlobalSettings.BrailleSpacing);
        }
        
        if (controls.BrailleSettings.BrailleSpacingDown.triggered)
        {
            GlobalSettings.BrailleSpacing = System.Math.Clamp(GlobalSettings.BrailleSpacing - GlobalSettings.BrailleSpacingIncrement, GlobalSettings.MinBrailleSpacing , GlobalSettings.MaxBrailleSpacing);
            IOEventManager.InvokeBrailleSpacingChanged();
            Debug.Log("InvokeBrailleSpacingChanged to: " + GlobalSettings.BrailleSpacing);
        }
        
        if (controls.BrailleSettings.LineSpacingUp.triggered)
        {
            GlobalSettings.LineSpacing = System.Math.Clamp(GlobalSettings.LineSpacing + GlobalSettings.LineSpacingIncrement, GlobalSettings.MinLineSpacing , GlobalSettings.MaxLineSpacing);
            IOEventManager.InvokeLineSpacingChanged();
            Debug.Log("InvokeLineSpacingChanged to: " + GlobalSettings.LineSpacing);
        }

        if (controls.BrailleSettings.LineSpacingDown.triggered)
        {
            GlobalSettings.LineSpacing = System.Math.Clamp(GlobalSettings.LineSpacing - GlobalSettings.LineSpacingIncrement, GlobalSettings.MinLineSpacing , GlobalSettings.MaxLineSpacing);
            IOEventManager.InvokeLineSpacingChanged();
            Debug.Log("InvokeLineSpacingChanged to: " + GlobalSettings.LineSpacing);
        }
    }
}
