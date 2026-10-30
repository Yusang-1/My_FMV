using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoPlayController : MonoBehaviour
{
    private VideoPlayerPool pool;
    private VideoLoader loader;
    private JudgePreLoadVideo judgePreLoad;
    private VideoTransition transition;
    private BranchController branchController;
    private LoadedVideoController loadedVideoController;

    private VideoPlayer currentVideoPlayer;

    public void Initialize(VideoPlayerPool pool, VideoLoader loader, JudgePreLoadVideo judgePreLoad,
        VideoTransition transition, BranchController branchController, LoadedVideoController loadedVideoController)
    {
        this.pool = pool;
        this.loader = loader;
        this.judgePreLoad = judgePreLoad;
        this.transition = transition;
        this.branchController = branchController;
        this.loadedVideoController = loadedVideoController;
    }

    /// <summary>
    /// address를 load완료한 VideoPlayer가 loadedVideoController에 있다면 재생
    /// </summary>
    public void PlayLoadedVideo(string url)
    {
        StartCoroutine(WaitForLoadedPlayer(url));
    }

    /// <summary>
    /// address를 load완료한 VideoPlayer가 loadedVideoController에 있다면 재생
    /// </summary>
    private IEnumerator WaitForLoadedPlayer(string address)
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

    public void ReserveNextVideo()
    {
        // branchController에서 현재 조건에 맞는 분기의 NodeSO를 가져옴
        if (!branchController.TryGetNextNode(out NextNode nextNode, out NodeSO nextNodeData))
        {
            return;
        }

        // 현재 재생중인 player의 VideoPlayerWrapper를 가져옴
        if (!pool.TryGetWrapper(currentVideoPlayer, out VideoPlayerWrapper currentWrapper))
        {
            return;
        }

        // pool에서 사용 가능한 VideoPlayerWrapper를 가져옴
        if (!pool.TryGetValue(out VideoPlayerWrapper nextWrapper))
        {
            return;
        }

        string address = nextNodeData.VideoAdress;
        loader.LoadVideo(nextWrapper, address);

        // 현재 재생중인 player와 다음 player의 transition을 예약
        branchController.SetCurrentNode(nextNodeData);
        transition.ReserveTransition(currentWrapper, nextWrapper,
            nextNode.TransitionType, nextNode.TransitionData);
    }
    
    public float GetCurrentPlayerTime()
    {
        return (float)currentVideoPlayer.time;
    }
    
    public VideoPlayer GetCurrentPlayer()
    {
        return currentVideoPlayer;
    }
}
