using UnityEngine;
using System.Collections.Generic;

public class VideoTransition : MonoBehaviour
{
    private TransitionCut policyCut;
    private TransitionDissolve policyDissolve;
    private TransitionFade policyFade;

    private readonly Dictionary<TransitionPolicyEnum, ITransitionPolicy> policyDict = new();
    
    public void Initialize(VideoPlayController videoPlayController)
    {
        policyCut = new TransitionCut(videoPlayController);
        policyDissolve = new TransitionDissolve(videoPlayController);
        policyFade = new TransitionFade(videoPlayController);
        
        policyDict.Add(policyCut.PolicyEnum, policyCut);
        policyDict.Add(policyDissolve.PolicyEnum, policyDissolve);
        policyDict.Add(policyFade.PolicyEnum, policyFade);
    }

    public void ReserveTransition(VideoPlayerWrapper current, VideoPlayerWrapper next,
        TransitionPolicyEnum policyEnum, TransitionData transitionData)
    {
        ITransitionPolicy policy = policyDict[policyEnum];

        transitionData.SetData(current, next);
        StartCoroutine(policy.WaitForTransition(transitionData));
    }
}

public enum TransitionPolicyEnum
{
    Cut, // 단순 연결
    Dissolve, // 앞 영상이 서서히 사라지고 뒤 영상이 서서히 나타나며 두 화면이 겹쳐지는 방식
    Fade, // 검정으로 완전히 변했다가 다음장면으로 전환
}
