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

    private void Start()
    {
        VideoPlayer.loopPointReached += OnVideoEnd;
        
        gameObject.SetActive(false);
    }

    public void UseVideoPlayer()
    {
        OnVideoPlayerUsed?.Invoke();
    }

    public void UnuseVideoPlayer()
    {
        OnVideoPlayerUnused?.Invoke(this);
    }
    
    void OnVideoEnd(VideoPlayer videoPlayer)
    {
        videoPlayer.Stop();
        gameObject.SetActive(false);
    }
}
