using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;

public class BrailleConverter : MonoBehaviour
{
    public  static BrailleConverter Instance;
    
    [SerializeField] private GameObject brailleCharacterPrefab, wordObjectPrefab, textObjectPrefab;
    
    //temporary Dictionary for Braille conversion
    Dictionary<string, List<bool>> german = new()
    {
        { "a", new List<bool> { true, false, false, false, false, false }},  
        { "b", new List<bool> { true, false, true, false, false, false }},    
        { "c", new List<bool> { true, true, false, false, false, false }},
        { "d", new List<bool> { true, true, false, true, false, false }},     
        { "e", new List<bool> { true, false, false, true, false, false }},    
        { "f", new List<bool> { true, true, true, false, false, false }},     
        { "g", new List<bool> { true, true, true, true, false, false }},      
        { "h", new List<bool> { true, false, true, true, false, false }},     
        { "i", new List<bool> { false, true, true, false, false, false }},    
        { "j", new List<bool> { false, true, true, true, false, false }},     
        { "k", new List<bool> { true, false, false, false, true, false }},    
        { "l", new List<bool> { true, false, true, false, true, false }},     
        { "m", new List<bool> { true, true, false, false, true, false }},     
        { "n", new List<bool> { true, true, false, true, true, false }},      
        { "o", new List<bool> { true, false, false, true, true, false }},     
        { "p", new List<bool> { true, true, true, false, true, false }},      
        { "q", new List<bool> { true, true, true, true, true, false }},       
        { "r", new List<bool> { true, false, true, true, true, false }},      
        { "s", new List<bool> { false, true, true, false, true, false }},     
        { "t", new List<bool> { false, true, true, true, true, false }},      
        { "u", new List<bool> { true, false, false, false, true, true }},     
        { "v", new List<bool> { true, false, true, false, true, true }},      
        { "w", new List<bool> { false, true, true, true, false, true }},      
        { "x", new List<bool> { true, true, false, false, true, true }},      
        { "y", new List<bool> { true, true, false, true, true, true }},       
        { "z", new List<bool> { true, false, false, true, true, true }},      
    
        // Umlaute
        { "ä", new List<bool> { false, true, false, true, true, false }},
        { "ö", new List<bool> { false, true, true, false, false, true }},
        { "ü", new List<bool> { true, false, true, true, false, true }},
        { "ß", new List<bool> { false, true, true, false, true, true }},
    
        // Kurzschrift characters
        { "au", new List<bool> { true, false, false, false, false, true }},   
        { "äu", new List<bool> { false, true, false, false, true, false }},    
        { "eu", new List<bool> { true, false, true, false, false, true }},    
        { "ei", new List<bool> { true, true, false, false, false, true }},   
        { "ie", new List<bool> { false, true, false, false, true, true }},   
        { "ch", new List<bool> { true, true, false, true, false, true }},  
        { "sch", new List<bool> { true, false, false, true, false, true }},
        { "st", new List<bool> { false, true, true, true, true, true }},
    
        // punctuation
        { ",", new List<bool> { false, false, true, false, false, false }},   
        { ".", new List<bool> { false, false, false, false, true, false }},     
        { ";", new List<bool> { false, false, true, false, true, false }},    
        { ":", new List<bool> { false, false, true, true, false, false }},   
        { "?", new List<bool> { false, false, true, false, false, true }},
        { "!", new List<bool> { false, false, true, true, true, false }},
        { "(", new List<bool> { false, false, true, true, true, true }},     
        { ")", new List<bool> { false, false, true, true, true, true }},     
        { "„", new List<bool> { false, false, true, false, true, true }},
        { "“", new List<bool> { false, false, false, true, true, true }},
        { "-", new List<bool> { false, false, false, false, true, true }},
        { "'", new List<bool> { false, false, false, false, false, true }},
    
        // number indicator 
        { "#", new List<bool> { false, true, true, true, true, true }},
    };
     
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ConvertTextToBraille("Hallo freunde!");
    }
    


    public GameObject ConvertCharacterToBraille(string s)
    {
        if (!german.TryGetValue(s, out var pattern))
        {
            Debug.LogWarning($"{s} is not supported");
            return null;
        }

        var brailleObject = Instantiate(brailleCharacterPrefab).GetComponent<BrailleObject>();
        brailleObject.gameObject.name = s;
        brailleObject.SetBrailleCharacter(pattern);

        return brailleObject.gameObject;
    }
    
    public GameObject ConvertWordToBraille(string s)
    {
        var wordObject = Instantiate(wordObjectPrefab);
        
        CharFactory text = new CharFactory(s);
        StringBuilder  character = new StringBuilder();

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
            Debug.Log("creating braille using string: "+ character.ToString());
            var brailleObject = ConvertCharacterToBraille(character.ToString());
            text.Next();
            if (brailleObject != null)
            {
                brailleObject.transform.SetParent(wordObject.transform, false);
            }
        } 
        return wordObject.gameObject;
    }
    
    /// <summary>
    /// Converts input string s into a <c>textObject</c> which handles output through Screenreader/Braille isplay
    /// </summary>
    /// <param name="s">string</param>
    /// <param name="outputType">enum OutputType, can be either BRAILLE, SPEAK or BOTH while BOTH is selected as default</param>
    /// <returns></returns>
    public GameObject ConvertTextToBraille(string s, AssistiveOutput.OutputType outputType = AssistiveOutput.OutputType.BOTH)
    {
        var textObject = Instantiate(textObjectPrefab,transform);
        textObject.GetComponent<TextObject>().text = s;
        textObject.GetComponent<TextObject>().outputType = outputType;
        
        
        CharFactory text = new CharFactory(s);
        StringBuilder  word = new StringBuilder();

        while (text.HasNext)
        {
            if (Char.IsWhiteSpace(text.Curr))
            {
                text.Next();
            }
            
            if (Char.IsDigit(text.Curr))
            {
                word.Append('#');
                while (Char.IsDigit(text.Curr))
                {
                    word.Append(ConvertNumberToChar(text.Curr));
                    text.Next();
                }
                Debug.Log("creating word using string: "+ word.ToString());
                var wordObject = ConvertWordToBraille(word.ToString());
                //wordObject.name = word.ToString();
                wordObject.transform.SetParent(textObject.transform, false);
                word.Clear();
            }
            else
            {
                while ((Char.IsLetter(text.Curr) || Char.IsPunctuation(text.Curr)) && !Char.IsDigit(text.Curr))
                {
                    word.Append(Char.ToLower(text.Curr));
                    text.Next();
                }
                Debug.Log("creating word using string: "+ word.ToString());
                var wordObject = ConvertWordToBraille(word.ToString());
                //wordObject.name = word.ToString();
                wordObject.transform.SetParent(textObject.transform, false);
                word.Clear();
            }
        }
        return textObject.gameObject;
    }

    
    
    //takes a single digit and converts it into its corresponding character for braille representation
    private Char ConvertNumberToChar(char c)
    {
        switch (c)
        {
            case '1': return('a');
            case '2': return('b');
            case '3': return('c');
            case '4': return('d');
            case '5': return('e');
            case '6': return('f');
            case '7': return('g');
            case '8': return('h');
            case '9': return('i');
            case '0': return('j');
            default: Debug.Log(c + " is not a number."); return '\0';
        }
    }
    
}