using UnityEngine;
using UnityEngine.Video;
using System;
using System.Collections;

public class TransitionFade : ITransitionPolicy
{
    public event Action OnTransitionStart;

    private readonly VideoPlayController videoPlayController;

    public TransitionPolicyEnum PolicyEnum { get; private set; } = TransitionPolicyEnum.Fade;
    
    private readonly float fadeLimit = 0.9f;

    public TransitionFade(VideoPlayController videoPlayController)
    {
        this.videoPlayController = videoPlayController;
    }

    public IEnumerator WaitForTransition(TransitionData transitionData)
    {
        VideoPlayerWrapper current = transitionData.Current;
        VideoPlayerWrapper next = transitionData.Next;
        float fadeTime = (transitionData as TransitionFadeData).Time;

        VideoPlayer currentPlayer = current.VideoPlayer;
        VideoPlayer nextPlayer = next.VideoPlayer;

        while (true)
        {
            if (current.VideoPlayer.isLooping)
            {
                continue;
            }

            if (currentPlayer.length - currentPlayer.time <= fadeTime)
            {
                // loop중 시간이 fadeTime에서 fadeLimit만큼 더 진행됐다면 다음 loop에서 transition이 진행되도록
                if(currentPlayer.length - currentPlayer.time <= fadeTime - fadeLimit)
                {
                    continue;
                }
                
                break;
            }
            yield return null;
        }

        float currentTime = 0;
        ReadyFadeIn(next);
        while (currentPlayer.isPlaying)
        {
            DoFadeOut(current, fadeTime, currentTime);
            currentTime += Time.deltaTime;

            WaitFadeIn(nextPlayer);

            yield return null;
        }

        StartFadeIn(next);
        FinishFadeOut(current);

        currentTime = 0;
        while (currentTime <= fadeTime)
        {
            DoFadeIn(next, fadeTime, currentTime);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private void DoFadeOut(VideoPlayerWrapper current, float totalTime, float currentTime)
    {
        float ratio = 1 - (currentTime / totalTime);

        DoFade(current, ratio);
    }

    private void FinishFadeOut(VideoPlayerWrapper current)
    {
        // fade out끝난 텍스처를 원래대로
        current.EndVideo();
        current.SetColor(1);
    }

    private void ReadyFadeIn(VideoPlayerWrapper next)
    {
        next.SetColor(0, 0, 0, 0);
        next.ActiveVideo();

        VideoPlayer player = next.VideoPlayer;
        isFirstFrameReady = false;

        player.sendFrameReadyEvents = true;
        player.frameReady += HandlerFrameReadyEvent;
        player.Play();
    }

    private void WaitFadeIn(VideoPlayer nextPlayer)
    {
        if (!isFirstFrameReady) return;

        nextPlayer.frameReady -= HandlerFrameReadyEvent;
        nextPlayer.sendFrameReadyEvents = false;
        nextPlayer.Pause(); // 첫 프레임을 받은 상태로 대기
    }

    private bool isFirstFrameReady;
    private void HandlerFrameReadyEvent(VideoPlayer source, long frameIndex)
    {
        isFirstFrameReady = true;
    }

    private void StartFadeIn(VideoPlayerWrapper next)
    {
        OnTransitionStart?.Invoke();

        // fade in 할 텍스처를 검은색으로
        next.SetColor(0, 0, 0, 1);
        videoPlayController.PlayLoadedVideo(next.VideoPlayer.url);
    }

    private void DoFadeIn(VideoPlayerWrapper next, float totalTime, float currentTime)
    {
        float ratio = currentTime / totalTime;

        DoFade(next, ratio);
    }

    private void DoFade(VideoPlayerWrapper wrapper, float ratio)
    {
        wrapper.SetColor(ratio);
    }
}
