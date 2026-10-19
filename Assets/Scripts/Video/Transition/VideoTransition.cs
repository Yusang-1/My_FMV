using UnityEngine;
using System.Collections.Generic;

public class VideoTransition : MonoBehaviour
{
    private readonly TransitionCut policyCut = new();

    private readonly Dictionary<TransitionPolicyEnum, ITransitionPolicy> policyDict = new();

    private void Start()
    {
        policyDict.Add(policyCut.PolicyEnum, policyCut);
    }
    
    public void Transition(VideoPlayerWrapper prev, VideoPlayerWrapper next, TransitionPolicyEnum policyEnum = TransitionPolicyEnum.Cut)
    {
        ITransitionPolicy policy = policyDict[policyEnum];

        policy.DoTransition(prev, next);
    }
}

public enum TransitionPolicyEnum
{
    Cut, // 단순 연결
    Dissolve, // 앞 영상이 서서히 사라지고 뒤 영상이 서서히 나타나며 두 화면이 겹쳐지는 방식
    Fade, // 검정으로 완전히 변했다가 다음장면으로 전환
}
