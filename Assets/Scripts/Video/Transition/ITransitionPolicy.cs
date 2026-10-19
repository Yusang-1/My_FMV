using System.Collections;

public interface ITransitionPolicy
{
    public IEnumerator WaitForTransition(VideoPlayerWrapper current, VideoPlayerWrapper next);
}
