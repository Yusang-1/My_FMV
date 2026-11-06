using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private VideoController videoController;
    [SerializeField] private VideoTransition transition;
    [SerializeField] private SubtitleController subtitleController;
    [SerializeField] private SubtitleLoader subtitleLoader;
    [SerializeField] private ChoiceController choiceController;
    [SerializeField] private ChoiceLoader choiceLoader;
    [SerializeField] private ChoiceActionDatas choiceActionDatas;
    [SerializeField] private ChoiceTextLoader choiceTextLoader;
    [SerializeField] private BranchController branchController;
    [SerializeField] private UIController uIController;

    private readonly SubtitleDatas subtitleDatas = new();
    private readonly ChoiceDatas choiceDatas = new();
    private readonly ChoiceTextDatas choiceTextDatas = new();
    private CurrentSubtitleDatas currentSubtitleDatas;

    private void Awake()
    {
        currentSubtitleDatas = new(subtitleDatas, choiceDatas);
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
        videoController.Initialize(branchController, transition);
    }

    private void BindEvents()
    {
        uIController.OnTestStartRequested += videoController.OnPlay;
        uIController.InvokeChoiceAction += choiceActionDatas.Dispatch;

        subtitleController.OnStartShowSubtitleRequested += HandleShowSubtitle;
        subtitleController.OnRemoveSubtitleRequested += HandleRemoveSubtitle;
        subtitleController.OnGetCurrentPlayerTimeRequest += videoController.GetCurrentPlayerTime;
        subtitleController.OnGetCurrentPlayerRequested += videoController.GetCurrentPlayer;
        subtitleController.OnGetNextSubtitleDataRequested += currentSubtitleDatas.GetSubtitleData;

        subtitleLoader.OnParseSubtitleCompelete += subtitleDatas.AddData;

        choiceController.OnStartShowChoiceRequested += HandleShowChoice;
        choiceController.OnRemoveChoiceRequested += HandleRemoveChoice;
        choiceController.OnGetCurrentPlayerTimeRequest += videoController.GetCurrentPlayerTime;
        choiceController.OnGetCurrentPlayerRequested += videoController.GetCurrentPlayer;
        choiceController.OnGetNextChoiceDataRequested += currentSubtitleDatas.GetChoiceData;
        choiceController.OnGetChoiceTextRequested += choiceTextDatas.GetData;

        choiceLoader.OnParseChoiceCompelete += choiceDatas.AddData;

        choiceTextLoader.OnParseChoiceCompelete += choiceTextDatas.AddData;

        transition.OnTransitionStart += branchController.SetCurrentNode;

        branchController.OnCurrentNodeChanged += currentSubtitleDatas.SetCurrentNodeSubtitleDatas;
        branchController.OnChangeCurrentNodeFinished += subtitleController.GetSubtitleDataOnLoadNextNode;
    }

    private void UnbindEvents()
    {
        uIController.OnTestStartRequested -= videoController.OnPlay;
        uIController.InvokeChoiceAction -= choiceActionDatas.Dispatch;

        subtitleController.OnStartShowSubtitleRequested -= HandleShowSubtitle;
        subtitleController.OnRemoveSubtitleRequested -= HandleRemoveSubtitle;
        subtitleController.OnGetCurrentPlayerTimeRequest -= videoController.GetCurrentPlayerTime;
        subtitleController.OnGetCurrentPlayerRequested -= videoController.GetCurrentPlayer;
        subtitleController.OnGetNextSubtitleDataRequested -= currentSubtitleDatas.GetSubtitleData;

        subtitleLoader.OnParseSubtitleCompelete -= subtitleDatas.AddData;

        choiceController.OnStartShowChoiceRequested -= HandleShowChoice;
        choiceController.OnRemoveChoiceRequested -= HandleRemoveChoice;
        choiceController.OnGetCurrentPlayerTimeRequest -= videoController.GetCurrentPlayerTime;
        choiceController.OnGetCurrentPlayerRequested -= videoController.GetCurrentPlayer;
        choiceController.OnGetNextChoiceDataRequested -= currentSubtitleDatas.GetChoiceData;
        choiceController.OnGetChoiceTextRequested -= choiceTextDatas.GetData;

        choiceLoader.OnParseChoiceCompelete -= choiceDatas.AddData;

        choiceTextLoader.OnParseChoiceCompelete -= choiceTextDatas.AddData;

        transition.OnTransitionStart -= branchController.SetCurrentNode;

        branchController.OnCurrentNodeChanged -= currentSubtitleDatas.SetCurrentNodeSubtitleDatas;
        branchController.OnChangeCurrentNodeFinished -= subtitleController.GetSubtitleDataOnLoadNextNode;
    }

    private void HandleShowSubtitle(SubtitleData data)
    {
        uIController.OnShowSubtitleRequested?.Invoke(data);
    }
    private void HandleRemoveSubtitle(string id)
    {
        uIController.OnRemoveSubtitleRequested(id);
    }
    private void HandleShowChoice(string id, string text)
    {
        uIController.OnShowChoiceRequested?.Invoke(id, text);
    }
    private void HandleSetChoice(ChoiceData data)
    {
        uIController.OnSetChoiceRequested?.Invoke(data);
    }
    private void HandleRemoveChoice(string id)
    {
        uIController.OnRemoveChoiceRequested(id);
    }
}
