using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI;
using UnityEngine;
using Utility;

namespace Braille
{
    public class GridBrailleConverter : MonoBehaviour
    {
        public static GridBrailleConverter Instance;

        [SerializeField] private GameObject brailleCharacterPrefab, textObjectPrefab;

        //temporary Dictionary for Braille conversion
        private readonly Dictionary<string, List<bool>> _german = new()
        {
            { "a", new List<bool> { true, false, false, false, false, false } },
            { "b", new List<bool> { true, false, true, false, false, false } },
            { "c", new List<bool> { true, true, false, false, false, false } },
            { "d", new List<bool> { true, true, false, true, false, false } },
            { "e", new List<bool> { true, false, false, true, false, false } },
            { "f", new List<bool> { true, true, true, false, false, false } },
            { "g", new List<bool> { true, true, true, true, false, false } },
            { "h", new List<bool> { true, false, true, true, false, false } },
            { "i", new List<bool> { false, true, true, false, false, false } },
            { "j", new List<bool> { false, true, true, true, false, false } },
            { "k", new List<bool> { true, false, false, false, true, false } },
            { "l", new List<bool> { true, false, true, false, true, false } },
            { "m", new List<bool> { true, true, false, false, true, false } },
            { "n", new List<bool> { true, true, false, true, true, false } },
            { "o", new List<bool> { true, false, false, true, true, false } },
            { "p", new List<bool> { true, true, true, false, true, false } },
            { "q", new List<bool> { true, true, true, true, true, false } },
            { "r", new List<bool> { true, false, true, true, true, false } },
            { "s", new List<bool> { false, true, true, false, true, false } },
            { "t", new List<bool> { false, true, true, true, true, false } },
            { "u", new List<bool> { true, false, false, false, true, true } },
            { "v", new List<bool> { true, false, true, false, true, true } },
            { "w", new List<bool> { false, true, true, true, false, true } },
            { "x", new List<bool> { true, true, false, false, true, true } },
            { "y", new List<bool> { true, true, false, true, true, true } },
            { "z", new List<bool> { true, false, false, true, true, true } },

            // Umlaute
            { "ä", new List<bool> { false, true, false, true, true, false } },
            { "ö", new List<bool> { false, true, true, false, false, true } },
            { "ü", new List<bool> { true, false, true, true, false, true } },
            { "ß", new List<bool> { false, true, true, false, true, true } },

            // Kurzschrift characters
            { "au", new List<bool> { true, false, false, false, false, true } },
            { "äu", new List<bool> { false, true, false, false, true, false } },
            { "eu", new List<bool> { true, false, true, false, false, true } },
            { "ei", new List<bool> { true, true, false, false, false, true } },
            { "ie", new List<bool> { false, true, false, false, true, true } },
            { "ch", new List<bool> { true, true, false, true, false, true } },
            { "sch", new List<bool> { true, false, false, true, false, true } },
            { "st", new List<bool> { false, true, true, true, true, true } },

            // punctuation
            { ",", new List<bool> { false, false, true, false, false, false } },
            { ".", new List<bool> { false, false, false, false, true, false } },
            { ";", new List<bool> { false, false, true, false, true, false } },
            { ":", new List<bool> { false, false, true, true, false, false } },
            { "?", new List<bool> { false, false, true, false, false, true } },
            { "!", new List<bool> { false, false, true, true, true, false } },
            { "(", new List<bool> { false, false, true, true, true, true } },
            { ")", new List<bool> { false, false, true, true, true, true } },
            { "„", new List<bool> { false, false, true, false, true, true } },
            { "“", new List<bool> { false, false, false, true, true, true } },
            { "-", new List<bool> { false, false, false, false, true, true } },
            { "'", new List<bool> { false, false, false, false, false, true } },

            // number indicator 
            { "#", new List<bool> { false, true, false, true, true, true } },

            //space
            { " ", new List<bool> { false, false, false, false, false, false } },
        };


        private void Awake()
        {
            Instance = this;
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
