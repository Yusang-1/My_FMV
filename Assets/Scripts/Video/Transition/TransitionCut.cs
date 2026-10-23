using System.Collections;

public class TransitionCut : ITransitionPolicy
{
    private readonly VideoPlayController videoPlayController;
    
    public TransitionPolicyEnum PolicyEnum { get; private set; } = TransitionPolicyEnum.Cut;
    
    public TransitionCut(VideoPlayController videoPlayController)
    {
        this.videoPlayController = videoPlayController;
    }

    public IEnumerator WaitForTransition(TransitionData transitionData)
    {
        VideoPlayerWrapper current = transitionData.Current;
        VideoPlayerWrapper next = transitionData.Next;

        while (true)
        {
            if (!current.VideoPlayer.isPlaying)
            {
                DoTransition(current, next);
                break;
            }

            yield return null;
        }
    }

    private void DoTransition(VideoPlayerWrapper current, VideoPlayerWrapper next)
    {
        current.EndVideo();
        videoPlayController.PlayLoadedVideo(next.VideoPlayer.url);
    }
}
