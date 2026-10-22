using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NodeData", menuName = "SO/Node/NodeData")]
public class NodeSO : ScriptableObject
{
    public int Code;
    public string VideoAdress;
    public VideoType VideoType;
    public NextNode[] NextNodes;
}

[Serializable]
public struct NextNode
{
    public int NextCode;
    
    public PlayerConditionEnum[] Conditions;
    
    public TransitionPolicyEnum TransitionType;
    
    [SerializeReference]
    public TransitionData TransitionData;
}

public enum VideoType
{
    Normal,
    Loop
}
