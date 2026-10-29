using System.Collections.Generic;
using UnityEngine;

public class CSVParser<TData> where TData : ICSVParseData, new()
{
    public bool TryParse(string name, out List<TData> result)
    {
        TextAsset csvData = Resources.Load<TextAsset>(name);

        if (csvData == null)
        {
            Debug.LogError($"CSV 파일을 찾을 수 없습니다: {name}");
            result = null;
            return false;
        }

        // 줄바꿈 기준으로 행 나누기
        string[] data = csvData.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        result = new();
        for (int i = 0; i < data.Length; i++)
        {
            // 첫 번째 줄 헤더(제목 행) 건너뛰기
            if (i == 0) continue;

            string[] row = data[i].Split(',');

            TData tData = new();
            (tData as ICSVParseData).SetData(row);
            result.Add(tData);
        }
        
        return true;
    }
}