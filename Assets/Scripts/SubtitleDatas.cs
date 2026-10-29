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
        // NodeCode는 두번째 '_' 전까지
        int firstIndex = subtitleID.IndexOf(target);
        int index = subtitleID.IndexOf(target, firstIndex + 1);

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
