using UnityEngine;
using System.Collections.Generic;

public class VideoPlayerPool : MonoBehaviour
{
    private readonly Stack<VideoPlayerWrapper> stack = new();

    private void Awake()
    {
        VideoPlayerWrapper[] wrappers =
            FindObjectsByType<VideoPlayerWrapper>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        if (wrappers == null || wrappers.Length == 0)
        {
            return;
        }
Debug.Log($"Find {wrappers.Length} Video Players");
        foreach (var wrapper in wrappers)
        {
            stack.Push(wrapper);
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

    private void ReturnToPool(VideoPlayerWrapper value)
    {
        stack.Push(value);

        value.OnVideoPlayerUnused -= ReturnToPool;
    }
}
