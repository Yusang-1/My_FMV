using UnityEngine;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayerPool pool;
    [SerializeField] private VideoLoader loader;
    [SerializeField] private JudgePreLoadVideo judgePreLoad;
    [SerializeField] private LoadVideoRunTimeContext loadVideoRunTimeContext;
    private readonly VideoPlayController controller = new();
    private readonly LoadedVideoController loadedVideoController = new();

    [Header("[ AWS CloudFront Setting ]")]
    [Tooltip("CloudFront 영상 주소")] // https://[CloudFront 도메인 이름]/[S3에 올린 파일명]
    public string cloudFrontVideoUrl = "Https://d2vwjcp3kqcnox.cloudfront.net/Sarangbang_Sonnimgwa_Eomeoni(1961).mp4";
    public string cloudFrontVideoUrl2 = "Https://d2vwjcp3kqcnox.cloudfront.net/The_Gold_Rush(1925).mp4";

    private void OnEnable()
    {
        BindEvents();
    }

    private void Start()
    {
        judgePreLoad.Initialize(loadVideoRunTimeContext);

        if (pool.TryGetValue(out VideoPlayerWrapper wrapper))
        {
            Debug.Log("Success Get VideoPlayer");
            loader.LoadVideo(wrapper.VideoPlayer, cloudFrontVideoUrl);
        }
    }

    private void OnDisable()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        loader.OnVideoPreparedEvent += loadedVideoController.AddLoadedVideo;
        // loader.OnErrorOccurred += ;
    }

    private void UnbindEvents()
    {
        loader.OnVideoPreparedEvent -= loadedVideoController.AddLoadedVideo;
        // loader.OnErrorOccurred -= ;
    }
}
