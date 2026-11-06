using UnityEngine;
using UnityEngine.Video;
using System;

public class ChoiceController : MonoBehaviour
{
    public event Func<ChoiceData> OnGetNextChoiceDataRequested;
    public event Func<float> OnGetCurrentPlayerTimeRequest;
    public event Func<VideoPlayer> OnGetCurrentPlayerRequested;

    // ChoiceShower 연결 event
    public event Action<ChoiceData> OnStartShowChoiceRequested;
    public event Action<string> OnRemoveChoiceRequested;

    private readonly ChoiceShower choiceShower = new();
    private ChoiceData currentChoiceData;

    private void OnEnable()
    {
        choiceShower.OnStartShowChoiceRequested += HandleStartShowChoice;
        choiceShower.OnRemoveChoiceRequested += HandleRemoveChoice;
    }

    private void OnDisable()
    {
        choiceShower.OnStartShowChoiceRequested -= HandleStartShowChoice;
        choiceShower.OnRemoveChoiceRequested -= HandleRemoveChoice;
    }

    private void Update()
    {
        JudgeChoiceTime();

        choiceShower.Update();
    }

    private void JudgeChoiceTime()
    {
        if (TryGetCurrentPlayerTime(out float currentTime))
        {
            if (IsChoiceShowTime(currentTime))
            {
                ShowChoice(currentChoiceData, currentTime);
            }
        }
    }

    /// <summary>
    /// 현재 Video Player의 재생 시간을 받아옴
    /// </summary>
    private bool TryGetCurrentPlayerTime(out float time)
    {
        if (OnGetCurrentPlayerTimeRequest == null)
        {
            time = float.MinValue;
            return false;
        }

        time = OnGetCurrentPlayerTimeRequest.Invoke();

        // 받은 시간이 0 이상이면 true
        return time >= 0;
    }

    /// <summary>
    /// currentTime과 currentChoiceData의 StartTime을 비교해 자막 재생 시간을 판정
    /// </summary>
    private bool IsChoiceShowTime(float currentTime)
    {
        if (currentChoiceData == null)
        {
            return false;
        }

        // 현재 시간이 자막 시작 시간 이후일 경우 true
        if (currentTime >= currentChoiceData.StartTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowChoice(ChoiceData choiceData, float currentTime)
    {
        VideoPlayer videoPlayer = OnGetCurrentPlayerRequested?.Invoke();
        // choiceShower.StartShowChoice(videoPlayer, choiceData);

        GetNextChoiceData(currentTime);
    }

    private void GetNextChoiceData(float currentTime)
    {
        currentChoiceData = OnGetNextChoiceDataRequested?.Invoke();

        // 새로 받아온 ChoiceData의 시작 시간이 현재 시간 이전일 경우 오류임으로 다음 ChoiceData를 받아옴
        if (currentChoiceData != null && currentChoiceData.StartTime < currentTime)
        {
            Debug.LogWarning("새 ChoiceData의 시작시간이 현재시간 이전");
            GetNextChoiceData(currentTime);
        }
    }

    public void GetChoiceDataOnLoadNextNode()
    {
        // 현재 node의 마지막 자막이 끝났을 때 CurrentChoiceDatas에 다음 node의 자막 데이터들이 저장되지 않은 경우
        // CurrentNode가 변경되었을 때 nextChoiceData를 다시 한번 받아옴
        if (currentChoiceData == null)
        {
            GetNextChoiceData(0);
        }
    }

    private void HandleRemoveChoice(string id)
    {
        OnRemoveChoiceRequested?.Invoke(id);
    }

    private void HandleStartShowChoice(ChoiceData choiceData)
    {
        OnStartShowChoiceRequested?.Invoke(choiceData);
    }
}
