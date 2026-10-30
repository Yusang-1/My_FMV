using UnityEngine;

public class VideoController : MonoBehaviour
{
    [Header("[ Video ]")]
    [SerializeField] private VideoPlayerPool pool;
    [SerializeField] private VideoLoader loader;
    [SerializeField] private JudgePreLoadVideo judgePreLoad;
    [SerializeField] private VideoTransition transition;
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

    private void OnEnable()
    {
        BindEvents();
    }

    public void Initialize(BranchController branchController)
    {
        this.branchController = branchController;

        judgePreLoad.Initialize(loadVideoRunTimeContext);
        transition.Initialize(videoPlayController);
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
    }

    private void UnbindEvents()
    {
        loader.OnVideoPreparedEvent -= loadedVideoController.AddLoadedVideo;

        judgePreLoad.OnPreLoadRequired -= videoPlayController.ReserveNextVideo;
    }

    public void OnPlay()
    {
        string address = default;

        if (pool.TryGetValue(out VideoPlayerWrapper wrapper))
        {
            NodeSO node = branchController.GetStartNode();
            if (node == null) return;

            address = node.VideoAdress;
            loader.LoadVideo(wrapper, address);
        }

        videoPlayController.PlayLoadedVideo(address);
    }

    public float GetCurrentPlayerTime()
    {
        return videoPlayController.GetCurrentPlayerTime();
    }
}
