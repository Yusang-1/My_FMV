using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Player Condition Data", menuName = "SO/Player Condition Data")]
public class PlayerConditionSO : ScriptableObject
{
    [SerializeField] private bool conditionA;
    [SerializeField] private bool conditionB;
    
    private readonly Dictionary<PlayerConditionEnum, Func<bool>> conditionDict = new();
    
    public Dictionary<PlayerConditionEnum, Func<bool>> ConditionDict => conditionDict;
    
    public void Initialize()
    {
        conditionDict.Add(PlayerConditionEnum.ConditionA, HandleReturnConditionA);
        conditionDict.Add(PlayerConditionEnum.ConditionB, HandleReturnConditionB);
    }
    
    private bool HandleReturnConditionA() => conditionA;
    private bool HandleReturnConditionB() => conditionB;
}

public enum PlayerConditionEnum
{
    None,
    ConditionA,
    ConditionB
}
