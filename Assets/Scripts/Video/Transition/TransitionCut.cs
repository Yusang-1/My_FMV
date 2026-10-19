public class TransitionCut : ITransitionPolicy
{
    public TransitionPolicyEnum PolicyEnum { get; private set; } = TransitionPolicyEnum.Cut;
    
    public void DoTransition(VideoPlayerWrapper prev, VideoPlayerWrapper next)
    {
        prev.EndVideo();
        next.PlayVideo();
    }
}
