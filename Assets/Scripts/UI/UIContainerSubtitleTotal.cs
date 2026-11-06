using UnityEngine;
using System;

public class UIContainerSubtitleTotal : MonoBehaviour
{
    public Action<SubtitleData> OnShowSubtitleRequested;
    public Action<string> OnRemoveSubtitleRequested;
    public Action<ChoiceData> OnShowChoiceRequested;
    public Action<string> OnRemoveChoiceRequested;
    public event Action<string> InvokeChoiceAction;

    [SerializeField] private UIContainerSubtitle uIContainerSubtitle;
    [SerializeField] private UIContainerChoice uIContainerChoice;
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
        OnShowSubtitleRequested += ShowSubtitle;
        OnRemoveSubtitleRequested += RemoveSubtitle;

        OnShowChoiceRequested += ShowChoice;
        OnShowChoiceRequested += uIContainerChoice.SetChoiceID;
        OnRemoveChoiceRequested += RemoveChoice;

        uIContainerChoice.InvokeChoiceAction += HandleChoiceAction;
    }

    private void UnbindEvents()
    {
        OnShowSubtitleRequested -= ShowSubtitle;
        OnRemoveSubtitleRequested -= RemoveSubtitle;

        OnShowChoiceRequested -= ShowChoice;
        OnShowChoiceRequested -= uIContainerChoice.SetChoiceID;
        OnRemoveChoiceRequested -= RemoveChoice;

        uIContainerChoice.InvokeChoiceAction -= HandleChoiceAction;
    }

    private void ShowSubtitle(SubtitleData data)
    {
        uIContainerSubtitle.ShowSubtitle(data);
    }
    private void RemoveSubtitle(string id)
    {
        uIContainerSubtitle.RemoveSubtitle(id);
    }
    private void ShowChoice(ChoiceData data)
    {
        uIContainerChoice.ShowChoice(data);
    }
    private void RemoveChoice(string id)
    {
        uIContainerChoice.RemoveSubtitle(id);
    }
    private void HandleChoiceAction(string choiceID)
    {
        InvokeChoiceAction?.Invoke(choiceID);
    }
}
