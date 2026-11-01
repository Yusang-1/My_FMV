using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private VideoController videoController;
    [SerializeField] private SubtitleController subtitleController;
    [SerializeField] private SubtitleLoader subtitleLoader;
    [SerializeField] private UIController uIController;
    [SerializeField] private BranchController branchController;

    private readonly SubtitleDatas datas = new();
    private CurrentSubtitleDatas currentSubtitleDatas;

    private void Awake()
    {
        currentSubtitleDatas = new(datas);
    }

    private void OnEnable()
    {
        BindEvents();
    }

    private void OnDisable()
    {
        UnbindEvents();
    }

    private void Start()
    {
        videoController.Initialize(branchController);
    }

    private void BindEvents()
    {
        uIController.OnTestStartRequested += videoController.OnPlay;

        subtitleController.OnStartShowSubtitleRequested += HandleShowSubtitle;
        subtitleController.OnRemoveSubtitleRequested += HandleRemoveSubtitle;
        subtitleController.OnGetCurrentPlayerTimeRequest += videoController.GetCurrentPlayerTime;
        subtitleController.OnGetCurrentPlayerRequested += videoController.GetCurrentPlayer;
        subtitleController.OnGetNextSubtitleDataRequested += currentSubtitleDatas.GetSubtitleData;

        subtitleLoader.OnParseSubtitleCompelete += datas.AddData;

        branchController.OnCurrentNodeChanged += currentSubtitleDatas.SetCurrentNodeSubtitleDatas;
        
        videoController.OnFirstPlayVideo += subtitleController.FirstSetSubtitle;
    }

    private void UnbindEvents()
    {
        uIController.OnTestStartRequested -= videoController.OnPlay;

        subtitleController.OnStartShowSubtitleRequested -= HandleShowSubtitle;
        subtitleController.OnRemoveSubtitleRequested -= HandleRemoveSubtitle;
        subtitleController.OnGetCurrentPlayerTimeRequest -= videoController.GetCurrentPlayerTime;
        subtitleController.OnGetCurrentPlayerRequested -= videoController.GetCurrentPlayer;
        subtitleController.OnGetNextSubtitleDataRequested -= currentSubtitleDatas.GetSubtitleData;

        subtitleLoader.OnParseSubtitleCompelete -= datas.AddData;

        branchController.OnCurrentNodeChanged -= currentSubtitleDatas.SetCurrentNodeSubtitleDatas;
        
        videoController.OnFirstPlayVideo -= subtitleController.FirstSetSubtitle;
    }

    private void HandleShowSubtitle(SubtitleData data)
    {
        uIController.OnShowSubtitleRequested?.Invoke(data);
    }
    
    private void HandleRemoveSubtitle(string id)
    {
        uIController.OnRemoveSubtitleRequested(id);
    }
}
