using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class BrailleConverter : MonoBehaviour
{
    public  static BrailleConverter Instance;
    
    [SerializeField] private GameObject brailleCharacterPrefab, groupObjectPrefab;
    
    //temporary Dictionary for Braille conversion
    Dictionary<string, List<bool>> german = new()
    {
        { "a", new List<bool> { true, false, false, false, false, false }},   // 1
        { "b", new List<bool> { true, false, true, false, false, false }},    // 1 2
        { "c", new List<bool> { true, true, false, false, false, false }},    // 1 4
        { "d", new List<bool> { true, true, false, true, false, false }},     // 1 4 5
        { "e", new List<bool> { true, false, false, true, false, false }},    // 1 5
        { "f", new List<bool> { true, true, true, false, false, false }},     // 1 2 4
        { "g", new List<bool> { true, true, true, true, false, false }},      // 1 2 4 5
        { "h", new List<bool> { true, false, true, true, false, false }},     // 1 2 5
        { "i", new List<bool> { false, true, true, false, false, false }},    // 2 4
        { "j", new List<bool> { false, true, true, true, false, false }},     // 2 4 5
        { "k", new List<bool> { true, false, false, false, true, false }},    // 1 3
        { "l", new List<bool> { true, false, true, false, true, false }},     // 1 2 3
        { "m", new List<bool> { true, true, false, false, true, false }},     // 1 3 4
        { "n", new List<bool> { true, true, false, true, true, false }},      // 1 3 4 5
        { "o", new List<bool> { true, false, false, true, true, false }},     // 1 3 5
        { "p", new List<bool> { true, true, true, false, true, false }},      // 1 2 3 4
        { "q", new List<bool> { true, true, true, true, true, false }},       // 1 2 3 4 5
        { "r", new List<bool> { true, false, true, true, true, false }},      // 1 2 3 5
        { "s", new List<bool> { false, true, true, false, true, false }},     // 2 3 4
        { "t", new List<bool> { false, true, true, true, true, false }},      // 2 3 4 5
        { "u", new List<bool> { true, false, false, false, true, true }},     // 1 3 6
        { "v", new List<bool> { true, false, true, false, true, true }},      // 1 2 3 6
        { "w", new List<bool> { false, true, true, true, false, true }},      // 2 4 5 6
        { "x", new List<bool> { true, true, false, false, true, true }},      // 1 3 4 6
        { "y", new List<bool> { true, true, false, true, true, true }},       // 1 3 4 5 6
        { "z", new List<bool> { true, false, false, true, true, true }},      // 1 3 5 6
    
        // Umlaute
        { "ä", new List<bool> { false, true, false, true, true, false }},
        { "ö", new List<bool> { false, true, true, false, false, true }},
        { "ü", new List<bool> { true, false, true, true, false, true }},
        { "ß", new List<bool> { false, true, true, false, true, true }},
    
        // Kurzschrift characters
        { "au", new List<bool> { false, false, true, false, true, false }},   // 2 3
        { "äu", new List<bool> { false, true, true, false, true, false }},    // approx
        { "eu", new List<bool> { false, false, true, true, true, false }},    // approx
        { "ei", new List<bool> { false, false, true, true, false, false }},   // approx
        { "ie", new List<bool> { false, false, true, false, false, true }},   // approx
        { "ch", new List<bool> { false, false, false, false, true, false }},  // dot 3
        { "sch", new List<bool> { false, false, true, false, true, true }},   // approx
        { "st", new List<bool> { false, false, true, true, false, true }},    // approx
    
        // punctuation
        { ",", new List<bool> { false, false, true, false, false, false }},   // dot 2
        { ".", new List<bool> { false, false, true, true, false, true }},     // 2 5 6
        { ";", new List<bool> { false, false, true, false, true, false }},    // 2 3
        { ":", new List<bool> { false, false, true, true, false, false }},    // 2 5
        { "?", new List<bool> { false, false, true, false, true, true }},     // 2 3 6
        { "!", new List<bool> { false, false, true, true, true, false }},     // 2 3 5
        { "(", new List<bool> { false, true, false, false, true, true }},     // approx
        { ")", new List<bool> { false, true, false, false, true, true }},     // same
        { "„", new List<bool> { false, false, true, false, true, true }},    // 4 5 6
        { "“", new List<bool> { false, false, false, true, true, true }},    // approx
        { "-", new List<bool> { false, false, false, false, true, true }},    // 3 6
        { "'", new List<bool> { false, false, false, false, true, false }},   // 3
    
        // number indicator 
        { "#", new List<bool> { false, true, true, true, true, true }},        // 3 4 5 6
    };
     
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ConvertTextToBraille("hello there");
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
        
        //printing pattern to letter
        String message = s + " = ";
        foreach (bool dot in pattern)
        {
            message = message + dot + ", ";
        }
        Debug.Log(message);

        return brailleObject.gameObject;
    }
    
    public GameObject ConvertWordToBraille(string s)
    {
        var wordObject = Instantiate(groupObjectPrefab);
        
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
    

    public GameObject ConvertTextToBraille(string s)
    {
        var textObject = Instantiate(groupObjectPrefab,transform);
        
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
                word.Append(text.Curr);
                while (Char.IsDigit(text.La))
                {
                    text.Next();
                    word.Append(ConvertNumberToChar(text.Curr));
                }
                Debug.Log("creating word using string: "+ word.ToString());
                var wordObject = ConvertWordToBraille(word.ToString());
                //wordObject.name = word.ToString();
                wordObject.transform.SetParent(textObject.transform, false);
                word.Clear();
            }
            else
            {
                word.Append(text.Curr);
                while (text.HasNext && !Char.IsWhiteSpace(text.Curr) && !Char.IsDigit(text.Curr))
                {
                    text.Next();
                    word.Append(text.Curr);
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