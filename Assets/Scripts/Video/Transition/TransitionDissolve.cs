using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class TransitionDissolve : ITransitionPolicy
{
    private readonly VideoPlayController videoPlayController;

    public TransitionPolicyEnum PolicyEnum { get; private set; } = TransitionPolicyEnum.Dissolve;

    public TransitionDissolve(VideoPlayController videoPlayController)
    {
        this.videoPlayController = videoPlayController;
    }

    public IEnumerator WaitForTransition(TransitionData transitionData)
    {
        VideoPlayerWrapper current = transitionData.Current;
        VideoPlayerWrapper next = transitionData.Next;
        float time = (transitionData as TransitionDissolveData).Time;

        VideoPlayer currentPlayer = current.VideoPlayer;
        while (true)
        {
            if (currentPlayer.length - currentPlayer.time <= time)
            {
                break;
            }
            yield return null;
        }

        StartDissolve(next);
        float currentTime = 0;
        while (currentPlayer.isPlaying)
        {
            DoTransition(current, next, time, currentTime);

            currentTime += Time.deltaTime;
            yield return null;
        }
        FinishDissolve(current, next);
    }

    private void StartDissolve(VideoPlayerWrapper next)
    {
        // next의 alpha를 0으로
        next.SetAlpha(0);
        videoPlayController.PlayLoadedVideo(next.VideoPlayer.url);
    }

    private void DoTransition(VideoPlayerWrapper current, VideoPlayerWrapper next, float totalTime, float currentTime)
    {
        float ratio = currentTime / totalTime;

        // current의 alpha를 1 - ratio로
        current.SetAlpha(1 - ratio);
        // next의 alpha를 ratio로
        next.SetAlpha(ratio);
    }

    private void FinishDissolve(VideoPlayerWrapper current, VideoPlayerWrapper next)
    {
        current.EndVideo();
        // 종료 후 current의 alpha를 1으로
        current.SetAlpha(1);

        // next의 alpha를 1으로
        next.SetAlpha(1);
    }
}
