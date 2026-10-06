using IO;

namespace Tutorial
{
    /// <summary>
    /// Holds the Outputs for the Tutorial.
    /// Only handles the Outputs and not the necessary transitions needed for the tutorial to continue to the next steps.
    /// Tutorial flow is handled in <see cref="TutorialFlow"/>
    /// <remarks>
    /// It is primarily used as a shorthand for assistive output invocations and is prepared for introducing data driven
    /// outputTexts, which is why we have so many string declarations that are not used anywhere else.
    /// In the future you would want to have something along the lines of `string outputText = getTextForCurrentLanguage(TutorialStep)`
    /// Every method also has a return value as you can control the tutorial flow with the use of the string being output via `AssistiveOutput.WaitForSpeakingToFinish(..., text);`
    /// </remarks>
    /// </summary>
    public static class TutorialOutputs
    {
        public static string FirstLetterOutput()
        {
            string outputText =
                "Willkommen zum Braille Lernprogramm Postage Braille! Die Einführung kann man mit der Escape-Taste überspringen. Das ist der Spiel Bildschirm. Am Anfang ist immer der Brief ausgewählt. Dieser beinhaltet die aktuelle Frage. Drücke die Tabulator taste um zu den Antwortmöglichkeiten zu gelangen.";
            Output(outputText);
            return outputText;
        }

        public static string FirstAnswerOutput()
        {
            string outputText =
                "Das ist die erste Antwortmöglichkeit. Mit Pfeiltaste rechts und Pfeiltaste links kann man die Antworten navigieren. Wähle doch mal die zweite Antwort aus.";
            Output(outputText);
            return outputText;
        }

        public static string SecondAnswerFocus()
        {
            string outputText = "Das ist die Zweite Antwort. Mit der Enter-Taste kannst du die Antwort bestätigen. ";
            Output(outputText);
            return outputText;
        }

        public static string SecondAnswerSubmit()
        {
            string outputText = "Du hast die Antwort bestätigt und bekommst danach eine Rückmeldung!";
            Output(outputText);
            return outputText;
        }

        public static string ThirdAnswerOutput()
        {
            string outputText =
                "Das ist die Dritte Antwort. Es gibt immer drei Antwortmöglichkeiten. Gehe doch zur Zweiten zurück. Nutze dafür die linke Pfeiltaste";
            Output(outputText);
            return outputText;
        }

        public static string SecondLetterOutput()
        {
            string outputText =
                "Nach beantworten der Frage kommt man zum Brief zurück. Verwende die Tabulator Taste um zum Menü zu gelangen.";
            Output(outputText);
            return outputText;
        }

        public static string SecondFirstAnswerOutput()
        {
            string outputText = "Das ist die erste Antwortmöglichkeit. Drücke die Tabulator-Taste erneut.";
            Output(outputText);
            return outputText;
        }

        public static string FirstSwitchPackButtonOutput()
        {
            string outputText =
                "Das ist die Spielmenü-Leiste. Zuerst ist der \"Einheit Auswählen\" Knopf ausgewählt. Drücke die Enter-Taste um eine andere Einheit auszuwählen.";
            Output(outputText);
            return outputText;
        }

        public static string FirstPackageBackButtonOutput()
        {
            string outputText =
                "Auf diesem Bildschirm kannst du die Einheiten auswählen. Am anfang ist der Zurück Knopf ausgewählt. Mit den Pfeiltasten kannst du im Menü navigieren.";
            Output(outputText);
            return outputText;
        }

        public static string FirstSaveButtonOutput()
        {
            string outputText =
                "Dieser Knopf speichert deinen Fortschritt. Vergiss nicht deinen Fortschritt zu Speichern.";
            Output(outputText);
            return outputText;
        }

        public static string FirstSelectedUnitOutput()
        {
            string outputText =
                "Hier kannst du die ausgewählte einheit Überprüfen. Das sind alle Buttons in der Menüleiste. Mit der Tabulator-Taste wechselst du auf die Einheitenliste.";
            Output(outputText);
            return outputText;
        }

        public static string FirstUnitpickerOutput()
        {
            string outputText =
                "Das ist die Einheitenliste. Mit der Enter-Taste kann man eine Einheit auswählen. Versuche jetzt selbstständig zum Spielbildschirm zurück zu navigierten.";
            Output(outputText);
            return outputText;
        }

        public static string SecondUnitpickerOutput()
        {
            string outputText = "Einheit eins. In der Einführung kannst du nur diese Auswählen.";
            Output(outputText);
            return outputText;
        }

        public static string SecondPackageBackButtonOutput()
        {
            string outputText = "Zurück";
            Output(outputText);
            return outputText;
        }

        public static string SecondSaveButtonOutput()
        {
            string outputText = "Fortschritt speichern.";
            Output(outputText);
            return outputText;
        }

