using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Player Condition Data", menuName = "SO/Player Condition Data")]
public class PlayerConditionSO : ScriptableObject
{
    [SerializeField] private bool conditionA;
    [SerializeField] private bool conditionB;
    [SerializeField] private bool conditionC;
    [SerializeField] private bool conditionD;
    
    private readonly Dictionary<PlayerConditionEnum, Func<bool>> conditionDict = new();
    
    public Dictionary<PlayerConditionEnum, Func<bool>> ConditionDict => conditionDict;
    
    public void Initialize()
    {
        conditionDict.Add(PlayerConditionEnum.ConditionA, HandleReturnConditionA);
        conditionDict.Add(PlayerConditionEnum.ConditionB, HandleReturnConditionB);
        conditionDict.Add(PlayerConditionEnum.ConditionC, HandleReturnConditionC);
        conditionDict.Add(PlayerConditionEnum.ConditionD, HandleReturnConditionD);
    }
    
    private bool HandleReturnConditionA() => conditionA;
    private bool HandleReturnConditionB() => conditionB;
    private bool HandleReturnConditionC() => conditionC;
    private bool HandleReturnConditionD() => conditionD;
    
    public void SetConditionATrue() => conditionA = true;    
    public void SetConditionBTrue() => conditionB = true;    
    public void SetConditionCTrue() => conditionC = true;    
    public void SetConditionDTrue() => conditionD = true;    
}

public enum PlayerConditionEnum
{
    None,
    ConditionA,
    ConditionB,
    ConditionC,
    ConditionD
}
