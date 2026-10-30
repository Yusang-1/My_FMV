using UnityEngine;
using TMPro;

public class UISubtitle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    public void SetText(string text, string character)
    {        
        char[] characterChar = StringToCharArray.Convert(character, out int length);
        characterText.SetText(characterChar, 0, length);
        
        char[] textChar = StringToCharArray.Convert(text, out length);
        dialogueText.SetText(textChar, 0, length);
    }
    
    public void SetText(char[] text, char[] character)
    {
        characterText.SetText(character);
        dialogueText.SetText(text);
    }
}
