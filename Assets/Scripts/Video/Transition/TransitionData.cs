using UnityEngine;
using System;

[Serializable]
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

[Serializable]
public class TransitionCutData : TransitionData
{
    public override void SetData(VideoPlayerWrapper current, VideoPlayerWrapper next)
    {
        base.SetData(current, next);
    }
}

[Serializable]
public class TransitionDissolveData : TransitionData
{
    [SerializeField] private float time = 3f;

    public float Time => time;

    public override void SetData(VideoPlayerWrapper current, VideoPlayerWrapper next)
    {
        base.SetData(current, next);
    }
}

[Serializable]
public class TransitionFadeData : TransitionData
{
    [SerializeField] private float time = 3f;

    public float Time => time;

    public override void SetData(VideoPlayerWrapper current, VideoPlayerWrapper next)
    {
        base.SetData(current, next);
    }
}
