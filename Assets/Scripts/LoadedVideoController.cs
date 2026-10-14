using UnityEngine.Video;
using System.Collections.Generic;

public class LoadedVideoController
{
    private readonly Dictionary<int, VideoPlayer> loadedPlayers = new();

    public void AddLoadedVideo(VideoPlayer videoPlayer, int code)
    {
        if (loadedPlayers == null) return;

        if (loadedPlayers.ContainsKey(code)) return;

        loadedPlayers.Add(code, videoPlayer);
    }


    /// <summary>
    /// url별로 구분할 수 있는 코드를 이용해 player반환
    /// </summary>
    public bool TryGetLoadedPlayer(int code, out VideoPlayer videoPlayer)
    {
        if (loadedPlayers == null || !loadedPlayers.ContainsKey(code))
        {
            videoPlayer = null;
            return false;
        }

        videoPlayer = loadedPlayers[code];
        return true;
    }
}
