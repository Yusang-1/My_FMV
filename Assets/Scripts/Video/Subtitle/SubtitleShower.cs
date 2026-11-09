using UnityEngine.Video;
using System;

public class SubtitleShower
{
    // UI에서 SubtitleData의 ID를 이용해 on/off 관리
    public event Action<SubtitleData> OnStartShowSubtitleRequested;
    public event Action<string> OnRemoveSubtitleRequested;
    public event Action<ITimeCheck> OnStartTimeCheck;

    private Action<ITimeCheck> OnSubtitleEnd;

    public void Initialize()
    {
        OnSubtitleEnd = RemoveSubtitle;
    }

    public void StartShowSubtitle(VideoPlayer player, SubtitleData data)
    {
        // VideoTimer에 담아 종료 시점 체크
        (data as ITimeCheck).SetEndAction(OnSubtitleEnd);
        OnStartTimeCheck?.Invoke(data);

        // UIContainerSubtitleTotal에 자막 켜기 요청
        OnStartShowSubtitleRequested?.Invoke(data);
    }

    private void RemoveSubtitle(ITimeCheck timeCheck)
    {
        // UISubtitleContainer에 해당 ID의 자막 종료 요청
        SubtitleData data = timeCheck as SubtitleData;
        OnRemoveSubtitleRequested?.Invoke(data.ID);
    }
}
