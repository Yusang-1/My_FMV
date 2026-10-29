using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private VideoController videoController;
    [SerializeField] private SubtitleController subtitleController;
    [SerializeField] private UIController uIController;

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
        uIController.OnTestStartRequested += videoController.OnPlay;

        subtitleController.OnShowSubtitleRequested += HandleShowSubtitle;
        subtitleController.OnGetCurrentPlayerTimeRequest += videoController.GetCurrentPlayerTime;
    }

    private void UnbindEvents()
    {
        uIController.OnTestStartRequested -= videoController.OnPlay;
        
        subtitleController.OnShowSubtitleRequested -= HandleShowSubtitle;
        subtitleController.OnGetCurrentPlayerTimeRequest -= videoController.GetCurrentPlayerTime;
    }

    private void HandleShowSubtitle(char[] text, float duration)
    {
        uIController.OnShowSubtitleRequested?.Invoke(text, duration);
    }
}
