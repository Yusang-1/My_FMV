using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerWrapper : MonoBehaviour
{
    public event Action OnVideoPlayerUsed;
    public event Action<VideoPlayerWrapper> OnVideoPlayerUnused;
    public event Action<NodeSO> OnNodePlayed;

    public VideoPlayer VideoPlayer { get; private set; }
    private NodeSO nodeData;
    [SerializeField] private RawImage rawImage;

    private void Awake()
    {
        VideoPlayer = GetComponent<VideoPlayer>();
        VideoPlayer.source = VideoSource.Url;

        // 모바일/PC 하드웨어 가속 및 성능 최적화 옵션 켜기
        VideoPlayer.skipOnDrop = true; // 프레임 드랍 발생 시 오디오 동기화를 위해 프레임 스킵
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

    public string GetLoadedNodeCode()
    {
        if (nodeData == null)
        {
            return null;
        }

        return nodeData.Code;
    }

    public void LoadVideo(NodeSO nodeData)
    {
        this.nodeData = nodeData;

        VideoPlayer.url = nodeData.VideoAdress;

        Debug.Log("CloudFront 스트리밍 준비 시작...");
        VideoPlayer.Prepare();
    }

    public void ActiveVideo()
    {
        gameObject.SetActive(true);
    }

    public void PlayVideo()
    {
        Debug.Log("Play Video");
        gameObject.SetActive(true);
        VideoPlayer.Play();

        if (nodeData != null)
        {
            if (nodeData.VideoType == VideoType.Loop)
            {
                SetLoop(true);
            }
            else
            {
                SetLoop(false);
            }
        }
        
        OnNodePlayed?.Invoke(nodeData);
    }

    public void EndVideo()
    {
        Debug.Log("Stop Video");
        VideoPlayer.Stop();
        gameObject.SetActive(false);
        UnuseVideoPlayer();
    }

    public void SetColor(float value)
    {
        Color color = rawImage.color;
        color.r = value;
        color.g = value;
        color.b = value;
        rawImage.color = color;
    }

    public void SetColor(float r, float g, float b, float a = 1)
    {
        Color color = rawImage.color;
        color.r = r;
        color.g = g;
        color.b = b;
        color.a = a;
        rawImage.color = color;
    }

    public void SetAlpha(float value)
    {
        Color color = rawImage.color;
        color.a = value;
        rawImage.color = color;
    }

    private void SetLoop(bool value)
    {
        VideoPlayer.isLooping = value;
    }

    private void ReleaseTexture()
    {
        VideoPlayer.targetTexture.Release();
    }
}
