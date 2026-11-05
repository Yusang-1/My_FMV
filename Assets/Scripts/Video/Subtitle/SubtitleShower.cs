using UnityEngine.Video;
using System;
using System.Collections.Generic;

public class SubtitleShower
{
    // UI에서 SubtitleData의 ID를 이용해 on/off 관리
    public event Action<SubtitleData> OnStartShowSubtitleRequested;
    public event Action<string> OnRemoveSubtitleRequested;

    private readonly Dictionary<VideoPlayer, List<SubtitleData>> subtitleDatasByPlayer = new(videoPlayerOnceLimit);
    private readonly Dictionary<SubtitleData, VideoPlayer> playerBySubtitleData = new(subtitleOnceLimit);
    private readonly HashSet<SubtitleData> removeReservedData = new(subtitleOnceLimit);

    private const int subtitleOnceLimit = 3;
    private const int videoPlayerOnceLimit = 2;

    public void Update()
    {
        CheckSubtitleEnd();
    }

    private void CheckSubtitleEnd()
    {
        foreach (var pair in subtitleDatasByPlayer)
        {
            float time = (float)pair.Key.time;
            List<SubtitleData> subtitleList = pair.Value;

            foreach (var data in subtitleList)
            {
                if (time >= data.EndTime)
                {
                    RemoveSubtitle(data);
                }
            }
        }
        RemoveCollection();
    }

    public void StartShowSubtitle(VideoPlayer player, SubtitleData data)
    {
        if (!subtitleDatasByPlayer.ContainsKey(player))
        {
            subtitleDatasByPlayer.Add(player, new(subtitleOnceLimit));
        }

        subtitleDatasByPlayer[player].Add(data);
        playerBySubtitleData.Add(data, player);

        // UIContainerSubtitleTotal에 자막 켜기 요청
        OnStartShowSubtitleRequested?.Invoke(data);
    }

    private void RemoveSubtitle(SubtitleData data)
    {
        if (playerBySubtitleData.ContainsKey(data))
        {
            ReserveRemoveCollection(data);
        }

        // UISubtitleContainer에 해당 ID의 자막 종료 요청
        OnRemoveSubtitleRequested?.Invoke(data.ID);
    }

    private void ReserveRemoveCollection(SubtitleData data)
    {
        removeReservedData.Add(data);
    }

    private void RemoveCollection()
    {
        foreach (var data in removeReservedData)
        {
            var player = playerBySubtitleData[data];

            subtitleDatasByPlayer[player].Remove(data);
            playerBySubtitleData.Remove(data);
        }

        removeReservedData.Clear();
    }
}
