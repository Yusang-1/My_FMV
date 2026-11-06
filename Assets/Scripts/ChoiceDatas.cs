using System.Collections.Generic;

public class ChoiceDatas
{
    private Dictionary<string, List<ChoiceData>> choiceDatasByNodeCode;

    /// <summary>
    /// nodeCode를 받아 ChoiceData list를 반환
    /// </summary>
    public List<ChoiceData> GetData(string code)
    {
        if (!choiceDatasByNodeCode.ContainsKey(code))
        {
            return null;
        }

        return choiceDatasByNodeCode[code];
    }

    /// <summary>
    /// choice 데이터 parse를 완료한 뒤 choice 데이터가 포함된 노드의 코드에 따라 dictionary에 분류
    /// </summary>    
    public void AddData(List<ChoiceData> datas)
    {
        choiceDatasByNodeCode = new(datas.Count);

        foreach (var data in datas)
        {
            // 자막 id를 자막이 포함된 node의 code로 변경
            string nodeCode = ChoiceIDToNodeCode(data.ID);

            if (!choiceDatasByNodeCode.ContainsKey(nodeCode))
            {
                choiceDatasByNodeCode.Add(nodeCode, new());
            }
            choiceDatasByNodeCode[nodeCode].Add(data);
        }
    }
    
    private readonly char target = '_';
    private string ChoiceIDToNodeCode(string subtitleID)
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
