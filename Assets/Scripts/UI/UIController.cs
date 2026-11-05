using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    public event Action OnTestStartRequested;
    public Action<SubtitleData> OnShowSubtitleRequested;
    public Action<string> OnRemoveSubtitleRequested;

    [SerializeField] private UITestStart uITestStart;
    [SerializeField] private UIContainerSubtitleTotal uIContainerSubtitleTotal;

    private void OnEnable()
    {
        BindEvents();
    }

    private void OnDisable()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        uITestStart.OnTestStartRequested += HandleTestStart;

        OnShowSubtitleRequested += HandleShowSubtitle;
        OnRemoveSubtitleRequested += HandleRemoveSubtitle;
    }

    private void UnbindEvents()
    {
        uITestStart.OnTestStartRequested -= HandleTestStart;

        OnShowSubtitleRequested -= HandleShowSubtitle;
        OnRemoveSubtitleRequested -= HandleRemoveSubtitle;
    }

    private void HandleTestStart()
    {
        OnTestStartRequested?.Invoke();
    }

    private void HandleShowSubtitle(SubtitleData data)
    {
        uIContainerSubtitleTotal.OnShowSubtitleRequested?.Invoke(data);
    }

    private void HandleRemoveSubtitle(string id)
    {
        uIContainerSubtitleTotal.OnRemoveSubtitleRequested?.Invoke(id);
    }
}
