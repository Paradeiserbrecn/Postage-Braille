using System;
using Data;
using UnityEngine;

namespace UI
{
    public enum Screen
    {
        GameScreen,
        SettingsScreen,
        PackagePickerScreen
    }

    public class SceneControl : MonoBehaviour
    {
        [SerializeField] public Screen currentScreen;
        [SerializeField] private Camera mainCamera;
        [SerializeField] public UIManager gameUI;
        [SerializeField] public UIManager settingsUI;
        [SerializeField] public UIManager packagePickerUI;

        [SerializeField] public Transform gameCameraTransform;
        [SerializeField] public Transform settingsCameraTransform;
        [SerializeField] public Transform packagePickerCameraTransform;
        
        
        public static UIManager CurrentUI => Instance.currentScreen switch
        {
            Screen.GameScreen => Instance.gameUI,
            Screen.SettingsScreen => Instance.settingsUI,
            Screen.PackagePickerScreen => Instance.packagePickerUI,
            _ => null
        };

        public static SceneControl Instance;

        private void Awake()
        {
            Instance = this;
        }

        public static void TransitionToGameScreen()
        {
            CurrentUI.CurrentLayer.Unfocus();
            Instance.currentScreen = Screen.GameScreen;
            CurrentUI.CurrentLayer.FocusFirst();
            Instance.mainCamera.transform.position = Instance.gameCameraTransform.position;
            GameManager.Instance.NextQuestion();
        }

        public static void TransitionToSettingsScreen()
        {
            CurrentUI.CurrentLayer.Unfocus();
            Instance.currentScreen = Screen.SettingsScreen;
            CurrentUI.CurrentLayer.FocusFirst();
            Instance.mainCamera.transform.position = Instance.settingsCameraTransform.position;
        }

        public static void TransitionToPackagePickerScreen()
        {
            CurrentUI.CurrentLayer.Unfocus();
            Instance.currentScreen = Screen.PackagePickerScreen;
            CurrentUI.CurrentLayer.FocusFirst();
            Instance.mainCamera.transform.position = Instance.packagePickerCameraTransform.position;
            
            LetterPackagePicker.Instance.ScrollToTop();
            LetterPackagePicker.Instance.PopulateWithCurrentLanguagePackage();
            LetterPackagePicker.Instance.SelectLetterUnit(LetterPackages.Instance.CurrentPackageUnit);
        }


    }
}
