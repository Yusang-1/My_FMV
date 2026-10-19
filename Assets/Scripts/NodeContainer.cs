using UnityEngine;
using System.Collections.Generic;

public class NodeContainer : MonoBehaviour
{
    [SerializeField] private NodeSO[] nodeDataList;
    private readonly Dictionary<int, NodeSO> nodeDataDict = new();

    private void Start()
    {
        for (int i = 0; i < nodeDataList.Length; i++)
        {
            int code = nodeDataList[i].Code;
            if (!nodeDataDict.ContainsKey(code))
            {
                nodeDataDict.Add(code, nodeDataList[i]);
            }
        }
    }

    public bool TryGetNodeData(int code, out NodeSO node)
    {
        if (!nodeDataDict.ContainsKey(code))
        {
            node = null;
            return false;
        }

        node = nodeDataDict[code];
        return true;
    }

    public bool TryGetNextNodeData(int currentNodeCode, out NodeSO nextNode)
    {
        if (!nodeDataDict.ContainsKey(currentNodeCode))
        {
            nextNode = null;
            return false;
        }

        int nextCode = nodeDataDict[currentNodeCode].NextCode;
        if (!nodeDataDict.ContainsKey(nextCode))
        {
            nextNode = null;
            return false;
        }

        nextNode = nodeDataDict[nextCode];
        return true;
    }
}
