using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BrailleConverter : MonoBehaviour
{
    public  static BrailleConverter Instance;
    
    [SerializeField] private GameObject brailleCharacterPrefab;
    
    
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
    
    //space
    { " ", new List<bool> { false, false, false, false, false, false, }} 
};
    private void Awake()
    {
        Instance = this;
    }

    public GameObject ConvertCharacterToBraille(string s)
    {
        var brailleObject = Instantiate(brailleCharacterPrefab).GetComponent<BrailleObject>();
        brailleObject.SetBrailleCharacter(german[s]);
        return brailleObject.gameObject;
    }
    
    public GameObject ConvertWordToBraille(string s)
    {
        //TODO: Implement method
        //separate word into characters
        //ERROR HANDLING
        //ConvertCharacterToBraille()
        throw new Exception("Not implemented");
    }
    
    public GameObject ConvertTextToBraille(string s)
    {
        //TODO: Implement method
        //separate text into words
        //if number: add # before
        //ConvertWordToBraille()
        throw new Exception("Not implemented");
    }
}