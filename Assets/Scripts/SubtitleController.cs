using UnityEngine;
using System;

public class SubtitleController : MonoBehaviour
{
    public event Action<string, float> OnShowSubtitleRequired;

    public void ShowSubtitle(string text, float duration)
    {
        OnShowSubtitleRequired?.Invoke(text, duration);
    }
}
