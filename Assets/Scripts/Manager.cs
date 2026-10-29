using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private VideoController videoController;
    [SerializeField] private SubtitleController subtitleController;
    [SerializeField] private SubtitleLoader subtitleLoader;
    [SerializeField] private UIController uIController;
    
    private readonly SubtitleDatas datas = new();

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
        
        subtitleLoader.OnParseSubtitleCompelete += datas.AddData;
    }

    private void UnbindEvents()
    {
        uIController.OnTestStartRequested -= videoController.OnPlay;
        
        subtitleController.OnShowSubtitleRequested -= HandleShowSubtitle;
        subtitleController.OnGetCurrentPlayerTimeRequest -= videoController.GetCurrentPlayerTime;
        
        subtitleLoader.OnParseSubtitleCompelete -= datas.AddData;
    }

    private void HandleShowSubtitle(char[] text, float duration)
    {
        uIController.OnShowSubtitleRequested?.Invoke(text, duration);
    }
}