        public static string SecondSelectedUnitOutput()
        {
            string outputText = "Das ist die ausgewählte Einheit. In der Einführung gibt es nur Einheit eins.";
            Output(outputText);
            return outputText;
        }

        public static string ThirdLetterOutput()
        {
            string outputText =
                "Du bist nun wieder am Spielbildschirm. Du bist nun mit dem Hauptteil des Spiels vertraut, es gibt aber noch eine andere Funktion. Navigiere in die menüleiste.";
            Output(outputText);
            return outputText;
        }

        public static string SecondSwitchPackButtonOutput()
        {
            string outputText =
                "Du bist nun wieder in der Menüleiste des Spielbildschirms. Den ersten Knopf kennst du bereits, mit diesem sind wir zum Bildschirm zum auswählen von Einheiten gekommen. Navigiere zu den anderen Knöpfen um zu lernen was sie können.";
            Output(outputText);
            return outputText;
        }

        public static string ThirdSwitchPackButtonOutput()
        {
            string outputText =
                "Dieser Knopf bringt dich in den Bildschirm zum Auswählen von Einheiten, navigiere zum Einstellungen Bildschirm um die Einleitung fortzufahren.";
            Output(outputText);
            return outputText;
        }

        public static string QuestionDirectionOutput()
        {
            string outputText =
                "Dieser Knopf ändert die Fragerichtung. Anfangs ist die Frage in Braille dargestellt und die Antworten durch Schwarzschrift und Audio. Nach drücken des Knopfes wird zur umgekehrten Fragerichtung gewechselt.";
            Output(outputText);
            return outputText;
        }

        public static string QuestionModeOutput()
        {
            string outputText =
                "Mit diesem Knopf änderst du die Art der Fragen. Anfangs werden einzelne Buchstaben der aktuellen Einheit abgefragt. Alternativ kann man Wörter aus allen gelernten Buchstaben abfragen. Vergiss nicht hin und wieder die Art zu wechseln.";
            Output(outputText);
            return outputText;
        }

        public static string SettingsOutput()
        {
            string outputText =
                "Dieser Knopf führt zum Einstellungsmenü. Dücke die Entertaste um zu den Einstellungen zu gelangen.";
            Output(outputText);
            return outputText;
        }

        public static string FirstRebindBackOutput()
        {
            string outputText =
                "Du befindest dich jetzt im Einstellungsbildschirm. Anfangs ist der Zurück Knopf ausgewählt. Navigiere doch durch die anderen Menüpunkte.";
            Output(outputText);
            return outputText;
        }

        public static string NavigationOutput()
        {
            string outputText =
                "Hiermit öffnest du die Tastenbelegung für die Navigation. Um eine Tastenbelegung zu ändern wähle in der Liste die zu ändernde Aktion aus und drücke die Enter-Taste. Danach drücke die gewünschte Taste. Die Aktion wird von nun an mit der neuen Taste ausgelöst.";
            Output(outputText);
            return outputText;
        }

        public static string BrailleSettingsOutput()
        {
            string outputText =
                "Hiermit öffnest du die Tastenbelegung für die Braille Darstellung. Das ändern der Tastenbelegung erfolgt gleich als bei der Navigation.";
            Output(outputText);
            return outputText;
        }

        public static string PerkinsBraillerOutput()
        {
            string outputText =
                "Hiermit öffnest du die Tastenbelegung für den Perkinsbrailler. Anfangs sind die Perkinsbraillertasten SDF und JKL an der Tastatur.";
            Output(outputText);
            return outputText;
        }

        public static string ResetOutput()
        {
            string outputText =
                "Dieser Knopf setzt alle Tastenbelegungen auf die Standardeinstellungen zurück. Alternativ kann man auch die Home-Taste oder Pos1-Taste drücken. Jetzt bist du mit der Navigation vertraut. Gehe doch auf den Spielbildschirm zurück.";
            Output(outputText);
            return outputText;
        }

        public static string SecondRebindBackOutput()
        {
            string outputText = "Zurück";
            Output(outputText);
            return outputText;
        }

        public static string RebindBackOutputSubmit()
        {
            var outputText =
                "Super! Du hast die Einführung abgeschlossen! Du kommst jetzt zum Spielbildschirm zurück und kannst ab jetzt frei navigieren. Viel Spaß beim Lernen.";
            Output(outputText);
            return outputText;
        }

        public static string InvalidKeyPressOutput()
        {
            string outputText = "Knopf in diesem Einführungsabschnitt deaktiviert.";
            Output(outputText);
            return outputText;
        }

        public static string SkipTutorial()
        {
            var outputText = "Einführung wird Übersprungen.";
            Output(outputText);
            return outputText;
        }

        private static void Output(string text)
        {
            IOEventManager.InvokeAssistiveOutput(text, Settings.GlobalSettings.standardOutputType);
        }
    }
}
