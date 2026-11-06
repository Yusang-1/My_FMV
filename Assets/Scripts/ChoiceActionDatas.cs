using UnityEngine;
using System;
using System.Collections.Generic;

public class ChoiceActionDatas : MonoBehaviour
{
    [SerializeField] private PlayerConditionSO playerConditionData;

    private Dictionary<string, Action> handlers;

    private void Awake()
    {
        handlers = new()
        {
            ["A"] = ConditionA,
            ["B"] = ConditionB,
            ["C"] = ConditionC,
            ["D"] = ConditionD
        };
    }

    public void Dispatch(string choiceID)
    {
        if (string.IsNullOrEmpty(choiceID))
            return;

        if (handlers.TryGetValue(choiceID, out var handler))
            handler.Invoke();
        else
            Debug.LogWarning($"등록되지 않은 ActionId: {choiceID}");
    }

    private void ConditionA()
    {
        playerConditionData.SetConditionATrue();
    }

    private void ConditionB()
    {
        playerConditionData.SetConditionBTrue();
    }

    private void ConditionC()
    {
        playerConditionData.SetConditionCTrue();
    }

    private void ConditionD()
    {
        playerConditionData.SetConditionDTrue();
    }
}
