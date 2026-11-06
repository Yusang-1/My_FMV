using UnityEngine;
using System;
using System.Collections.Generic;

public class ChoiceTextLoader : MonoBehaviour
{
    public event Action<List<ChoiceTextData>> OnParseChoiceCompelete;

    private readonly CSVParser<ChoiceTextData> parser = new();

    [SerializeField] private string fileName;

    private void Start()
    {
        if (parser.TryParse(fileName, out List<ChoiceTextData> datas))
        {
            OnParseChoiceCompelete?.Invoke(datas);
        }
        else
        {
            Debug.LogWarning("CSV 파싱 실패");
        }
    }
}
