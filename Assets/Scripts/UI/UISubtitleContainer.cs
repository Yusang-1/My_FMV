using UnityEngine;
using TMPro;

public class UISubtitleContainer : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI subtitleText;

    private readonly char[] blank = { };
    
    public void ShowSubtitle(SubtitleData data)
    {
        char[] text = data.Text_EN.ToCharArray(); // 수정
        
        subtitleText.SetText(text);
    }
    
    public void RemoveSubtitle(string id)
    {
        subtitleText.SetText(blank);
    }
}
