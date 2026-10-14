using UnityEngine;
using UnityEngine.Video;

public class VideoPlayController : MonoBehaviour
{
    public void Play(VideoPlayer videoPlayer)
    {
        videoPlayer.Play();
    }

    public void Stop(VideoPlayer videoPlayer)
    {
        videoPlayer.Stop();
    }
}
