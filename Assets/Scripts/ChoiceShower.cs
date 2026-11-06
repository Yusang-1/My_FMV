using UnityEngine.Video;
using System;
using System.Collections.Generic;

public class ChoiceShower
{
    // UI에서 ChoiceData의 ID를 이용해 on/off 관리
    public event Action<ChoiceData> OnStartShowChoiceRequested;
    public event Action<string> OnRemoveChoiceRequested;
    
    private readonly Dictionary<VideoPlayer, List<ChoiceData>> choiceDatasByPlayer = new(videoPlayerOnceLimit);
    private readonly Dictionary<ChoiceData, VideoPlayer> playerByChoiceData = new(choiceOnceLimit);
    private readonly HashSet<ChoiceData> removeReservedData = new(choiceOnceLimit);

    private const int choiceOnceLimit = 3;
    private const int videoPlayerOnceLimit = 2;
    
    public void Update()
    {
        CheckChoiceEnd();
    }
    
    private void CheckChoiceEnd()
    {
        foreach (var pair in choiceDatasByPlayer)
        {
            float time = (float)pair.Key.time;
            List<ChoiceData> choiceList = pair.Value;

            foreach (var data in choiceList)
            {
                if (time >= data.EndTime)
                {
                    RemoveChoice(data);
                }
            }
        }
        RemoveCollection();
    }
    
    public void StartShowChoice(VideoPlayer player, ChoiceData data)
    {
        if (!choiceDatasByPlayer.ContainsKey(player))
        {
            choiceDatasByPlayer.Add(player, new(choiceOnceLimit));
        }

        choiceDatasByPlayer[player].Add(data);
        playerByChoiceData.Add(data, player);

        // UIContainerSubtitleTotal에 자막 켜기 요청
        OnStartShowChoiceRequested?.Invoke(data);
    }
    
    private void RemoveChoice(ChoiceData data)
    {
        if (playerByChoiceData.ContainsKey(data))
        {
            ReserveRemoveCollection(data);
        }

        // UIChoiceContainer에 해당 ID의 자막 종료 요청
        OnRemoveChoiceRequested?.Invoke(data.ID);
    }
    
    private void ReserveRemoveCollection(ChoiceData data)
    {
        removeReservedData.Add(data);
    }

    private void RemoveCollection()
    {
        foreach (var data in removeReservedData)
        {
            var player = playerByChoiceData[data];

            choiceDatasByPlayer[player].Remove(data);
            playerByChoiceData.Remove(data);
        }

        removeReservedData.Clear();
    }
}
