using UnityEngine;
using UnityEngine.Video;
using System;
using System.Collections;

public class JudgePreLoadVideo : MonoBehaviour
{
    public event Action OnPreLoadRequired;

    private VideoPlayer currentVideoPlayer;

    private float loadNextVideoJudgeTime;

    public void Initialize(LoadVideoRunTimeContext loadVideoRunTimeContext)
    {
        loadNextVideoJudgeTime = loadVideoRunTimeContext.LoadNextTimeBeforeVideoEnd;
    }

    /// <summary>
    /// 새로운 영상이 재생되면 호출
    /// </summary>
    public void StartNewJudge(VideoPlayer source)
    {
        StopAllCoroutines();
        
        currentVideoPlayer = source;
        if (currentVideoPlayer == null)
        {
            return;
        }
        
        StartCoroutine(JudgeLoadVideo());
    }

    /// <summary>
    /// 영상의 남은 길이를 측정해 pre load를 요청
    /// </summary>
    private IEnumerator JudgeLoadVideo()
    {
        while (true)
        {
            if (currentVideoPlayer.length - currentVideoPlayer.time <= loadNextVideoJudgeTime)
            {
                currentVideoPlayer = null;
                OnPreLoadRequired?.Invoke();
                break;
            }
            yield return null;
        }
    }
}
