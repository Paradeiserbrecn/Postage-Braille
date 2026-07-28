using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using UI;
using UnityEngine;
using Utility;
using Unity.VisualScripting;

namespace Braille
{
    public class GridBrailleConverter : MonoBehaviour
    {
        public static GridBrailleConverter Instance;

        [SerializeField] private GameObject brailleCharacterPrefab, textObjectPrefab;
        
        public readonly Dictionary<SupportedLanguage, List<BrailleLanguage>> Packages = new();

        public enum ConditionType
        {
            BeforeWord,
            AfterWord
        }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            /*
            foreach (var language in Enum.GetValues(typeof(SupportedLanguage)).Cast<SupportedLanguage>())
            {
                LoadBrailleConversionData(language);
            }
            */
            LoadBrailleConversionData((SupportedLanguage.De));
        }
        
        private void LoadBrailleConversionData(SupportedLanguage language)
        {
            TextAsset json = Resources.Load<TextAsset>("BrailleLanguages/" + language.HumanName());

             BrailleLanguage brailleLanguage =
                JsonUtility.FromJson<BrailleLanguage>(json.text);
             
             Debug.Log(brailleLanguage.brailleConversions.Count);
        }
                
        public GameObject ConvertTextToBraille(string s,
            AssistiveOutput.OutputType outputType = AssistiveOutput.OutputType.Both, UITextObject.DisplayMode displayMode = UITextObject.DisplayMode.Braille, Transform parent = null)
        {
            var textObject = Instantiate(textObjectPrefab, parent ?? transform);
            var brailleTextObject = textObject.GetComponent<UITextObject>();
            brailleTextObject.text = s;
            brailleTextObject.outputType = outputType;
            brailleTextObject.UpdateBlackletterText();
            

            GenerateBrailleObjects(PreprocessText(s), textObject.gameObject);


            brailleTextObject.SetDisplayMode(displayMode);
            return textObject.gameObject;
        }

        /// <summary>
        ///    The gameObject generated with this method does not support accessibility features, only ConvertTextToBraille does that
        /// </summary>
        /// <param name="s">character to convert (type string to support character combinations)</param>
        /// <returns>BrailleObject</returns>
        public GameObject ConvertCharacterToBrailleObject(string s)
        {
            List<bool> pattern = ConvertCharacterToBrailleList(s);
            if(pattern == null) return null;
            
            var brailleObject = Instantiate(brailleCharacterPrefab).GetComponent<BrailleObject>();
            brailleObject.gameObject.name = s;
            brailleObject.SetBrailleCharacter(pattern);
            
            return brailleObject.gameObject;
        }

        public List<bool> ConvertCharacterToBrailleList(string s)
        {
            if (!_german.TryGetValue(s, out var pattern))
            {
                Debug.LogWarning($"{s} is not supported");
                return null;
            }

            return pattern;
        }

        public string ConvertBrailleToCharacter(List<bool> brailleList)
        {
            foreach (string letter in _german.Keys)
            {
                if (brailleList.SequenceEqual(_german[letter]))
                {
                    return letter;
                }
            }

            return "";
        }

        private void GenerateBrailleObjects(string s, GameObject textObject)
        {
            CharFactory text = new CharFactory(s);
            StringBuilder character = new StringBuilder();

            while (text.Curr != '\0')
            {
                character.Clear();
                switch (text.Curr)
                {
                    case 'a':
                        character.Append(text.Curr);
                        if (text.La == 'u')
                        {
                            text.Next();
                            character.Append(text.Curr);
                        }

                        break;
                    case 'ä':
                        character.Append(text.Curr);
                        if (text.La == 'u')
                        {
                            text.Next();
                            character.Append(text.Curr);
                        }

                        break;
                    case 'e':
                        character.Append(text.Curr);
                        if (text.La == 'u' || text.La == 'i')
                        {
                            text.Next();
                            character.Append(text.Curr);
                        }

                        break;
                    case 'i':
                        character.Append(text.Curr);
                        if (text.La == 'e')
                        {
                            text.Next();
                            character.Append(text.Curr);
                        }

                        break;
                    case 'c':
                        character.Append(text.Curr);
                        if (text.La == 'h')
                        {
                            text.Next();
                            character.Append(text.Curr);
                        }

                        break;
                    case 's':
                        character.Append(text.Curr);
                        if (text.La == 'c')
                        {
                            text.Next();
                            if (text.La == 'h')
                            {
                                character.Append(text.Curr);
                                text.Next();
                                character.Append(text.Curr);
                            }
                        }

                        if (text.La == 't')
                        {
                            text.Next();
                            character.Append(text.Curr);
                        }

                        break;
                    default:
                        character.Append(text.Curr);
                        break;
                }

                var brailleObject = ConvertCharacterToBrailleObject(character.ToString());
                text.Next();
                if (brailleObject != null)
                {
                    brailleObject.transform.SetParent(textObject.transform, false);
                }
            }
        }

        /// <summary>
        /// Converts input string s into a <c>textObject</c> which handles output through Screenreader/Braille isplay
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="outputType">enum OutputType, can be either BRAILLE, SPEAK or BOTH while BOTH is selected as default</param>
        /// <returns></returns>
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
        public Char ConvertNumberToChar(char c)
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
