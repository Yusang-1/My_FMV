using UnityEngine;
using System.Collections;
using TMPro;

public class UISubtitleContainer : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI subtitleText;

    private IEnumerator subtitleTimerEnumerator;

    private readonly char[] blank = { };

    public void ShowSubtitle(char[] text, float duration)
    {
        subtitleText.SetText(text);

        // 새 텍스트가 설정되면 기존 SubtitleTimer를 중단, 삭제하고 새로 Timer를 시작
        if (subtitleTimerEnumerator != null)
        {
            StopCoroutine(subtitleTimerEnumerator);
            subtitleTimerEnumerator = null;
        }

        subtitleTimerEnumerator = SubtitleTimer(duration);
        StartCoroutine(subtitleTimerEnumerator);
    }

    public void RemoveSubtitle()
    {
        subtitleText.SetText(blank);
    }

    /// <summary>
    /// duration이 지나면 text를 빈칸으로 설정
    /// </summary>
    private IEnumerator SubtitleTimer(float duration)
    {
        float time = 0;

        while (time <= duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        RemoveSubtitle();
    }
}
