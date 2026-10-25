using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Video;

public class VideoPlayerPool : MonoBehaviour
{
    private readonly Stack<VideoPlayerWrapper> stack = new();
    private readonly Dictionary<VideoPlayer, VideoPlayerWrapper> wrapperDict = new();

    private void Awake()
    {
        VideoPlayerWrapper[] wrappers =
            FindObjectsByType<VideoPlayerWrapper>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        if (wrappers == null || wrappers.Length == 0)
        {
            return;
        }

        foreach (var wrapper in wrappers)
        {
            stack.Push(wrapper);
        }
    }

    private void Start()
    {
        foreach (var wrapper in stack)
        {
            if (wrapperDict.ContainsKey(wrapper.VideoPlayer)) continue;

            wrapperDict.Add(wrapper.VideoPlayer, wrapper);
        }
    }

    public bool TryGetValue(out VideoPlayerWrapper videoPlayerWrapper)
    {
        if (stack == null || stack.Count == 0)
        {
            videoPlayerWrapper = null;
            return false;
        }

        videoPlayerWrapper = stack.Pop();
        videoPlayerWrapper.OnVideoPlayerUnused += ReturnToPool;
        videoPlayerWrapper.UseVideoPlayer();

        return true;
    }

    public bool TryGetWrapper(VideoPlayer videoPlayer, out VideoPlayerWrapper wrapper)
    {
        if (!wrapperDict.ContainsKey(videoPlayer))
        {
            wrapper = null;
            return false;
        }

        wrapper = wrapperDict[videoPlayer];
        return true;
    }

    private void ReturnToPool(VideoPlayerWrapper value)
    {
        stack.Push(value);

        value.OnVideoPlayerUnused -= ReturnToPool;
    }
}
