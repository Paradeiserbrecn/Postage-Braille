using System;
using System.Collections.Generic;
using Braille;
using Data;
using IO;
using UI;
using UnityEngine;
using Screen = UI.Screen;

namespace Tutorial
{
    public static class TutorialOutputs
    {
        public static void FirstLetterOutput()
        {
            Output(
                "Das ist der Spiel Bildschirm. Am Anfang ist immer der Brief ausgewählt. Dieser beinhaltet die aktuelle Frage. Drücke die Tabulator taste um zu den Antwortmöglichkeiten zu gelangen.");
        }

        public static void SecondLetterOutput()
        {
            Output(
                "Nach beantworten der Frage kommt man zum Brief zurück. Verwende die Tabulator Taste um zum Menü zu gelangen.");
        }

        public static void FirstAnswerOutput()
        {
            Output(
                "Das ist die erste Antwortmöglichkeit. Mit Pfeiltaste rechts und Pfeiltaste links kann man die Antworten navigieren. Wähle doch mal die zweite Antwort aus.");
        }

        public static void SecondAnswerFocus()
        {
            Output("Das ist die Zweite Antwort. Mit der Enter-Taste kannst du die Antwort bestätigen. ");
            AssistiveOutput.WaitForSpeakingToFinish(() => UIManager.Instance.SwitchLayer(1));
        }

        public static void SecondAnswerSubmit()
        {
            Output("Das ist die Zweite Antwort. Mit der Enter-Taste kannst du die Antwort bestätigen");
        }

        public static void ThirdAnswerFocus()
        {
            Output(
                "Das ist die Dritte Antwort. Es gibt immer drei Antwortmöglichkeiten. Gehe doch zur Zweiten zurück. Nutze dafür die linke Pfeiltaste");
        }

        public static void SecondFirstAnswerOutput()
        {
            Output("Das ist die erste Antwortmöglichkeit. Drücke die Tabulator erneut.");
        }

        public static void FirstLetterPackageButtonOutput()
        {
            Output(
                "Das ist die Spielmenü-Leiste. Zuerst ist der \"Einheit Auswählen\" Knopf ausgewählt. Drücke die Enter-Taste um eine andere Einheit auszuwählen.");
        }

        public static void FirstPackageBackButtonOutput()
        {
            Output(
                "Auf diesem Bildschirm kannst du die Einheiten auswählen. Am anfang ist der Zurück Knopf ausgewählt. Mit den Pfeiltasten kannst du im Menü navigieren.");
        }

        public static void SecondPackageBackButtonOutput()
        {
            Output("Zurück");
        }

        public static void SaveButtonOutput()
        {
            Output("Dieser Knopf speichert deinen Fortschritt. Vergiss nicht deinen Fortschritt zu Speichern.");
        }

        public static void SelectedUnitOutput()
        {
            Output(
                "Hier kannst du die ausgewählte einheit Überprüfen. Das sind alle Buttons in der Menüleiste. Mit der Tabulator-Taste wechselst du auf die Einheitenliste.");
        }

        public static void UnitOutput()
        {
            Output(
                "Das ist die Einheitenliste. Mit der Enter-Taste kann man eine Einheit auswählen. Versuche jetzt zum Spielbildschirm zurück zu navigierten.");
        }

        public static void SecondLetterPackageButtonOutput()
        {
            Output(
                "Jetzt bist du wieder in der Menüleiste des Spielbildschirms. Navigiere mal zu den anderen Menüoptionen.");
        }

        public static void QuestionDirectionOutput()
        {
            Output(
                "Dieser Knopf änderst die Fragerichtung. Anfangs ist die Frage in Braille dargestellt und die Antworten durch Schwarzschrift und Audio.");
        }

        public static void QuestionModeOutput()
        {
            Output(
                "Mit diesem Knopf änderst du die Art der Fragen. Anfangs werden einzelne Buchstaben der aktuellen Einheit abgefragt. Alternativ kann man Wörter aus allen gelernten Buchstaben abfragen. Vergiss nicht hin und wieder die die Art zu wechseln.");
        }

        public static void SettingsOutput()
        {
            Output(
                "Dieser Knopf führt zum Einstellungsmenü. Dücke die Entertaste um zu den Einstellungen zu gelangen.");
        }

        public static void FirstRebindBackOutput()
        {
            Output(
                "Du befindest dich jetzt in EinstellungsBildschirm. Anfangs ist der Zurück Knopf ausgewählt. Navigiere doch durch die anderen Menüpunkte.");
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
                "Dieser Knopf setzte alle Tastenbelegungen auf die Standardeinstellungen zurück. Alternativ kann man auch die Home-Taste oder Pos1-Taste drücken. Jetzt bist du mit der Navigation vertraut. Gehe doch auf des Spielbildschirm zurück.");
        }

        public static void SecondRebindBackOutput()
        {
            Output("Zurück");
        }

        public static void RebindBackOutput()
        {
            Output(
                "Super! Dus hast die Einführung abgeschlossen! Du kommst jetzt zum Spielbildschirm zurück und kannst ab jetzt frei navigieren. Viel Spaß beim Lernen.");
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
        public static TutorialKeymap CurrentStateKeymap => Instance == null ? null : Instance.Keymaps[Instance.CurrentState];
        public static Tutorial Instance { get; private set; }
        public State CurrentState;
        private enum Layer
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
            Letter,
            pickAnswer
        }
        
        public Dictionary<State, TutorialKeymap> Keymaps = new Dictionary<State, TutorialKeymap>()
        {
            { State.Letter , new TutorialKeymap(false,false,true,true)}
        };

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            UIManager.Instance.SwitchLayer((int)Layer.Letter);
        }
        
        public void LetterFocus()
        {
            TutorialOutputs.FirstLetterOutput();
        }

        public void LetterSubmit()
        {
            TutorialOutputs.FirstLetterOutput();
        }
        
    }
}
