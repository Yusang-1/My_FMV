using UnityEngine;
using UnityEngine.Video;

public class VideoTransition : MonoBehaviour
{
    public void Transition(VideoPlayer prev, VideoPlayer next, ITransitionPolicy transitionPolicy)
    {
        transitionPolicy.DoTransition(prev, next);
    }
}
