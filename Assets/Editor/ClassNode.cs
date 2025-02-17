using System.Collections.Generic;
using UnityEngine;

public class ClassNode
{
    public Rect Rect;
    public string Name;
    public List<string> Variables = new List<string>();
    public List<string> Methods = new List<string>();
    public int ParentIndex = -1;
    private bool _isDragging;
    private Vector2 _dragOffset;

    public ClassNode(Vector2 position)
    {
        Rect = new Rect(position.x, position.y, 200, 150);
        Name = "New Class";
    }

    /// <summary>
    /// ノードを作成
    /// </summary>
    public void Draw()
    {
        GUILayout.BeginArea(Rect, GUI.skin.box);
        Name = GUILayout.TextField(Name);

        // 変数のエリア
        GUILayout.Label("Variables:");
        for (int i = 0; i < Variables.Count; i++)
        {
            Variables[i] = GUILayout.TextField(Variables[i]);
        }
        if (GUILayout.Button("Add Variable"))
        {
            Variables.Add("New Variable");
        }
        
        // メソッドのエリア
        GUILayout.Label("Methods:");
        for (int i = 0; i < Methods.Count; i++)
        {
            Methods[i] = GUILayout.TextField(Methods[i]);
        }
        if (GUILayout.Button("Add Method"))
        {
            Methods.Add("New Method");
        }
        
        GUILayout.EndArea();
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0 && Rect.Contains(e.mousePosition))
                {
                    _isDragging = true;
                    _dragOffset = e.mousePosition - new Vector2(Rect.x, Rect.y);
                    return true;
                }
                break;
            case EventType.MouseDrag:
                if (_isDragging)
                {
                    Rect.position = e.mousePosition - _dragOffset;
                    return true;
                }
                break;
            case EventType.MouseUp:
                _isDragging = false;
                break;
        }
        return false;
    }
}