using UnityEngine;
using System.Collections.Generic;

public class UISubtitleContainer : MonoBehaviour
{
    [SerializeField] private UISubtitle[] subtitles;
    private readonly Queue<UISubtitle> tmpQueue = new();

    private readonly Dictionary<string, UISubtitle> subtitleByID = new();

    private readonly char[] blank = { };

    private void Start()
    {
        foreach (var tmp in subtitles)
        {
            tmpQueue.Enqueue(tmp);
        }
    }

    public void ShowSubtitle(SubtitleData data)
    {
        char[] text = data.Text_EN.ToCharArray(); // 수정
        char[] character = data.Character.ToCharArray(); // 수정

        // queue에서 사용하지 않는 tmp가져옴
        UISubtitle subtitle;
        if (tmpQueue.Count == 0)
        {
            Debug.LogWarning("queue에 subtitle tmp부족");
            return;
        }
        else
        {
            subtitle = tmpQueue.Dequeue();
        }

        // textByID에 subtitle id와 tmp를 dictionary에 저장
        if (subtitleByID.ContainsKey(data.ID))
        {
            subtitleByID[data.ID] = subtitle;
        }
        else
        {
            subtitleByID.Add(data.ID, subtitle);
        }

        subtitle.SetText(text, character);
        subtitle.gameObject.SetActive(true);
    }

    public void RemoveSubtitle(string id)
    {
        if (!subtitleByID.ContainsKey(id))
        {
            return;
        }

        UISubtitle subtitle = subtitleByID[id];
        subtitleByID.Remove(id);
        tmpQueue.Enqueue(subtitle);
        subtitle.SetText(blank, blank);
        subtitle.gameObject.SetActive(false);
    }
}
