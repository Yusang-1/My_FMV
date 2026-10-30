using UnityEngine.Video;
using System;
using System.Collections.Generic;

public class SubtitleShower
{
    // UI에서 SubtitleData의 ID를 이용해 on/off 관리
    public event Action<SubtitleData> OnStartShowSubtitleRequested;
    public event Action<string> OnRemoveSubtitleRequested;

    private readonly Dictionary<VideoPlayer, List<SubtitleData>> subtitleDatasByPlayer = new();

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
    }

    public void StartShowSubtitle(VideoPlayer player, SubtitleData data)
    {
        if (!subtitleDatasByPlayer.ContainsKey(player))
        {
            subtitleDatasByPlayer.Add(player, new());
        }

        subtitleDatasByPlayer[player].Add(data);

        // UISubtitleContainer에 자막 켜기 요청
        OnStartShowSubtitleRequested?.Invoke(data);
    }

    private void RemoveSubtitle(SubtitleData data)
    {
        // UISubtitleContainer에 해당 ID의 자막 종료 요청
        OnRemoveSubtitleRequested?.Invoke(data.ID);
    }
}
