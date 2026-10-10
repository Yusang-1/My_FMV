using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class WebVideoPlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    [Header("[ AWS CloudFront Setting ]")]
    [Tooltip("CloudFront 영상 주소")] // https://[CloudFront 도메인 이름]/[S3에 올린 파일명]
    public string cloudFrontVideoUrl = "Https://d2vwjcp3kqcnox.cloudfront.net/Sarangbang_Sonnimgwa_Eomeoni(1961).mp4";
    public string cloudFrontVideoUrl2 = "Https://d2vwjcp3kqcnox.cloudfront.net/The_Gold_Rush(1925).mp4";

    [Header("[ UI Setting ]")]
    [Tooltip("영상이 버퍼링/로딩 중일 때 띄워줄 UI 오브젝트")]
    public GameObject loadingIndicatorUI;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = cloudFrontVideoUrl;

        // 3. 모바일/PC 하드웨어 가속 및 성능 최적화 옵션 켜기
        videoPlayer.skipOnDrop = true; // 프레임 드랍 발생 시 오디오 동기화를 위해 프레임 스킵

        // 4. 이벤트 리스너 등록 (준비 완료 및 에러 처리)
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.errorReceived += OnVideoError;

        // 5. 영상 로딩(버퍼링) UI 켜고 준비 시작
        if (loadingIndicatorUI != null) loadingIndicatorUI.SetActive(true);

        Debug.Log("CloudFront 스트리밍 준비 시작...");
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer source)
    {
        Debug.Log("스트리밍 준비 완료! 영화 재생을 시작합니다.");

        if (loadingIndicatorUI != null) loadingIndicatorUI.SetActive(false);

        source.Play();
    }

    // 네트워크 끊김, URL 오타, CORS 권한 문제 등으로 재생 실패 시 호출됨
    void OnVideoError(VideoPlayer source, string message)
    {
        Debug.LogError($"[Video Error] 영상 재생 중 오류가 발생했습니다: {message}");

        if (loadingIndicatorUI != null) loadingIndicatorUI.SetActive(false);

        // 팁: 유저에게 '네트워크 연결 상태를 확인해 주세요'라는 안내 UI를 띄우는 로직을 여기에 추가하면 좋습니다.
    }
}
