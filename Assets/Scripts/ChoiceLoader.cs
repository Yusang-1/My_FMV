using UnityEngine;
using System;
using System.Collections.Generic;

public class ChoiceLoader : MonoBehaviour
{
    public event Action<List<ChoiceData>> OnParseChoiceCompelete;
    
    private readonly CSVParser<ChoiceData> parser = new();    

    [SerializeField] private string fileName;

    private void Start()
    {
        if (parser.TryParse(fileName, out List<ChoiceData> datas))
        {
            OnParseChoiceCompelete?.Invoke(datas);
        }
        else
        {
            Debug.LogWarning("CSV 파싱 실패");
        }
    }
}
