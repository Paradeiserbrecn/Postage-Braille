using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Data;
using NUnit.Framework.Internal;
using UI;
using UnityEngine;
using Utility;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

namespace Braille
{
    public class BrailleConverter : MonoBehaviour
    {
        public static BrailleConverter Instance;

        [SerializeField] private GameObject brailleCharacterPrefab, textObjectPrefab;
        
        private Dictionary<SupportedLanguage, BrailleLanguage> _conversionLanguages = new();


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            foreach (var language in Enum.GetValues(typeof(SupportedLanguage)).Cast<SupportedLanguage>())
            {
                LoadBrailleConversionData(language);
                
            }
        }
        
        private void LoadBrailleConversionData(SupportedLanguage language)
        {
            TextAsset json = Resources.Load<TextAsset>("BrailleLanguages/" + language.HumanName());

             BrailleLanguage brailleLanguage =
                JsonUtility.FromJson<BrailleLanguage>(json.text);
             
             _conversionLanguages.Add(language, brailleLanguage);
             Debug.Log("Loaded braille language: " + language.HumanName());
        }
        
        /// <summary>
        /// Generates a UITextObject prefab and configures it according to the parameters.
        /// </summary>
        /// <param name="s">text that should be displayed</param>
        /// <param name="outputType">What kind of AssistiveOutput should be invoked on selecting</param>
        /// <param name="displayMode">Text can be displayed as print letters or braille</param>
        /// <param name="parent">Parent transform the UITextObject should be a child of</param>
        /// <returns></returns>
        public GameObject ConvertTextToBraille(string s,
            AssistiveOutput.OutputType outputType = AssistiveOutput.OutputType.Both, UITextObject.DisplayMode displayMode = UITextObject.DisplayMode.Braille, Transform parent = null)
        {
            var textObject = Instantiate(textObjectPrefab, parent ?? transform);
            var brailleTextObject = textObject.GetComponent<UITextObject>();
            brailleTextObject.text = s;
            brailleTextObject.outputType = outputType;
            brailleTextObject.UpdateBlackletterText();

            s = PreprocessText(s);
            GenerateBrailleObjects(s, textObject);

            brailleTextObject.SetDisplayMode(displayMode);
            return textObject.gameObject;
        }
        
        private void GenerateBrailleObjects(string s, GameObject textObject)
        {
            StringBuilder text = new StringBuilder(s);
            while (text.Length > 0)
            {
                var conversion = ConvertCharacterToConversion(text.ToString());
                if (conversion != null)
                {
                    var brailleObject = ConvertConversionToBrailleObject(conversion);
                    brailleObject.transform.SetParent(textObject.transform, false);
                    text.Remove(0, conversion.printCharacter.Length);
                }
                else
                {
                    text.Remove(0, 1);
                }
            }
        }

        /// <summary>
        ///  The gameObject generated with this method does not support accessibility features, only ConvertTextToBraille does that
        /// </summary>
        /// <param name="brailleConversion">BrailleConversion picked by ConvertCharacterToConversion</param>
        /// <returns>BrailleObject prefab</returns>
        private GameObject ConvertConversionToBrailleObject(BrailleConversion brailleConversion)
        {
            if(brailleConversion == null) return null;
            
            var brailleObject = Instantiate(brailleCharacterPrefab).GetComponent<BrailleObject>();
            brailleObject.gameObject.name = brailleConversion.printCharacter;
            brailleObject.SetBrailleCharacter(brailleConversion.brailleCharacter);
            
            return brailleObject.gameObject;
        }

        /// <summary>
        /// Converts characters to the longest fitting braille Conversion
        /// </summary>
        /// <param name="s">Characters to convert</param>
        /// <returns>Braille Character that corresponds to the longest leading section of the input string, or null if no match was found, alongside the number of leading characters used</returns>
        public BrailleConversion ConvertCharacterToConversion(string s)
        {
            CharFactory text = new CharFactory(s);
            StringBuilder character = new StringBuilder();
            var currentLanguage = LetterPackages.Instance.currentLanguage;
            
            List<BrailleConversion> possibleConversions = _conversionLanguages[currentLanguage].brailleConversions;
            BrailleConversion bestMatch = null;
            
            while (text.Curr != '\0')
            {
                character.Append(text.Curr);
                
                var newPossibleConversions = possibleConversions.Where(c => c.printCharacter.StartsWith(character.ToString()))
                    .ToList();
                
                if (newPossibleConversions.Count == 0) break;
                
                bestMatch = newPossibleConversions.First();
                possibleConversions = newPossibleConversions;
                text.Next();
            }

            if (bestMatch == null)
            {
                Debug.Log("Character could not be converted to Braille");
            }

            return bestMatch;
        }


        /// <param name="brailleList">boolList describing Braille Character</param>
        /// <returns>Corresponding character or character combination</returns>
        public string ConvertBrailleToCharacter(List<bool> brailleList)
        {
            var currentLanguage = LetterPackages.Instance.currentLanguage;
            List<BrailleConversion> possibleConversions = _conversionLanguages[currentLanguage].brailleConversions;

            var besteConversion = possibleConversions.FirstOrDefault(c => c.brailleCharacter == brailleList);

            if (besteConversion == null)
            {
                Debug.Log("Braille could not be converted to Character");
            }
            
            return besteConversion?.printCharacter;
        }



        /// <summary>
        /// Preprocesses Text such that the text can be converted to braille.
        /// </summary>
        /// <param name="s">String to preprocess</param>
        /// <param name="outputType">enum OutputType, can be either BRAILLE, SPEAK or BOTH while BOTH is selected as default</param>
        /// <returns>Input string but all whitespace characters replaced by space, upper case letters replaced by lower case and '#' inserted before numbers</returns>
        private string PreprocessText(string s)
        {
            CharFactory text = new CharFactory(s);
            StringBuilder processedText = new StringBuilder();

            do
            {
                if (Char.IsWhiteSpace(text.Curr))
                {
                    processedText.Append(' ');
                    text.Next();
                }

                else if (Char.IsDigit(text.Curr))
                {
                    processedText.Append('#');
                    while (Char.IsDigit(text.Curr))
                    {
                        processedText.Append(ConvertNumberToChar(text.Curr));
                        text.Next();
                    }
                }
                else
                {
                    while ((Char.IsLetter(text.Curr) || Char.IsPunctuation(text.Curr)) && !Char.IsDigit(text.Curr))
                    {
                        processedText.Append(Char.ToLower(text.Curr));
                        text.Next();
                    }
                }
            } while (text.HasNext);

            return processedText.ToString();
        }


        //takes a single digit and converts it into its corresponding character for braille representation
        public char ConvertNumberToChar(char c)
        {
            switch (c)
            {
                case '1': return ('a');
                case '2': return ('b');
                case '3': return ('c');
                case '4': return ('d');
                case '5': return ('e');
                case '6': return ('f');
                case '7': return ('g');
                case '8': return ('h');
                case '9': return ('i');
                case '0': return ('j');
                default:
                    Debug.LogWarning(c + " is not a number.");
                    return '\0';
            }
        }
    }
}
