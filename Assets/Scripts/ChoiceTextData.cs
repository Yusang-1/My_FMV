public class ChoiceTextData : ICSVParseData
{
    public string ChoiceID;
    public string Text_KR;
    public string Text_EN;

    public void SetData(string[] datas)
    {
        ChoiceID = datas[0];
        Text_KR = datas[1];
        Text_EN = datas[2];
    }
}
