using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UISubtitleContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] subtitleTexts;
    private readonly Queue<TextMeshProUGUI> tmpQueue = new();

    private readonly Dictionary<string, TextMeshProUGUI> textByID = new();

    private readonly char[] blank = { };

    private void Start()
    {
        foreach (var tmp in subtitleTexts)
        {
            tmpQueue.Enqueue(tmp);
        }
    }

    public void ShowSubtitle(SubtitleData data)
    {
        char[] text = data.Text_EN.ToCharArray(); // 수정

        // queue에서 사용하지 않는 tmp가져옴
        TextMeshProUGUI tmp;
        if (tmpQueue.Count == 0)
        {
            Debug.LogWarning("queue에 subtitle tmp부족");
            return;
        }
        else
        {
            tmp = tmpQueue.Dequeue();
        }

        // textByID에 subtitle id와 tmp를 dictionary에 저장
        if (textByID.ContainsKey(data.ID))
        {
            textByID[data.ID] = tmp;
        }
        else
        {
            textByID.Add(data.ID, tmp);
        }

        tmp.SetText(text);
        tmp.gameObject.SetActive(true);
    }

    public void RemoveSubtitle(string id)
    {
        if (!textByID.ContainsKey(id))
        {
            return;
        }

        TextMeshProUGUI tmp = textByID[id];
        textByID.Remove(id);
        tmpQueue.Enqueue(tmp);
        tmp.SetText(blank);
        tmp.gameObject.SetActive(false);
    }
}
