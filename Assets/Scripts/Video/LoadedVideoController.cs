using UnityEngine.Video;
using System.Collections.Generic;

public class LoadedVideoController
{
    private readonly Dictionary<string, VideoPlayer> loadedPlayers = new();

    public void AddLoadedVideo(VideoPlayer videoPlayer, string code)
    {
        if (loadedPlayers == null) return;

        if (loadedPlayers.ContainsKey(code)) return;

        loadedPlayers.Add(code, videoPlayer);
    }

    /// <summary>
    /// code를 이용해 해당 code의 NodeSO가 준비된 VideoPlayer반환
    /// </summary>
    public bool TryGetLoadedPlayer(string code, out VideoPlayer videoPlayerWrapper)
    {
        if (loadedPlayers == null || !loadedPlayers.ContainsKey(code))
        {
            videoPlayerWrapper = null;
            return false;
        }

        videoPlayerWrapper = loadedPlayers[code];
        return true;
    }
}
