using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoController : MonoBehaviour
{
    public event Action<VideoPlayer> OnVideoPlayerChanged;
    
    [Header("[ Video ]")]
    [SerializeField] private VideoPlayerPool pool;
    [SerializeField] private VideoLoader loader;
    [SerializeField] private JudgePreLoadVideo judgePreLoad;
    [SerializeField] private VideoPlayController videoPlayController;
    [SerializeField] private LoadVideoRunTimeContext loadVideoRunTimeContext;
    private BranchController branchController;

    private readonly LoadedVideoController loadedVideoController = new();

    [Header("[ AWS CloudFront Setting ]")] // https://[CloudFront 도메인 이름]/[S3에 올린 파일명]    
    public string cloudFrontVideoUrl = "Https://d2vwjcp3kqcnox.cloudfront.net/Sarangbang_Sonnimgwa_Eomeoni(1961).mp4";
    public string cloudFrontVideoUrl2 = "Https://d2vwjcp3kqcnox.cloudfront.net/The_Gold_Rush(1925).mp4";
    public string cloudFrontShortVideoUrl1 = "Https://d2vwjcp3kqcnox.cloudfront.net/Nat-Landscape-1.mp4";
    public string cloudFrontShortVideoUrl2 = "Https://d2vwjcp3kqcnox.cloudfront.net/Nat-Landscape-2.mp4";
    public string cloudFrontShortVideoUrl3 = "Https://d2vwjcp3kqcnox.cloudfront.net/Nat-Volcano-1.mp4";

    public void AwakeInitialize(BranchController branchController)
    {
        this.branchController = branchController;
    }
    
    private void OnEnable()
    {
        BindEvents();
    }

    public void Initialize(VideoTransition transition)
    {
        transition.Initialize(videoPlayController);
        judgePreLoad.Initialize(loadVideoRunTimeContext);
        videoPlayController.Initialize(pool, loader, judgePreLoad, transition, branchController, loadedVideoController);
    }

    private void OnDisable()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        loader.OnVideoPreparedEvent += loadedVideoController.AddLoadedVideo;

        judgePreLoad.OnPreLoadRequired += videoPlayController.ReserveNextVideo;
        
        videoPlayController.OnVideoPlayerChanged += HandleVideoPlayerChanged;

        pool.BindEvents(branchController);
    }

    private void UnbindEvents()
    {
        loader.OnVideoPreparedEvent -= loadedVideoController.AddLoadedVideo;

        judgePreLoad.OnPreLoadRequired -= videoPlayController.ReserveNextVideo;
        
        videoPlayController.OnVideoPlayerChanged -= HandleVideoPlayerChanged;

        pool.UnbindEvents(branchController);
    }

    public void OnPlay()
    {
        NodeSO node = default;

        if (pool.TryGetValue(out VideoPlayerWrapper wrapper))
        {
            node = branchController.GetStartNode();
            if (node == null) return;

            loader.LoadVideo(wrapper, node);
        }

        videoPlayController.PlayLoadedVideo(node.Code);        
    }

    public float GetCurrentPlayerTime()
    {
        return videoPlayController.GetCurrentPlayerTime();
    }

    public VideoPlayer GetCurrentPlayer()
    {
        return videoPlayController.GetCurrentPlayer();
    }
    
    private void HandleVideoPlayerChanged(VideoPlayer player)
    {
        OnVideoPlayerChanged?.Invoke(player);
    }
}
