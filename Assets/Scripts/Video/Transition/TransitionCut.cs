using System.Collections;

public class TransitionCut : ITransitionPolicy
{
    public TransitionPolicyEnum PolicyEnum { get; private set; } = TransitionPolicyEnum.Cut;

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
        next.PlayVideo();
    }
}
