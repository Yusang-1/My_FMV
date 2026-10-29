using UnityEngine;
using System;

public class SubtitleController : MonoBehaviour
{
    public event Action<char[], float> OnShowSubtitleRequested;

    public void ShowSubtitle(char[] text, float duration)
    {
        OnShowSubtitleRequested?.Invoke(text, duration);
    }
}
