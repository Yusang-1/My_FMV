using System;
using System.Collections.Generic;

public class ChoiceData : ICSVParseData, ITimeCheck
{
    private Action<ITimeCheck> endAction;

    public string ID;
    public float StartTime;
    public float Duration;
    public float EndTime;
    public List<string> ChoiceIDs = new();
    public ChoiceType ChoiceType;

    public void SetData(string[] datas)
    {
        ID = datas[0];
        StartTime = float.TryParse(datas[1], out float startTime) ? startTime : float.MinValue;
        Duration = float.TryParse(datas[2], out float duration) ? duration : float.MinValue;
        EndTime = float.TryParse(datas[3], out float endTime) ? endTime : float.MinValue;

        if (Duration > 0)
        {
            ChoiceType = ChoiceType.Default;
        }
        else
        {
            ChoiceType = ChoiceType.HasTimeLimit;
        }

        string[] d = datas[4].Split('/');
        foreach (string s in d)
        {
            ChoiceIDs.Add(s);
        }
    }

    public void SetEndAction(Action<ITimeCheck> endAction)
    {
        this.endAction = endAction;
    }

    public void EndTimeArrived()
    {
        endAction?.Invoke(this);
    }

    public float GetEndTime() => EndTime;
}

public enum ChoiceType
{
    Default,
    HasTimeLimit
}
