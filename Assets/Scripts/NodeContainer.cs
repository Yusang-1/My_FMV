using UnityEngine;
using System.Collections.Generic;

public class NodeContainer : MonoBehaviour
{
    [SerializeField] private NodeSO[] nodeDataList;
    private readonly Dictionary<string, NodeSO> nodeDataDict = new();

    private void Start()
    {
        for (int i = 0; i < nodeDataList.Length; i++)
        {
            string code = nodeDataList[i].Code;
            if (!nodeDataDict.ContainsKey(code))
            {
                nodeDataDict.Add(code, nodeDataList[i]);
            }
        }
    }

    public bool TryGetNodeData(string code, out NodeSO node)
    {
        if (!nodeDataDict.ContainsKey(code))
        {
            node = null;
            return false;
        }

        node = nodeDataDict[code];
        return true;
    }

    public bool TryGetNextNodeData(string currentNodeCode, out NodeSO nextNode)
    {
        if (!nodeDataDict.ContainsKey(currentNodeCode))
        {
            nextNode = null;
            return false;
        }

        string nextCode = nodeDataDict[currentNodeCode].NextNodes[0].NextCode;
        if (!nodeDataDict.ContainsKey(nextCode))
        {
            nextNode = null;
            return false;
        }

        nextNode = nodeDataDict[nextCode];
        return true;
    }
}
