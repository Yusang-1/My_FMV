using UnityEngine;
using System;

public class SubtitleController : MonoBehaviour
{
    public event Action<char[], float> OnShowSubtitleRequested;
    public event Func<SubtitleData> OnGetNextSubtitleDataRequested;
    public event Func<float> OnGetCurrentPlayerTimeRequest;

    private SubtitleData currentSubtitleData;

    private void Update()
    {
        JudgeSubtitle();
    }

    private void JudgeSubtitle()
    {
        if (TryGetCurrentPlayerTime(out float currentTime))
        {
            if (IsSubtitleShowTime(currentTime))
            {
                ShowSubtitle(currentSubtitleData.Text_EN.ToCharArray(), currentTime, currentSubtitleData.EndTime);
            }
        }
    }

    private bool TryGetCurrentPlayerTime(out float time)
    {
        if (OnGetCurrentPlayerTimeRequest == null)
        {
            time = float.MinValue;
            return false;
        }

        time = OnGetCurrentPlayerTimeRequest.Invoke();
        return true;
    }

    private bool IsSubtitleShowTime(float currentTime)
    {
        if (currentSubtitleData == null)
        {
            return false;
        }

        // 현재 시간이 자막 시작 시간 이후일 경우 true
        if (currentTime >= currentSubtitleData.StartTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowSubtitle(char[] text, float currentTime, float endTime)
    {
        OnShowSubtitleRequested?.Invoke(text, endTime - currentTime);

        GetNextSubtitleData(currentTime);
    }

    private void GetNextSubtitleData(float currentTime)
    {
        currentSubtitleData = OnGetNextSubtitleDataRequested?.Invoke();

        // 새로 받아온 subtitleData의 시작 시간이 현재 시간 이전일 경우 오류임으로 다음 subtitleData를 받아옴
        if (currentSubtitleData != null && currentSubtitleData.StartTime < currentTime)
        {
            Debug.LogWarning("새 SubtitleData의 시작시간이 현재시간 이전");
            GetNextSubtitleData(currentTime);
        }
    }
}
