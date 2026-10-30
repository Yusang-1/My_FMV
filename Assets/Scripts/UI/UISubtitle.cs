using UnityEngine;
using TMPro;

public class UISubtitle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    public void SetText(char[] text, char[] character)
    {
        characterText.SetText(character);
        dialogueText.SetText(text);
    }
}
