public class SubtitleRuntimeContext
{
    public SubtitleDatas SubtitleDatas { get; private set; } = new();
    public ChoiceDatas ChoiceDatas { get; private set; } = new();
    public ChoiceTextDatas ChoiceTextDatas { get; private set; } = new();
    public CurrentSubtitleDatas CurrentSubtitleDatas { get; private set; }

    public SubtitleRuntimeContext()
    {
        CurrentSubtitleDatas = new(SubtitleDatas, ChoiceDatas);
    }
}
