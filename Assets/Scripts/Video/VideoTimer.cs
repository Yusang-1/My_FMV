using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

public class VideoTimer : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private readonly HashSet<ITimeCheck> timeCheckList = new(10);
    private readonly List<ITimeCheck> removeList = new(5);

    // 시간 체크해야되는 것들 처리
    // startTime, endTime

    private void Update()
    {
        if (videoPlayer == null)
        {
            return;
        }

        float currentTime = (float)videoPlayer.time;

        foreach (var timeCheckObject in timeCheckList)
        {
            if (currentTime >= timeCheckObject.GetEndTime())
            {
                // endTime에 도달했을 때의 처리
                timeCheckObject.EndTimeArrived();

                // 제거할 리스트를 hash에 저장
                removeList.Add(timeCheckObject);
            }
        }

        // timeCheckList에서 endTime에 도달한 것들 제거
        foreach (var remove in removeList)
        {
            if (timeCheckList.Contains(remove))
            {
                timeCheckList.Remove(remove);
            }
        }
        removeList.Clear();
    }

    /// <summary>
    /// videoPlayer 변경을 timer에 반영
    /// </summary>
    public void SetVideoPlayer(VideoPlayer videoPlayer)
    {
        this.videoPlayer = videoPlayer;
    }

    /// <summary>
    /// 시간을 확인할 객체를 추가
    /// </summary>
    public void AddTimeCheckObject(ITimeCheck timeCheckObject)
    {
        if (timeCheckList.Contains(timeCheckObject))
        {
            return;
        }
        else
        {
            timeCheckList.Add(timeCheckObject);
        }
    }
}

public interface ITimeCheck
{
    public float GetEndTime();
    public void EndTimeArrived();
}
