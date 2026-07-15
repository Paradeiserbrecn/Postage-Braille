using System;
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
        [SerializeField] public UIManager gameUI;
        [SerializeField] public UIManager settingsUI;
        [SerializeField] public UIManager packagePickerUI;
        
        public UIManager CurrentUI()
        {
            return currentScreen switch
            {
                Screen.GameScreen => gameUI,
                Screen.SettingsScreen => settingsUI,
                Screen.PackagePickerScreen => packagePickerUI,
                _ => null
            };
        }

        public static SceneControl Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
