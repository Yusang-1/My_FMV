using UnityEngine;
using System;

public class UIContainerSubtitleTotal : MonoBehaviour
{
    public Action<SubtitleData> OnShowSubtitleRequested;
    public Action<string> OnRemoveSubtitleRequested;

    [SerializeField] private UIContainerSubtitle uIContainerSubtitle;
    [SerializeField] private UIContainerSubtitleChoice uIContainerSubtitleChoice;
    [SerializeField] private UIContainerTimer uIContainerTimer;

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
        OnShowSubtitleRequested += uIContainerSubtitle.ShowSubtitle;
        OnRemoveSubtitleRequested += uIContainerSubtitle.RemoveSubtitle;
    }

    private void UnbindEvents()
    {
        OnShowSubtitleRequested -= uIContainerSubtitle.ShowSubtitle;
        OnRemoveSubtitleRequested -= uIContainerSubtitle.RemoveSubtitle;
    }
}
