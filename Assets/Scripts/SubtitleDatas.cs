using System.Collections.Generic;

public class SubtitleDatas
{
    private readonly Dictionary<string, List<SubtitleData>> subtitleDatasByNodeCode = new();

    public void AddData(List<SubtitleData> datas)
    {
        foreach (var data in datas)
        {
            string nodeCode = SubtitleIDToNodeCode(data.ID);

            if (!subtitleDatasByNodeCode.ContainsKey(nodeCode))
            {
                subtitleDatasByNodeCode.Add(nodeCode, new());
            }
            subtitleDatasByNodeCode[nodeCode].Add(data);
        }
    }

    private readonly char target = '_';
    private string SubtitleIDToNodeCode(string subtitleID)
    {
        int index = subtitleID.IndexOf(target);

        if (index != -1)
        {
            // 범위 연산자 0번째부터 index 전까지
            return subtitleID[..index];
        }
        else
        {
            return subtitleID;
        }
    }
}
