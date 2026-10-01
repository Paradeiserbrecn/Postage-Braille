using System.Collections.Generic;
using Braille;
using IO;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{
    public static class TutorialOutputs
    {
        public static void FirstLetterOutput()
        {
            Output(
                "Das ist der Spiel Bildschirm. Am Anfang ist immer der Brief ausgewählt. Dieser beinhaltet die aktuelle Frage. Drücke die Tabulator taste um zu den Antwortmöglichkeiten zu gelangen.");
        }

        public static void FirstAnswerOutput()
        {
            Output(
                "Das ist die erste Antwortmöglichkeit. Mit Pfeiltaste rechts und Pfeiltaste links kann man die Antworten navigieren. Wähle doch mal die zweite Antwort aus.");
        }

        public static void SecondAnswerFocus()
        {
            Output("Das ist die Zweite Antwort. Mit der Enter-Taste kannst du die Antwort bestätigen. ");
        }

        public static void SecondAnswerSubmit()
        {
            Output("Du hast die Antwort bestätigt und bekommst danach eine Rückmeldung!");
            AssistiveOutput.WaitForSpeakingToFinish(() =>
            {
                Debug.Log("should switch to letter");
                UIManager.Instance.SwitchLayer(1);
            },"Du hast die Antwort bestätigt und bekommst danach eine Rückmeldung!");
        }

        public static void ThirdAnswerOutput()
        {
            Output(
                "Das ist die Dritte Antwort. Es gibt immer drei Antwortmöglichkeiten. Gehe doch zur Zweiten zurück. Nutze dafür die linke Pfeiltaste");
        }

        public static void SecondLetterOutput()
        {
            Output(
                "Nach beantworten der Frage kommt man zum Brief zurück. Verwende die Tabulator Taste um zum Menü zu gelangen.");
        }

        public static void SecondFirstAnswerOutput()
        {
            Output("Das ist die erste Antwortmöglichkeit. Drücke die Tabulator-Taste erneut.");
        }

        public static void FirstSwitchPackButtonOutput()
        {
            Output(
                "Das ist die Spielmenü-Leiste. Zuerst ist der \"Einheit Auswählen\" Knopf ausgewählt. Drücke die Enter-Taste um eine andere Einheit auszuwählen.");
        }

        public static void FirstPackageBackButtonOutput()
        {
            Output(
                "Auf diesem Bildschirm kannst du die Einheiten auswählen. Am anfang ist der Zurück Knopf ausgewählt. Mit den Pfeiltasten kannst du im Menü navigieren.");
        }


        public static void FirstSaveButtonOutput()
        {
            Output("Dieser Knopf speichert deinen Fortschritt. Vergiss nicht deinen Fortschritt zu Speichern.");
        }

        public static void FirstSelectedUnitOutput()
        {
            Output(
                "Hier kannst du die ausgewählte einheit Überprüfen. Das sind alle Buttons in der Menüleiste. Mit der Tabulator-Taste wechselst du auf die Einheitenliste.");
        }

        public static void FirstUnitpickerOutput()
        {
            Output(
                "Das ist die Einheitenliste. Mit der Enter-Taste kann man eine Einheit auswählen. Versuche jetzt selbstständig zum Spielbildschirm zurück zu navigierten.");
        }

        public static void SecondUnitpickerOutput()
        {
            Output("Einheit eins. In der Einführung kannst du nur diese Auswählen.");
        }

        public static void SecondPackageBackButtonOutput()
        {
            Output("Zurück");
        }

        public static void SecondSaveButtonOutput()
        {
            Output("Fortschritt speichern.");
        }

        public static void SecondSelectedUnitOutput()
        {
            Output("Das ist die ausgewählte Einheit. In der Einführung gibt es nur Einheit eins.");
        }

        public static void ThirdLetterOutput()
        {
            Output(
                "Du bist nun wieder am Spielbildschirm. Du bist nun mit dem Hauptteil des Spiels vertraut, es gibt aber noch eine andere Funktion. Navigiere in die menüleiste.");
        }

        public static void SecondSwitchPackButtonOutput()
        {
            Output(
                "Du bist nun wieder in der Menüleiste des Spielbildschirms. Den ersten Knopf kennst du bereits, mit diesem sind wir zum Bildschirm zum auswählen von Einheiten gekommen. Navigiere zu den anderen Knöpfen um zu lernen was sie können.");
        }

        public static void ThirdSwitchPackButtonOutput()
        {
            Output(
                "Dieser Knopf bringt dich in den Bildschirm zum Auswählen von Einheiten, navigiere zum Einstellungen Bildschirm um die Einleitung fortzufahren.");
        }

        public static void QuestionDirectionOutput()
        {
            Output(
                "Dieser Knopf ändert die Fragerichtung. Anfangs ist die Frage in Braille dargestellt und die Antworten durch Schwarzschrift und Audio. Nach drücken des Knopfes wird zur umgekehrten Fragerichtung gewechselt.");
        }

        public static void QuestionModeOutput()
        {
            Output(
                "Mit diesem Knopf änderst du die Art der Fragen. Anfangs werden einzelne Buchstaben der aktuellen Einheit abgefragt. Alternativ kann man Wörter aus allen gelernten Buchstaben abfragen. Vergiss nicht hin und wieder die Art zu wechseln.");
        }

        public static void SettingsOutput()
        {
            Output(
                "Dieser Knopf führt zum Einstellungsmenü. Dücke die Entertaste um zu den Einstellungen zu gelangen.");
        }

        public static void FirstRebindBackOutput()
        {
            Output(
                "Du befindest dich jetzt im Einstellungsbildschirm. Anfangs ist der Zurück Knopf ausgewählt. Navigiere doch durch die anderen Menüpunkte.");
        }

        public static void NavigationOutput()
        {
            Output(
                "Hiermit öffnest du die Tastenbelegung für die Navigation. Um eine Tastenbelegung zu ändern wähle in der Liste die zu ändernde Aktion aus und drücke die Enter-Taste. Danach drücke die gewünschte Taste. Die Aktion wird von nun an mit der neuen Taste ausgelöst.");
        }

        public static void BrailleSettingsOutput()
        {
            Output(
                "Hiermit öffnest du die Tastenbelegung für die Braille Darstellung. Das ändern der Tastenbelegung erfolgt gleich als bei der Navigation.");
        }

        public static void PerkinsBraillerOutput()
        {
            Output(
                "Hiermit öffnest du die Tastenbelegung für den Perkinsbrailler. Anfangs sind die Perkinsbraillertasten SDF und JKL an der Tastatur.");
        }

        public static void ResetOutput()
        {
            Output(
                "Dieser Knopf setzt alle Tastenbelegungen auf die Standardeinstellungen zurück. Alternativ kann man auch die Home-Taste oder Pos1-Taste drücken. Jetzt bist du mit der Navigation vertraut. Gehe doch auf den Spielbildschirm zurück.");
        }

        public static void SecondRebindBackOutput()
        {
            Output("Zurück");
        }

        public static void RebindBackOutputSubmit()
        {
            // TODO: This currently does not work as intended, it seems like some parts of the tutorial stay loaded, look also: MutliModalInputManager.cs TODO
            var text = "Super! Du hast die Einführung abgeschlossen! Du kommst jetzt zum Spielbildschirm zurück und kannst ab jetzt frei navigieren. Viel Spaß beim Lernen.";
            Output(text);
            MultimodalInputManager.Instance?.DisableInput(MultimodalInputManager.InputType.Tutorial);
            AssistiveOutput.WaitForSpeakingToFinish(() => SceneManager.LoadScene("Scenes/LetterSortingScene"), text);
        }

        public static void InvalidKeyPressOutput()
        {
            Output("Knopf in diesem Einführungsabschnitt deaktiviert.");
        }

        private static void Output(string text)
        {
            IOEventManager.InvokeAssistiveOutput(text, Settings.GlobalSettings.standardOutputType);
        }
    }

    public class Tutorial : MonoBehaviour
    {
        public enum Layer
        {
            Letter = 1,
            QuestionLayer = 2,
            GameMenu = 0,
            PackageMenu = 0,
            PackageList = 1,
            SettingsMenu = 0,
        }

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

        public State CurrentState;

        public Dictionary<State, TutorialKeymap> Keymaps = new()
        {
            { State.Start, new TutorialKeymap(false, false, true, true) },
            { State.PickAnswer, new TutorialKeymap(true, true, false, true) },
            { State.AfterAnswerPicked, new TutorialKeymap(false, false, true, true) },
            { State.FirstSwitchPackButton, new TutorialKeymap(false, false, false, true) },
            { State.PackagePickerMenuNavigation, new TutorialKeymap(true, true, false, false) },
            { State.OnSelectedUnit, new TutorialKeymap(false, false, true, true) },
            { State.AfterUnitPicked, new TutorialKeymap(true, true, true, true) },
            { State.AfterPackageScreen, new TutorialKeymap(false, false, true, false) },
            { State.GameMenuNavigation, new TutorialKeymap(true, true, false, true) },
            { State.SettingsMenuIntroduction, new TutorialKeymap(true, true, false, true) },
            { State.SettingsMenuNavigation, new TutorialKeymap(true, true, false, true) },
        };

        public static TutorialKeymap CurrentStateKeymap =>
            Instance == null ? null : Instance.Keymaps[Instance.CurrentState];

        public static Tutorial Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            
            UIManager.Instance.SwitchLayer((int)Layer.Letter);
        }
        

        #region GameScreen

        public void Letter()
        {
            switch (CurrentState)
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
            switch (CurrentState)
            {
                case State.Start:
                    TutorialOutputs.FirstAnswerOutput();
                    CurrentState = State.PickAnswer;
                    break;
                case State.PickAnswer:
                    TutorialOutputs.FirstAnswerOutput();
                    break;
                case State.AfterAnswerPicked:
                    TutorialOutputs.SecondFirstAnswerOutput();
                    break;
                case State.AfterPackageScreen:
                    TutorialOutputs.SecondFirstAnswerOutput();
                    break;
            }
        }

        public void SecondAnswerFocus()
        {
            switch (CurrentState)
            {
                case State.PickAnswer:
                    TutorialOutputs.SecondAnswerFocus();
                    break;
            }
        }

        public void SecondAnswerSubmit()
        {
            switch (CurrentState)
            {
                case State.PickAnswer:
                    TutorialOutputs.SecondAnswerSubmit();
                    CurrentState = State.AfterAnswerPicked;
                    break;
            }
        }

        public void ThirdAnswer()
        {
            switch (CurrentState)
            {
                case State.PickAnswer:
                    TutorialOutputs.ThirdAnswerOutput();
                    break;
            }
        }

        public void SwitchPackFocus()
        {
            switch (CurrentState)
            {
                case State.AfterAnswerPicked:
                    CurrentState = State.FirstSwitchPackButton;
                    TutorialOutputs.FirstSwitchPackButtonOutput();
                    break;
                case State.AfterPackageScreen:
                    CurrentState = State.GameMenuNavigation;
                    TutorialOutputs.SecondSwitchPackButtonOutput();
                    break;
                case State.GameMenuNavigation:
                    TutorialOutputs.ThirdSwitchPackButtonOutput();
                    break;
            }
        }

        public void SwitchPackSubmit()
        {
            switch (CurrentState)
            {
                case State.FirstSwitchPackButton:
                    CurrentState = State.PackagePickerMenuNavigation;
                    SceneControl.TransitionToPackagePickerScreenInTutorial();
                    break;
                case State.GameMenuNavigation:
                    TutorialOutputs.ThirdSwitchPackButtonOutput();
                    break;
            }
        }

        public void QuestionDirection()
        {
            switch (CurrentState)
            {
                case State.GameMenuNavigation:
                    TutorialOutputs.QuestionDirectionOutput();
                    break;
            }
        }

        public void QuestionMode()
        {
            switch (CurrentState)
            {
                case State.GameMenuNavigation:
                    TutorialOutputs.QuestionModeOutput();
                    break;
            }
        }

        public void SettingsFocus()
        {
            switch (CurrentState)
            {
                case State.GameMenuNavigation:
                    TutorialOutputs.SettingsOutput();
                    break;
            }
        }

        public void SettingsSubmit()
        {
            switch (CurrentState)
            {
                case State.GameMenuNavigation:
                    CurrentState = State.SettingsMenuIntroduction;
                    SceneControl.TransitionToSettingsScreen();
                    break;
            }
        }

        #endregion

        #region PackageScreen

        public void PackageBackButtonFocus()
        {
            switch (CurrentState)
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
            switch (CurrentState)
            {
                case State.PackagePickerMenuNavigation:
                    TutorialOutputs.FirstPackageBackButtonOutput();
                    break;
                case State.AfterUnitPicked:
                    CurrentState = State.AfterPackageScreen;
                    SceneControl.TransitionToGameScreenInTutorial();
                    UIManager.Instance.SwitchLayer((int)Layer.Letter);

                    break;
            }
        }

        public void SaveButton()
        {
            switch (CurrentState)
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
            switch (CurrentState)
            {
                case State.PackagePickerMenuNavigation:
                    CurrentState = State.OnSelectedUnit;
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
            switch (CurrentState)
            {
                case State.OnSelectedUnit:
                    TutorialOutputs.FirstUnitpickerOutput();
                    CurrentState = State.AfterUnitPicked;
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
            switch (CurrentState)
            {
                case State.SettingsMenuIntroduction:
                    TutorialOutputs.FirstRebindBackOutput();
                    CurrentState = State.SettingsMenuNavigation;
                    break;
                case State.SettingsMenuNavigation:
                    TutorialOutputs.SecondRebindBackOutput();
                    break;
            }
        }

        public void RebindBackSubmit()
        {
            switch (CurrentState)
            {
                case State.SettingsMenuIntroduction:
                    TutorialOutputs.FirstRebindBackOutput();
                    CurrentState = State.SettingsMenuNavigation;
                    break;
                case State.SettingsMenuNavigation:
                    TutorialOutputs.RebindBackOutputSubmit();
                    break;
            }
        }

        public void Navigation()
        {
            if (CurrentState == State.SettingsMenuNavigation)
            {
                TutorialOutputs.NavigationOutput();
            }
        }

        public void BrailleSettings()
        {
            if (CurrentState == State.SettingsMenuNavigation)
            {
                TutorialOutputs.BrailleSettingsOutput();
            }
        }

        public void PerkinsBrailler()
        {
            if (CurrentState == State.SettingsMenuNavigation)
            {
                TutorialOutputs.PerkinsBraillerOutput();
            }
        }

        public void ResetSettings()
        {
            if (CurrentState == State.SettingsMenuNavigation)
            {
                TutorialOutputs.ResetOutput();
            }
        }

        #endregion
    }
}
