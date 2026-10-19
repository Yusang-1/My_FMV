using UnityEngine;
using UnityEngine.Video;
using System;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerWrapper : MonoBehaviour
{
    public event Action OnVideoPlayerUsed;
    public event Action<VideoPlayerWrapper> OnVideoPlayerUnused;

    public VideoPlayer VideoPlayer { get; private set; }

    private void Awake()
    {
        VideoPlayer = GetComponent<VideoPlayer>();
    }

    public void UseVideoPlayer()
    {
        OnVideoPlayerUsed?.Invoke();
    }

    public void UnuseVideoPlayer()
    {
        OnVideoPlayerUnused?.Invoke(this);
    }
}
