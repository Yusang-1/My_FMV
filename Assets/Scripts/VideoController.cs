using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoController : MonoBehaviour
{
    [Header("[ Video ]")]
    [SerializeField] private VideoPlayerPool pool;
    [SerializeField] private VideoLoader loader;
    [SerializeField] private JudgePreLoadVideo judgePreLoad;
    [SerializeField] private LoadVideoRunTimeContext loadVideoRunTimeContext;
    [SerializeField] private VideoTransition transition;
    [SerializeField] private BranchController branchController;

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
            currentNode = branchController.GetStartNode();
            if (currentNode == null) return;

            address = currentNode.VideoAdress;
            loader.LoadVideo(wrapper, address);
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
                    wrapper.PlayVideo();
                }
                judgePreLoad.StartNewJudge(videoPlayer);
                break;
            }

            yield return null;
        }
    }

    public void TestNextVideo()
    {
        if (branchController.TryGetNextNode(out NodeSO nextNode))
        {
            // preload
            if (pool.TryGetValue(out VideoPlayerWrapper nextWrapper))
            {
                string address = nextNode.VideoAdress;
                loader.LoadVideo(nextWrapper, address);
            }

            if (!pool.TryGetWrapper(currentVideoPlayer, out VideoPlayerWrapper currentWrapper))
            {
                return;
            }

            transition.ReserveTransition(currentWrapper, nextWrapper, currentNode.NextNodes[0].TransitionType, currentNode.NextNodes[0].TransitionData);
        }
    }
}
