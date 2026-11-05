using UnityEngine;
using System;
using System.Collections.Generic;

public class UIContainerSubtitleTotal : MonoBehaviour
{
    public Action<SubtitleData> OnShowSubtitleRequested;
    public Action<string> OnRemoveSubtitleRequested;

    [SerializeField] private UIContainerSubtitle uIContainerSubtitle;
    [SerializeField] private UIContainerSubtitleChoice uIContainerSubtitleChoice;
    [SerializeField] private UIContainerTimer uIContainerTimer;

    protected readonly Dictionary<string, UIContainerSubtitle> subtitleByID = new();

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
        OnShowSubtitleRequested += ShowSubtitle;
        OnRemoveSubtitleRequested += RemoveSubtitle;
    }

    private void UnbindEvents()
    {
        OnShowSubtitleRequested -= ShowSubtitle;
        OnRemoveSubtitleRequested -= RemoveSubtitle;
    }

    private void ShowSubtitle(SubtitleData data)
    {
        UIContainerSubtitle subtitleUI;

        if (data.Type == SubtitleType.Default)
        {
            subtitleUI = uIContainerSubtitle;
            subtitleUI.ShowSubtitle(data);
        }
        else
        {
            subtitleUI = uIContainerSubtitleChoice;
            subtitleUI.ShowSubtitle(data);
        }

        subtitleByID.Add(data.ID, subtitleUI);
    }

    private void RemoveSubtitle(string id)
    {
        if (!subtitleByID.ContainsKey(id))
        {
            return;
        }

        subtitleByID[id].RemoveSubtitle(id);
        subtitleByID.Remove(id);
    }
}
