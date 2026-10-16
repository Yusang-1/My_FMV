using UnityEngine.Video;

public interface ITransitionPolicy
{
    public void DoTransition(VideoPlayer prev, VideoPlayer next);
}
