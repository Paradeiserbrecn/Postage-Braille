using System;
using System.Collections;
using Data;
using Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LetterPackagePicker : MonoBehaviour
    {
        private const string PackageLayerName = "PackageLayer";
        private int _packageLayerIndex;
        private UILayer PackageLayer => SceneControl.Instance.packagePickerUI.layers[_packageLayerIndex];

        [SerializeField] private GameObject packageListObject;
        [SerializeField] private GameObject packagePrefab;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private SelectedUnitDisplay selectedUnitDisplayGameObject;
        private const float TopOffset = 160f;

        public static LetterPackagePicker Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private IEnumerator Start()
        {
            _packageLayerIndex = SceneControl.Instance.packagePickerUI.AddLayer(new UILayer(PackageLayerName));
            PopulateWithCurrentLanguagePackage();
            yield return new WaitUntil(() => LetterPackages.Instance.CurrentPackageUnit != null);
            
            selectedUnitDisplayGameObject.ChangeLetterUnit(LetterPackages.Instance.CurrentPackageUnit);
        }

        private void OnDestroy()
        {
            SceneControl.Instance.packagePickerUI.RemoveLayer(_packageLayerIndex);
        }

        /// <summary>
        /// Populates the package list with focusable UI elements for every
        /// <see cref="LetterUnit"/> in the current language package.
        /// </summary>
        public void PopulateWithCurrentLanguagePackage()
        {
            PackageLayer.Clear();
            UIManager.Instance.ClearChildren(packageListObject.transform);
            foreach (var letterUnit in LetterPackages.Instance.currentLanguagePackage)
            {
                PackageLayer.Add(GenerateFocusableLetterUnit(letterUnit));
            }
        }

        /// <summary>
        /// Creates and initializes a <see cref="FocusableLetterUnit"/> instance to represent
        /// the specified <see cref="LetterUnit"/> in the package list.
        /// The returned UI element is populated with the unit's metadata, themed using the
        /// current application settings, and linked back to the source <see cref="LetterUnit"/>.
        /// </summary>
        /// <param name="letterUnit">
        /// The <see cref="LetterUnit"/> whose data should be displayed.
        /// </param>
        /// <returns>
        /// A fully initialized <see cref="FocusableLetterUnit"/> ready to be added to the UI.
        /// </returns>
        private FocusableLetterUnit GenerateFocusableLetterUnit(LetterUnit letterUnit)
        {
            var focusableUnit = Instantiate(packagePrefab, packageListObject.transform)
                .GetComponentInChildren<FocusableLetterUnit>();
            
            focusableUnit.text =
                $"Unit:     {letterUnit.UnitIndex}\n" +
                $"Letters:  {string.Join(", ", letterUnit.Letters)}\n" +
                $"Attempts: {letterUnit.attempts}\n" +
                $"Success:  {letterUnit.SuccessPercentage}%";

            focusableUnit.indexTMP.text = letterUnit.UnitIndex.ToString();
            focusableUnit.lettersTMP.text = string.Join(", ", letterUnit.Letters);
            focusableUnit.attemptsTMP.text = letterUnit.attempts.ToString();
            focusableUnit.percentageTMP.text = letterUnit.SuccessPercentage + "%";

            focusableUnit.indexTMP.color = GlobalSettings.PackageTextColor;
            focusableUnit.lettersTMP.color = GlobalSettings.PackageTextColor;
            focusableUnit.attemptsTMP.color = GlobalSettings.PackageTextColor;
            focusableUnit.percentageTMP.color = GlobalSettings.PackageTextColor;


            focusableUnit.letterUnit = letterUnit;
            return focusableUnit;
        }


        /// <summary>
        /// Scrolls the <see cref="ScrollRect"/> so that the specified target element is aligned
        /// with the top of the viewport, applying the configured <c>TopOffset</c> if possible.
        /// The scroll position is clamped to the content bounds to prevent overscrolling.
        /// </summary>
        /// <param name="target">
        /// The <see cref="RectTransform"/> within the scroll content to bring into view.
        /// </param>
        public void ScrollTo(RectTransform target)
        {
            Canvas.ForceUpdateCanvases();

            RectTransform content = scrollRect.content;
            RectTransform viewport = scrollRect.viewport;

            float viewportHeight = viewport.rect.height;
            float contentHeight = content.rect.height;

            // Position of the target from the top of the content
            float targetTop = -target.anchoredPosition.y;

            // Desired content position so target is at the top
            float desiredY = targetTop - TopOffset;

            // Clamp so we don't scroll past the bottom
            desiredY = Mathf.Clamp(desiredY, 0, contentHeight - viewportHeight);

            Vector2 pos = content.anchoredPosition;
            pos.y = desiredY;
            content.anchoredPosition = pos;
        }

        public void ScrollToTop()
        {
            scrollRect.content.anchoredPosition = Vector2.zero;
        }

        public void SelectLetterUnit(LetterUnit letterUnit)
        {
            selectedUnitDisplayGameObject.ChangeLetterUnit(letterUnit);
        }
    }
}
