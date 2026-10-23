using UnityEngine;

public class BranchController : MonoBehaviour
{
    [SerializeField] private NodeContainer nodeContainer;
    [SerializeField] private PlayerConditionSO playerConditionData;
    [SerializeField] private NodeSO startNode;
    private NodeSO currentNode;

    private void Start()
    {
        playerConditionData.Initialize();
    }

    public NodeSO GetStartNode()
    {
        if (nodeContainer.TryGetNodeData(0, out NodeSO node))
        {
            currentNode = node;
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
        currentNode = node;
    }
}
