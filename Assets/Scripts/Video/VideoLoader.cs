using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoLoader : MonoBehaviour
{
    public event Action<VideoPlayer, int> OnVideoPreparedEvent;
    public event Action OnErrorOccurred;

    [Header("[ UI Setting ]")]
    [Tooltip("영상이 버퍼링/로딩 중일 때 띄워줄 UI 오브젝트")]
    [SerializeField] private GameObject loadingIndicatorUI;

    public void LoadVideo(VideoPlayerWrapper wrapper, NodeSO nodeData)
    {
        wrapper.gameObject.SetActive(true);

        wrapper.LoadVideo(nodeData);
        
        BindEvents(wrapper.VideoPlayer);

        // 영상 로딩(버퍼링) UI 켜고 준비 시작
        if (loadingIndicatorUI != null) loadingIndicatorUI.SetActive(true);        
    }

    private void OnVideoPrepared(VideoPlayer source)
    {
        if (loadingIndicatorUI != null) loadingIndicatorUI.SetActive(false);

        int code = source.url.GetHashCode();
        OnVideoPreparedEvent?.Invoke(source, code);
        Debug.Log($"스트리밍 준비 완료!\n{source.url}");

        UnbindEvents(source);
    }

    // 네트워크 끊김, URL 오타, CORS 권한 문제 등으로 재생 실패 시 호출됨
    private void OnVideoError(VideoPlayer source, string message)
    {
        Debug.LogError($"[Video Error] 영상 재생 중 오류가 발생했습니다: {message}");

        if (loadingIndicatorUI != null) loadingIndicatorUI.SetActive(false);

        OnErrorOccurred?.Invoke();

        UnbindEvents(source);
    }

    private void BindEvents(VideoPlayer videoPlayer)
    {
        // 이벤트 리스너 등록 (준비 완료 및 에러 처리)
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.errorReceived += OnVideoError;
    }

    private void UnbindEvents(VideoPlayer videoPlayer)
    {
        videoPlayer.prepareCompleted -= OnVideoPrepared;
        videoPlayer.errorReceived -= OnVideoError;
    }
}
