using System.Collections.Generic;
using Braille;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        private const int QuestionLayerIndex = 0;
        private const string QuestionLayerName = "QuestionLayer";

        [Header("Scene References")] [SerializeField]
        private GameObject questionPosition;

        [SerializeField] private GameObject optionsGrid;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private List<UILayer> layers;

        [Header("Prefabs")] [SerializeField] private GameObject optionParentPrefab;
        public GameObject questionTextPrefab;
        [Header("Parameters")] public int optionsCount;


        private int _currentOptionIndex;

        /// <summary>
        /// The focused option in the currently selected layer
        /// </summary>
        public Focusable CurrentlyFocusedOption => CurrentLayer?.Current;

        private int _currentLayerIdx;

        /// <summary>
        /// The currently selected layer. Switching between different layers is possibe via .SwitchLayer()
        /// </summary>
        private UILayer CurrentLayer => layers.Count == 0 ? null : layers[_currentLayerIdx];

        /// <summary>
        /// The Focusables inside the currently selected layer.
        /// </summary>
        public List<Focusable> CurrentOptions => layers.Count == 0
            ? null
            : layers[_currentLayerIdx].Focusables;

        private void Awake()
        {
            Instance = this;

            layers.Insert(QuestionLayerIndex, new UILayer(QuestionLayerName));
            _currentLayerIdx = 0;
        }

        /// <summary>
        /// Generates GridTextObjects/FocusableTextObjects based on QuestionManager.Instance.currentOptions,
        /// sets the current layer to the question-layer, and then focuses the first option in that layer
        /// </summary>
        public void DisplayQuestion()
        {
            CurrentLayer?.Unfocus();
            _currentLayerIdx = QuestionLayerIndex;

            if (GameManager.Instance.currentQuestionType == GameManager.QuestionType.CharBrailleToLatin)
            {
                DisplayBrailleToLatinQuestion();
            }
            else if (GameManager.Instance.currentQuestionType == GameManager.QuestionType.CharLatinToBraille)
            {
                DisplayLatinToBrailleQuestion();
            }
            else
            {
                Debug.LogWarning("Tried to display unsupported question type.");
            }

            CurrentLayer?.FocusFirst();
        }

        /// <summary>
        /// Removes all currently displayed question content, option content,
        /// and registered focusable elements from the active layer.
        /// </summary>
        private void ClearQuestionCanvas()
        {
            ClearChildren(questionPosition.transform);
            ClearChildren(optionsGrid.transform);
            CurrentLayer?.Clear();
        }

        /// <summary>
        /// Displays a Braille-to-Latin question by rendering the correct answer
        /// in Braille and creating text-based answer options.
        /// </summary>
        private void DisplayBrailleToLatinQuestion()
        {
            ClearQuestionCanvas();
            questionText.text = "";

            // Generate the question braille and set it to the correct position
            GridBrailleConverter.Instance
                .ConvertTextToBraille(QuestionManager.Instance.correctAnswer, parent: questionPosition.transform);

            foreach (var option in QuestionManager.Instance.currentOptions)
            {
                CurrentLayer.Add(GenerateFocusableTextObjectOption(option));
            }

            feedbackText.text = "";
        }

        /// <summary>
        /// Creates a focusable text option and adds it to the options grid.
        /// </summary>
        /// <param name="optionText">The text displayed for the option.</param>
        /// <returns>The created focusable text object.</returns>
        private FocusableTextObject GenerateFocusableTextObjectOption(string optionText)
        {
            var parent = Instantiate(optionParentPrefab, optionsGrid.transform, false);

            var focusableText = Instantiate(questionTextPrefab, parent.transform)
                .GetComponent<FocusableTextObject>();

            focusableText.Text = optionText;

            return focusableText;
        }

        /// <summary>
        /// Displays a Latin-to-Braille question by showing the answer as text
        /// and generating Braille answer options.
        /// </summary>
        private void DisplayLatinToBrailleQuestion()
        {
            ClearQuestionCanvas();

            questionText.text = QuestionManager.Instance.correctAnswer;

            foreach (var optionText in QuestionManager.Instance.currentOptions)
            {
                CurrentLayer.Add(GenerateBrailleOption(optionText));
            }

            feedbackText.text = "";
        }

        /// <summary>
        /// Creates a Braille option and adds it to the options grid.
        /// </summary>
        /// <param name="optionText">The text to convert into Braille.</param>
        /// <returns>The generated Braille grid text object.</returns>
        private GridTextObject GenerateBrailleOption(string optionText)
        {
            var parent = Instantiate(optionParentPrefab, optionsGrid.transform, false);

            var optionBraille = GridBrailleConverter.Instance
                .ConvertTextToBraille(optionText, parent: parent.transform);

            return optionBraille.GetComponent<GridTextObject>();
        }

        /// <summary>
        /// Highlights the next available option in the current layer.
        /// </summary>
        /// <returns>The newly focused option, or null if no option could be focused.</returns>
        public Focusable HighlightNextOption() => HighlightOption();

        /// <summary>
        /// Highlights the previous available option in the current layer.
        /// </summary>
        /// <returns>The newly focused option, or null if no option could be focused.</returns>
        public Focusable HighlightPreviousOption() => HighlightOption(false);

        /// <summary>
        /// Moves focus to the next or previous option depending on the direction specified.
        /// </summary>
        /// <param name="next">
        /// True to move to the next option; false to move to the previous option.
        /// </param>
        /// <returns>The newly focused option, or null if highlighting is unavailable.</returns>
        private Focusable HighlightOption(bool next = true)
        {
            if (GameManager.Instance.currentState != GameManager.GameState.WaitingForInput)
            {
                Debug.LogWarning("Tried to highlight a braille text object while not waiting for input.");
                return null;
            }

            // This will be expanded once we have different menu options.
            switch (GameManager.Instance.currentQuestionType)
            {
                case GameManager.QuestionType.CharBrailleToLatin:
                case GameManager.QuestionType.CharLatinToBraille:
                    return next ? HighlightNextFocusable() : HighlightPreviousFocusable();
            }

            Debug.LogWarning("Highlighting for this Option type is not yet supported");
            return null;
        }

        /// <summary>
        /// Moves focus to the next focusable element in the current layer.
        /// </summary>
        /// <returns>The newly focused element.</returns>
        private Focusable HighlightNextFocusable()
        {
            return CurrentLayer.FocusNext();
        }

        /// <summary>
        /// Moves focus to the previous focusable element in the current layer.
        /// </summary>
        /// <returns>The newly focused element.</returns>
        private Focusable HighlightPreviousFocusable()
        {
            return CurrentLayer.FocusPrevious();
        }

        /// <summary>
        /// Displays feedback indicating whether the user's answer was correct.
        /// </summary>
        /// <param name="correct">
        /// True to display positive feedback; otherwise displays negative feedback.
        /// </param>
        public void ShowFeedback(bool correct)
        {
            feedbackText.text = correct ? "Correct!" : "Wrong!";
        }

        /// <summary>
        /// Destroys all child GameObjects of the specified transform and detaches them.
        /// </summary>
        /// <param name="parent">The transform whose children should be removed.</param>
        private void ClearChildren(Transform parent)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }

            parent.DetachChildren();
        }

        /// <summary>
        /// Switches focus to the next UI layer and focuses its first element.
        /// </summary>
        public void SwitchLayer()
        {
            CurrentLayer.Unfocus();
            _currentLayerIdx = (_currentLayerIdx + 1) % layers.Count;
            CurrentLayer.FocusFirst();
        }

        /// <summary>
        /// Creates a new UI layer and optionally transfers focus to it.
        /// </summary>
        /// <param name="layerName">The name assigned to the new layer.</param>
        /// <param name="focusables">
        /// The collection of focusable elements contained in the layer.
        /// </param>
        /// <param name="focusImmediately">
        /// If true, the current layer is unfocused and the first element in the
        /// newly added layer is focused.
        /// If the focusables is empty, focus is not changed
        /// </param>
        /// <returns>
        /// The index of the newly added layer, or -1 if the layer could not be created.
        /// </returns>
        public int AddLayer(string layerName, List<Focusable> focusables, bool focusImmediately = true)
        {
            if (string.IsNullOrWhiteSpace(layerName))
            {
                Debug.LogWarning("Layer name cannot be empty.");
                return -1;
            }

            if (focusables == null)
            {
                Debug.LogWarning("Tried to add a null focusables List to UIManager. Returning -1");
                return -1;
            }

            layers.Add(new UILayer(layerName, focusables));
            var newLayerIdx = layers.Count - 1;

            if (focusImmediately && focusables.Count > 0)
            {
                CurrentLayer?.Unfocus();
                _currentLayerIdx = newLayerIdx;
                CurrentLayer?.FocusFirst();
            }

            return newLayerIdx;
        }

        
        
        /// <summary>
        /// Creates a new UI layer and optionally transfers focus to it.
        /// </summary>
        /// <param name="layer">The layer to add to the UIManager</param>
        /// <param name="focusImmediately">
        /// If true, the current layer is unfocused and the first element in the
        /// newly added layer is focused.
        /// If the focusables is empty, focus is not changed
        /// </param>
        /// <returns></returns>
        public int AddLayer(UILayer layer, bool focusImmediately = true)
        {
            if (layer == null)
            {
                Debug.LogWarning("Tried to add a null layer to UIManager. Returning -1");
                return -1;
            }

            layers.Add(layer);

            var newLayerIdx = layers.Count - 1;


            if (focusImmediately && layer.Focusables.Count > 0)
            {
                CurrentLayer?.Unfocus();
                _currentLayerIdx = newLayerIdx;
                CurrentLayer?.FocusFirst();
            }

            return newLayerIdx;
        }

        /// <summary>
        /// Removes the specified UI layer.
        /// </summary>
        /// <param name="layerIdx">
        /// The index of the layer to remove.
        /// </param>
        /// <remarks>
        /// The question layer cannot be removed. If the removed layer is currently
        /// focused, focus is returned to the question layer.
        /// </remarks>
        public void RemoveLayer(int layerIdx)
        {
            if (layerIdx < 0 || layerIdx >= layers.Count)
            {
                Debug.LogWarning("Index out of range when trying to remove a layer: " + layerIdx);
                return;
            }

            if (layerIdx == QuestionLayerIndex)
            {
                Debug.LogWarning("Trying to illegally remove questions-layer.");
                return;
            }

            if (layerIdx < _currentLayerIdx)
            {
                _currentLayerIdx--;
            }

            if (layerIdx == _currentLayerIdx)
            {
                CurrentLayer.Unfocus();
                _currentLayerIdx = QuestionLayerIndex;
                CurrentLayer?.FocusFirst();
            }

            layers.RemoveAt(layerIdx);
        }
    }
}