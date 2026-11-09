using UnityEngine.Video;
using System;

public class ChoiceShower
{
    // UI에서 ChoiceData의 ID를 이용해 on/off 관리
    public event Action<string, string> OnStartShowChoiceRequested;
    public event Action<ChoiceData> OnStartSetChoiceRequested;
    public event Action<string> OnRemoveChoiceRequested;
    public event Func<string, ChoiceTextData> OnGetChoiceTextRequested;
    public event Action<ITimeCheck> OnStartTimeCheck;

    private Action<ITimeCheck> OnSubtitleEnd;

    public void Initialize()
    {
        OnSubtitleEnd = RemoveChoice;
    }

    public void StartShowChoice(VideoPlayer player, ChoiceData data)
    {
        // VideoTimer에 담아 종료 시점 체크
        (data as ITimeCheck).SetEndAction(OnSubtitleEnd);
        OnStartTimeCheck?.Invoke(data);

        // UIContainerSubtitleTotal에 자막 켜기 요청
        foreach (string choiceID in data.ChoiceIDs)
        {
            var choiceData = OnGetChoiceTextRequested?.Invoke(choiceID);
            OnStartShowChoiceRequested?.Invoke(data.ID, choiceData.Text_EN);
            OnStartSetChoiceRequested?.Invoke(data);
        }
    }

    private void RemoveChoice(ITimeCheck timeCheck)
    {
        // UIChoiceContainer에 해당 ID의 자막 종료 요청
        ChoiceData data = timeCheck as ChoiceData;
        OnRemoveChoiceRequested?.Invoke(data.ID);
    }
}
