using System;

public class UIContainerSubtitleChoice : UIContainerSubtitle
{
    private Action firstChoiceAction;
    private Action secondChoiceAction;

    public void SetChoiceAction(Action firstChoice, Action secondChoice)
    {
        firstChoiceAction = firstChoice;
        secondChoiceAction = secondChoice;
    }

    public void OnChoiceFirst()
    {
        firstChoiceAction?.Invoke();
    }

    public void OnChoiceSecond()
    {
        secondChoiceAction?.Invoke();
    }
}
