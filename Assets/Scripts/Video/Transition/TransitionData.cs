using UnityEngine;

public class TransitionData
{
    public VideoPlayerWrapper Current { get; private set; }
    public VideoPlayerWrapper Next { get; private set; }

    public virtual void SetData(VideoPlayerWrapper current, VideoPlayerWrapper next)
    {
        Current = current;
        Next = next;
    }
}

public class TransitionCutData : TransitionData
{
    public TransitionCutData(VideoPlayerWrapper current, VideoPlayerWrapper next)
    {
        SetData(current, next);
    }

    public override void SetData(VideoPlayerWrapper current, VideoPlayerWrapper next)
    {
        base.SetData(current, next);
    }
}

public class TransitionDissolveData : TransitionData
{
    public float Time { get; private set; }

    public TransitionDissolveData(VideoPlayerWrapper current, VideoPlayerWrapper next, float time)
    {
        SetData(current, next, time);
    }

    public void SetData(VideoPlayerWrapper current, VideoPlayerWrapper next, float time)
    {
        base.SetData(current, next);
        Time = time;
    }
}

public class TransitionFadeData : TransitionData
{
    public float Time { get; private set; }

    public TransitionFadeData(VideoPlayerWrapper current, VideoPlayerWrapper next, float time)
    {
        SetData(current, next, time);
    }

    public void SetData(VideoPlayerWrapper current, VideoPlayerWrapper next, float time)
    {
        base.SetData(current, next);
        Time = time;
    }
}
