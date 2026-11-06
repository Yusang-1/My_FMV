using UnityEngine;
using UnityEngine.Video;
using System;
using System.Collections;

public class ChoiceDurationController : MonoBehaviour
{
    public event Func<VideoPlayer> OnGetVideoPlayerRequested;
    public event Action<float> OnCheckDurationStarted;
    public event Action<float> OnCheckDurationPerformed;
    public event Action<ChoiceData> OnNotChoose;

    public void StartChoice(ChoiceData choiceData)
    {
        // duration이 음수인 경우 선택할때까지 넘어가지 않음
        if (choiceData.Duration < 0)
        {

        }
        else
        {
            VideoPlayer player = OnGetVideoPlayerRequested?.Invoke();
            StartCoroutine(CheckDuration(choiceData, player));
        }
    }

    private IEnumerator CheckDuration(ChoiceData choiceData, VideoPlayer player)
    {
        // UITimer를 활성화하고 값 세팅
        OnCheckDurationStarted?.Invoke(choiceData.Duration);

        while (true)
        {
            // 남은 시간만큼 UITimer 변경
            OnCheckDurationPerformed?.Invoke(choiceData.EndTime - (float)player.time);
            
            if (player.time > choiceData.EndTime)
            {
                break;
            }
            yield return null;
        }

        // 아무것도 고르지 않았을 때
        OnNotChoose?.Invoke(choiceData);
    }
    
    /// <summary>
    /// 중간에 선택지를 골랐을 때 checkDuration을 멈춘다
    /// </summary>
    public void StopCheckDuration()
    {
        StopAllCoroutines();
    }
}
