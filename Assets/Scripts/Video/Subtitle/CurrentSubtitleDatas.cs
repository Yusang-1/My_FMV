using System.Collections.Generic;

public class CurrentSubtitleDatas
{
    private readonly Queue<SubtitleData> queue = new();

    private readonly SubtitleDatas subtitleDatas;

    public CurrentSubtitleDatas(SubtitleDatas subtitleDatas)
    {
        this.subtitleDatas = subtitleDatas;
    }

    /// <summary>
    /// BranchController의 currentNode가 변경되면 해당 node의 code에 맞는 subtitleData들을 queue에 저장
    /// </summary>
    public void SetCurrentNodeSubtitleDatas(string code)
    {
        List<SubtitleData> datas = subtitleDatas.GetData(code);

        foreach (var data in datas)
        {
            queue.Enqueue(data);
        }
    }

    public SubtitleData GetSubtitleData()
    {
        if (queue.Count == 0)
        {
            return null;
        }

        return queue.Dequeue();
    }
}
