using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerWrapper : MonoBehaviour
{
    public event Action OnVideoPlayerUsed;
    public event Action<VideoPlayerWrapper> OnVideoPlayerUnused;

    public VideoPlayer VideoPlayer { get; private set; }
    [SerializeField] private RawImage rawImage;

    private void Awake()
    {
        VideoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ReleaseTexture();
    }

    public void UseVideoPlayer()
    {
        OnVideoPlayerUsed?.Invoke();
    }

    public void UnuseVideoPlayer()
    {
        OnVideoPlayerUnused?.Invoke(this);
    }

    public void PlayVideo()
    {
        Debug.Log("Play Video");
        gameObject.SetActive(true);
        VideoPlayer.Play();
    }

    public void EndVideo()
    {
        Debug.Log("Stop Video");
        VideoPlayer.Stop();
        gameObject.SetActive(false);
    }

    public void SetAlpha(float value)
    {
        Color color = rawImage.color;
        color.a = value;
        rawImage.color = color;
    }

    private void ReleaseTexture()
    {
        VideoPlayer.targetTexture.Release();
    }
}
