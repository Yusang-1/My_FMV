using UnityEngine;
using System;
using System.Collections.Generic;

public class SubtitleLoader : MonoBehaviour
{
    public event Action<List<SubtitleData>> OnParseSubtitleCompelete;
    
    private readonly CSVParser<SubtitleData> parser = new();    

    [SerializeField] private string fileName;

    private void Start()
    {
        if (parser.TryParse(fileName, out List<SubtitleData> datas))
        {
            OnParseSubtitleCompelete?.Invoke(datas);
        }
        else
        {
            Debug.LogWarning("CSV 파싱 실패");
        }
    }
}
