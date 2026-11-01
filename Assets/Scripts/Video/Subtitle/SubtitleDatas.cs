using System.Collections.Generic;

public class SubtitleDatas
{
    private Dictionary<string, List<SubtitleData>> subtitleDatasByNodeCode;

    public List<SubtitleData> GetData(string code)
    {
        if (!subtitleDatasByNodeCode.ContainsKey(code))
        {
            return null;
        }

        return subtitleDatasByNodeCode[code];
    }

    /// <summary>
    /// 자막 데이터 parse를 완료한 뒤 자막 데이터가 포함된 노드의 코드에 따라 dictionary에 분류
    /// </summary>    
    public void AddData(List<SubtitleData> datas)
    {
        subtitleDatasByNodeCode = new(datas.Count);

        foreach (var data in datas)
        {
            // 자막 id를 자막이 포함된 node의 code로 변경
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
