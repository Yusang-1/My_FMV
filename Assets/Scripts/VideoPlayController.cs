using UnityEngine.Video;

public class VideoPlayController
{
    public void Play(VideoPlayer videoPlayer)
    {
        videoPlayer.Play();
    }
    public void Play(VideoPlayerWrapper wrapper)
    {
        wrapper.gameObject.SetActive(true);
        wrapper.VideoPlayer.Play();
    }

    public void Stop(VideoPlayer videoPlayer)
    {
        videoPlayer.Stop();
    }
    public void Stop(VideoPlayerWrapper wrapper)
    {
        wrapper.gameObject.SetActive(false);
        wrapper.VideoPlayer.Stop();
    }
}
