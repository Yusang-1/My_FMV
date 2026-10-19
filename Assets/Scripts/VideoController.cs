using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayerPool pool;
    [SerializeField] private VideoLoader loader;
    [SerializeField] private JudgePreLoadVideo judgePreLoad;
    [SerializeField] private LoadVideoRunTimeContext loadVideoRunTimeContext;
    private readonly VideoPlayController controller = new();
    private readonly LoadedVideoController loadedVideoController = new();

    [Header("[ AWS CloudFront Setting ]")] // https://[CloudFront 도메인 이름]/[S3에 올린 파일명]    
    public string cloudFrontVideoUrl = "Https://d2vwjcp3kqcnox.cloudfront.net/Sarangbang_Sonnimgwa_Eomeoni(1961).mp4";
    public string cloudFrontVideoUrl2 = "Https://d2vwjcp3kqcnox.cloudfront.net/The_Gold_Rush(1925).mp4";
    public string cloudFrontShortVideoUrl1 = "Https://d2vwjcp3kqcnox.cloudfront.net/Nat-Landscape-1.mp4";
    public string cloudFrontShortVideoUrl2 = "Https://d2vwjcp3kqcnox.cloudfront.net/Nat-Landscape-2.mp4";
    public string cloudFrontShortVideoUrl3 = "Https://d2vwjcp3kqcnox.cloudfront.net/Nat-Volcano-1.mp4";

    private void OnEnable()
    {
        BindEvents();
    }

    private void Start()
    {
        judgePreLoad.Initialize(loadVideoRunTimeContext);
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

    public void TestStart()
    {
        if (pool.TryGetValue(out VideoPlayerWrapper wrapper))
        {
            loader.LoadVideo(wrapper.VideoPlayer, cloudFrontShortVideoUrl1);
        }

        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        bool isLoaded = false;

        while (!isLoaded)
        {
            isLoaded = loadedVideoController.TryGetLoadedPlayer(cloudFrontShortVideoUrl1.GetHashCode(), out VideoPlayer videoPlayer);
            if (isLoaded)
            {
                controller.Play(videoPlayer);                
            }

            yield return false;
        }
    }
}
