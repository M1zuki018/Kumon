using System.Collections.Generic;

[System.Serializable]
public class ClassDiagramData
{
    public List<ClassNode> Nodes;

    public ClassDiagramData(List<ClassNode> nodes)
    {
        Nodes = nodes;
    }

    public List<ClassNode> ToClassNodes()
    {
        return Nodes ?? new List<ClassNode>();
    }
}