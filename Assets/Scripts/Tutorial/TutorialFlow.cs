using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Braille;
using IO;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace Tutorial
{

    /// <summary>
    /// Does flow control of the tutorial
    /// Each Focusable in the tutorial should have its own method that gets invoked on focus and submit.
    /// To control what will be output TutorialFlow.State is used.
    /// To limit user input during the tutorial <see cref="TutorialInput"/> is used and enabled in <see cref="MultimodalInputManager"/>
    /// </summary>
    public class TutorialFlow : MonoBehaviour
    {
        /// <summary>
        /// The different states the tutorial can be in.
        /// </summary>
        public enum State
        {
            Start,
            PickAnswer,
            AfterAnswerPicked,
            FirstSwitchPackButton,
            PackagePickerMenuNavigation,
            OnSelectedUnit,
            AfterUnitPicked,
            AfterPackageScreen,
            GameMenuNavigation,
            SettingsMenuNavigation,
            SettingsMenuIntroduction
        }

        public State currentState;


        public static TutorialFlow Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UIManager.Instance.SwitchLayer((int)Layer.Letter);
        }

        // TODO: This would optimally be populated with references to the different UIManagers in the scene
        private enum Layer
        {
            Letter = 1,
            QuestionLayer = 2,
            GameMenu = 0,
            PackageMenu = 0,
            PackageList = 1,
            SettingsMenu = 0,
        }

        #region GameScreen

        public void Letter()
        {
            switch (currentState)
            {
                case State.Start:
                    TutorialOutputs.FirstLetterOutput();
                    break;
                case State.AfterAnswerPicked:
                    TutorialOutputs.SecondLetterOutput();
                    break;
                case State.AfterPackageScreen:
                    TutorialOutputs.ThirdLetterOutput();
                    break;
            }
        }

        public void FirstAnswer()
        {
            switch (currentState)
            {
                case State.Start:
                    TutorialOutputs.FirstAnswerOutput();
                    currentState = State.PickAnswer;
                    break;
                case State.PickAnswer:
                    TutorialOutputs.FirstAnswerOutput();
                    break;
                case State.AfterAnswerPicked:
                case State.AfterPackageScreen:
                    TutorialOutputs.SecondFirstAnswerOutput();
                    break;
            }
        }

        public void SecondAnswerFocus()
        {
            switch (currentState)
            {
                case State.PickAnswer:
                    TutorialOutputs.SecondAnswerFocus();
                    break;
            }
        }

        public void SecondAnswerSubmit()
        {
            switch (currentState)
            {
                case State.PickAnswer:
                    var outputText = TutorialOutputs.SecondAnswerSubmit();
                    AssistiveOutput.WaitForSpeakingToFinish(() =>
                        {
                            currentState = State.AfterAnswerPicked;
                            UIManager.Instance.SwitchLayer((int)Layer.Letter);
                        },
                        outputText);

                    break;
            }
        }

        public void ThirdAnswer()
        {
            switch (currentState)
            {
                case State.PickAnswer:
                    TutorialOutputs.ThirdAnswerOutput();
                    break;
            }
        }

        public void SwitchPackFocus()
        {
            switch (currentState)
            {
                case State.AfterAnswerPicked:
                    currentState = State.FirstSwitchPackButton;
                    TutorialOutputs.FirstSwitchPackButtonOutput();
                    break;
                case State.AfterPackageScreen:
                    currentState = State.GameMenuNavigation;
                    TutorialOutputs.SecondSwitchPackButtonOutput();
                    break;
                case State.GameMenuNavigation:
                    TutorialOutputs.ThirdSwitchPackButtonOutput();
                    break;
            }
        }

        public void SwitchPackSubmit()
        {
            switch (currentState)
            {
                case State.FirstSwitchPackButton:
                    currentState = State.PackagePickerMenuNavigation;
                    SceneControl.TransitionToPackagePickerScreenInTutorial();
                    break;
                case State.GameMenuNavigation:
                    TutorialOutputs.ThirdSwitchPackButtonOutput();
                    break;
            }
        }

        public void QuestionDirection()
        {
            switch (currentState)
            {
                case State.GameMenuNavigation:
                    TutorialOutputs.QuestionDirectionOutput();
                    break;
            }
        }

        public void QuestionMode()
        {
            switch (currentState)
            {
                case State.GameMenuNavigation:
                    TutorialOutputs.QuestionModeOutput();
                    break;
            }
        }

        public void SettingsFocus()
        {
            switch (currentState)
            {
                case State.GameMenuNavigation:
                    TutorialOutputs.SettingsOutput();
                    break;
            }
        }

        public void SettingsSubmit()
        {
            switch (currentState)
            {
                case State.GameMenuNavigation:
                    currentState = State.SettingsMenuIntroduction;
                    SceneControl.TransitionToSettingsScreen();
                    break;
            }
        }

        #endregion

        #region PackageScreen

        public void PackageBackButtonFocus()
        {
            switch (currentState)
            {
                case State.PackagePickerMenuNavigation:
                    TutorialOutputs.FirstPackageBackButtonOutput();
                    break;
                case State.AfterUnitPicked:
                    TutorialOutputs.SecondPackageBackButtonOutput();
                    break;
            }
        }

        public void PackageBackButtonSubmit()
        {
            switch (currentState)
            {
                case State.PackagePickerMenuNavigation:
                    TutorialOutputs.FirstPackageBackButtonOutput();
                    break;
                case State.AfterUnitPicked:
                    currentState = State.AfterPackageScreen;
                    SceneControl.TransitionToGameScreenInTutorial();
                    UIManager.Instance.SwitchLayer((int)Layer.Letter);

                    break;
            }
        }

        public void SaveButton()
        {
            switch (currentState)
            {
                case State.PackagePickerMenuNavigation:
                    TutorialOutputs.FirstSaveButtonOutput();
                    break;
                case State.AfterUnitPicked:
                    TutorialOutputs.SecondSaveButtonOutput();
                    break;
            }
        }

        public void SelectedUnit()
        {
            switch (currentState)
            {
                case State.PackagePickerMenuNavigation:
                    currentState = State.OnSelectedUnit;
                    TutorialOutputs.FirstSelectedUnitOutput();
                    break;
                case State.OnSelectedUnit:
                    TutorialOutputs.FirstSelectedUnitOutput();
                    break;
                case State.AfterUnitPicked:
                    TutorialOutputs.SecondSelectedUnitOutput();
                    break;
            }
        }

        public void Unitpicker()
        {
            switch (currentState)
            {
                case State.OnSelectedUnit:
                    TutorialOutputs.FirstUnitpickerOutput();
                    currentState = State.AfterUnitPicked;
                    break;
                case State.AfterUnitPicked:
                    TutorialOutputs.SecondUnitpickerOutput();
                    break;
            }
        }

        #endregion

        #region SettingsScreen

        public void RebindBackFocus()
        {
            switch (currentState)
            {
                case State.SettingsMenuIntroduction:
                    TutorialOutputs.FirstRebindBackOutput();
                    currentState = State.SettingsMenuNavigation;
                    break;
                case State.SettingsMenuNavigation:
                    TutorialOutputs.SecondRebindBackOutput();
                    break;
            }
        }

        [SuppressMessage("Compiler",
            "CS4014:Because this call is not awaited, execution of the current method continues before the call is completed")]
        public void RebindBackSubmit()
        {
            switch (currentState)
            {
                case State.SettingsMenuIntroduction:
                    TutorialOutputs.FirstRebindBackOutput();
                    currentState = State.SettingsMenuNavigation;
                    break;
                case State.SettingsMenuNavigation:
                    var text = TutorialOutputs.RebindBackOutputSubmit();

                    MultimodalInputManager.Instance?.DisableInput(MultimodalInputManager.InputType.Tutorial);
                    AssistiveOutput.WaitForSpeakingToFinish(() => SceneManager.LoadScene("Scenes/LetterSortingScene"),
                        text);
                    break;
            }
        }

        public void Navigation()
        {
            if (currentState == State.SettingsMenuNavigation)
            {
                TutorialOutputs.NavigationOutput();
            }
        }

        public void BrailleSettings()
        {
            if (currentState == State.SettingsMenuNavigation)
            {
                TutorialOutputs.BrailleSettingsOutput();
            }
        }

        public void PerkinsBrailler()
        {
            if (currentState == State.SettingsMenuNavigation)
            {
                TutorialOutputs.PerkinsBraillerOutput();
            }
        }

        public void ResetSettings()
        {
            if (currentState == State.SettingsMenuNavigation)
            {
                TutorialOutputs.ResetOutput();
            }
        }

        #endregion
    }
}
