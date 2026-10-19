using UnityEngine.Video;

public interface ITransitionPolicy
{
    public void DoTransition(VideoPlayerWrapper prev, VideoPlayerWrapper next);
}
