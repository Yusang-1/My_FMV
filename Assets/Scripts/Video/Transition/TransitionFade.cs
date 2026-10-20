using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class TransitionFade : ITransitionPolicy
{
    public TransitionPolicyEnum PolicyEnum { get; private set; } = TransitionPolicyEnum.Fade;

    public IEnumerator WaitForTransition(TransitionData transitionData)
    {
        VideoPlayerWrapper current = transitionData.Current;
        VideoPlayerWrapper next = transitionData.Next;
        float time = (transitionData as TransitionFadeData).Time;

        VideoPlayer currentPlayer = current.VideoPlayer;
        VideoPlayer nextPlayer = next.VideoPlayer;

        while (true)
        {
            if (currentPlayer.length - currentPlayer.time <= time)
            {
                break;
            }
            yield return null;
        }

        float currentTime = 0;
        ReadyFadeIn(next);
        while (currentPlayer.isPlaying)
        {
            DoFadeOut(current, time, currentTime);
            currentTime += Time.deltaTime;

            WaitFadeIn(nextPlayer);

            yield return null;
        }

        StartFadeIn(next);
        FinishFadeOut(current);

        currentTime = 0;
        while (currentTime <= time)
        {
            DoFadeIn(next, time, currentTime);

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
        // fade in 할 텍스처를 검은색으로
        next.SetColor(0, 0, 0, 1);
        next.PlayVideo();
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
