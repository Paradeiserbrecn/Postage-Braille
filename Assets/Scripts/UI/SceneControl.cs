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
        public static SceneControl Instance;
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

        public static void TransitionToGameScreenInTutorial()
        {
            CurrentUI.CurrentLayer.Unfocus();
            Instance.currentScreen = Screen.GameScreen;
            // We will not FocusFirst in the tutorial, as we have to manually re-focus
            // the letter to follow the exact flow the game has in a regular scenario
            // Since we do not have a GameManager and thus cannot invoke NextQuestion, letter is never re-focused by just switching scenes

            // CurrentUI.CurrentLayer.FocusFirst();
            Instance.mainCamera.transform.position = Instance.gameCameraTransform.position;
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

        public static void TransitionToPackagePickerScreenInTutorial()
        {
            CurrentUI.CurrentLayer.Unfocus();
            Instance.currentScreen = Screen.PackagePickerScreen;
            CurrentUI.CurrentLayer.FocusFirst();
            Instance.mainCamera.transform.position = Instance.packagePickerCameraTransform.position;
        }
    }
}
