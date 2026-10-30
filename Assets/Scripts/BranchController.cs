using UnityEngine;
using System;

public class BranchController : MonoBehaviour
{
    public event Action<string> OnCurrentNodeChanged;

    [SerializeField] private NodeContainer nodeContainer;
    [SerializeField] private PlayerConditionSO playerConditionData;
    [SerializeField] private NodeSO startNode;
    private NodeSO currentNode;

    private NodeSO CurrentNode
    {
        set
        {
            currentNode = value;
            OnCurrentNodeChanged?.Invoke(currentNode.Code);
        }
    }

    private void Start()
    {
        playerConditionData.Initialize();
    }

    public NodeSO GetStartNode()
    {
        if (nodeContainer.TryGetNodeData("C01_M", out NodeSO node))
        {
            CurrentNode = node;
            return currentNode;
        }
        else return null;
    }

    public bool TryGetNextNode(out NextNode nextNode, out NodeSO nextNodeData)
    {
        if (currentNode.NextNodes == null || currentNode.NextNodes.Length == 0)
        {
            nextNode = default;
            nextNodeData = null;
            return false;
        }

        foreach (var next in currentNode.NextNodes)
        {
            bool isTotalTrue = true;
            foreach (var condition in next.Conditions)
            {
                if (condition == PlayerConditionEnum.None)
                {
                    break;
                }

                bool isConditionTrue = playerConditionData.ConditionDict[condition].Invoke();

                isTotalTrue = isTotalTrue && isConditionTrue;

                if (!isTotalTrue)
                {
                    break;
                }
            }

            if (isTotalTrue)
            {
                if (nodeContainer.TryGetNodeData(next.NextCode, out NodeSO getNextNode))
                {
                    nextNode = next;
                    nextNodeData = getNextNode;
                    return true;
                }
                else
                {
                    nextNode = default;
                    nextNodeData = null;
                    return false;
                }
            }
        }

        nextNode = default;
        nextNodeData = null;
        return false;
    }

    public void SetCurrentNode(NodeSO node)
    {
        CurrentNode = node;
    }
}
