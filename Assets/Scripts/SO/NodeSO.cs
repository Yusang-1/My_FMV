using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "SO/Node/NodeData")]
public class NodeSO : ScriptableObject
{
    public int Code;
    public string VideoAdress;
    public VideoType VideoType;
    public int NextCode;
}

public enum VideoType
{
    Normal,
    Loop
}
