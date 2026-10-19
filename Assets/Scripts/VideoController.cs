using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [Header("[ Video ]")]
    [SerializeField] private VideoPlayerPool pool;
    [SerializeField] private VideoLoader loader;
    [SerializeField] private JudgePreLoadVideo judgePreLoad;
    [SerializeField] private LoadVideoRunTimeContext loadVideoRunTimeContext;
    private readonly VideoPlayController controller = new();
    private readonly LoadedVideoController loadedVideoController = new();

    [Header("[ Node ]")]
    [SerializeField] private NodeContainer nodeContainer;

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

        judgePreLoad.OnPreLoadRequired += TestNextVideo;
    }

    private void UnbindEvents()
    {
        loader.OnVideoPreparedEvent -= loadedVideoController.AddLoadedVideo;
        // loader.OnErrorOccurred -= ;

        judgePreLoad.OnPreLoadRequired -= TestNextVideo;
    }

    private NodeSO currentNode;
    private VideoPlayer currentVideoPlayer;
    public void TestStart()
    {
        string address = default;

        if (pool.TryGetValue(out VideoPlayerWrapper wrapper))
        {
            if (nodeContainer.TryGetNodeData(0, out NodeSO node))
            {
                
                currentNode = node;
                address = node.VideoAdress;
                loader.LoadVideo(wrapper, address);
            }
        }

        StartCoroutine(Test(address));
    }

    private IEnumerator Test(string address)
    {
        bool isLoaded = false;

        while (!isLoaded)
        {
            isLoaded = loadedVideoController.TryGetLoadedPlayer(address.GetHashCode(), out VideoPlayer videoPlayer);
            if (isLoaded)
            {
                currentVideoPlayer = videoPlayer;
                if (pool.TryGetWrapper(videoPlayer, out VideoPlayerWrapper wrapper))
                {
                    controller.Play(wrapper);
                }
                judgePreLoad.StartNewJudge(videoPlayer);
                break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// 현재 videoPlayer가 끝날때
    /// </summary>
    public void TestNextVideo()
    {
        string address = default;

        if (nodeContainer.TryGetNextNodeData(currentNode.Code, out NodeSO nextNode))
        {
            // preload
            if (pool.TryGetValue(out VideoPlayerWrapper wrapper))
            {
                address = nextNode.VideoAdress;
                loader.LoadVideo(wrapper, address);
            }

            StartCoroutine(WaitForCurrentPlayerFinished(address));
        }
    }

    private IEnumerator WaitForCurrentPlayerFinished(string address)
    {
        bool isLoaded = false;
        VideoPlayer newVideoPlayer = null;

        while (!isLoaded)
        {
            isLoaded = loadedVideoController.TryGetLoadedPlayer(address.GetHashCode(), out newVideoPlayer);
            if (isLoaded)
            {
                break;
            }

            yield return null;
        }

        while (true)
        {
            if (currentVideoPlayer.length - currentVideoPlayer.time <= 0.01f)
            {
                if(pool.TryGetWrapper(currentVideoPlayer, out VideoPlayerWrapper currentWrapper))
                {
                    controller.Stop(currentWrapper);                    
                }
                
                currentVideoPlayer = newVideoPlayer;
                if (pool.TryGetWrapper(currentVideoPlayer, out VideoPlayerWrapper wrapper))
                {
                    controller.Play(wrapper);
                }
                judgePreLoad.StartNewJudge(currentVideoPlayer);
                break;
            }
            yield return null;
        }
    }
}
