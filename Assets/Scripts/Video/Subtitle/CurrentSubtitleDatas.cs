using System.Collections.Generic;

public class CurrentSubtitleDatas
{
    private readonly Queue<SubtitleData> subtitleDataQueue = new();
    private readonly Queue<ChoiceData> choiceDataQueue = new();

    private readonly SubtitleDatas subtitleDatas;
    private readonly ChoiceDatas choiceDatas;

    public CurrentSubtitleDatas(SubtitleDatas subtitleDatas, ChoiceDatas choiceDatas)
    {
        this.subtitleDatas = subtitleDatas;
        this.choiceDatas = choiceDatas;
    }

    /// <summary>
    /// BranchController의 currentNode가 변경되면 해당 node의 code에 맞는 subtitleData들을 queue에 저장
    /// </summary>
    public void SetCurrentNodeSubtitleDatas(string code)
    {
        List<SubtitleData> subtitleDatasByCode = subtitleDatas.GetData(code);

        foreach (var subtitleData in subtitleDatasByCode)
        {
            subtitleDataQueue.Enqueue(subtitleData);
        }

        List<ChoiceData> choiceDatasByCode = choiceDatas.GetData(code);
        
        foreach (var choiceData in choiceDatasByCode)
        {
            choiceDataQueue.Enqueue(choiceData);
        }
    }

    public SubtitleData GetSubtitleData()
    {
        if (subtitleDataQueue.Count == 0)
        {
            return null;
        }

        return subtitleDataQueue.Dequeue();
    }
    
    public ChoiceData GetChoiceData()
    {
        if (choiceDataQueue.Count == 0)
        {
            return null;
        }

        return choiceDataQueue.Dequeue();
    }
}
