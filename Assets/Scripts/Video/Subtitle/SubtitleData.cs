public class SubtitleData : ICSVParseData
{
    public string ID;
    public float StartTime;
    public float EndTime;
    public string Character;
    public string Text_KR;
    public string Text_EN;

    public void SetData(string[] datas)
    {
        ID = datas[0];
        StartTime = float.TryParse(datas[1], out float startTime) ? startTime : float.MinValue;
        EndTime = float.TryParse(datas[2], out float endTime) ? endTime : float.MinValue;
        Character = datas[3];
        Text_KR = datas[4];
        Text_EN = datas[5];
    }
}
