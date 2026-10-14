using UnityEngine;
using UnityEngine.Video;
using System;

public class JudgePreLoadVideo : MonoBehaviour
{
    public event Action OnPreLoadRequired;

    private VideoPlayer currentVideoPlayer;

    private float loadNextVideoJudgeTime;
    private bool isJudged;

    private void Update()
    {
        JudgeLoadVideo();
    }

    public void Initialize(LoadVideoRunTimeContext loadVideoRunTimeContext)
    {
        loadNextVideoJudgeTime = loadVideoRunTimeContext.LoadNextTimeBeforeVideoEnd;
    }

    /// <summary>
    /// 새로운 영상이 재생되면 호출
    /// </summary>
    public void StartNewJudge(VideoPlayer source)
    {
        currentVideoPlayer = source;
        isJudged = false;
    }

    /// <summary>
    /// 영상의 남은 길이를 측정해 pre load를 요청
    /// </summary>
    private void JudgeLoadVideo()
    {
        if (currentVideoPlayer == null) return;
        if (isJudged) return;

        if (currentVideoPlayer.length - currentVideoPlayer.time <= loadNextVideoJudgeTime)
        {
            isJudged = true;
            currentVideoPlayer = null;
            OnPreLoadRequired?.Invoke();
        }
    }
}
