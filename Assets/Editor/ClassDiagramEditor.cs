using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// クラス図を書けるウィンドウを追加する
/// </summary>
public class ClassDiagramEditor : EditorWindow
{
    private List<ClassNode> _classNodes = new List<ClassNode>();
    private Vector2 _offset, _drag;
    private ClassNode _selectedNode;

    /// <summary>
    /// ウィンドウを表示する
    /// </summary>
    [MenuItem("Tools/Class Diagram Editor")]
    public static void ShowWindow()
    {
        ClassDiagramEditor window = GetWindow<ClassDiagramEditor>("Class Diagram");
        window.LoadDiagram(); // データを復元する
    }

    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);
        DrawNodes();
        DrawConnections();
        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);
        DrawToolbar();
        if (GUI.changed) Repaint();
    }
    
    /// <summary>
    /// 上部のツールバーを表示する
    /// </summary>
    private void DrawToolbar()
    {
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Add Class"))
        { 
            //クラス追加ボタン
            _classNodes.Add(new ClassNode(new Vector2(200, 200)));
        }
        if (GUILayout.Button("Save"))
        {
            //セーブボタン
            SaveDiagram();
        }
        
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// グリッド表示
    /// </summary>
    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);
        
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
        _offset += _drag;
        Vector2 newOffset = new Vector2(_offset.x % gridSpacing, _offset.y % gridSpacing);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector2(gridSpacing * i + newOffset.x, 0), 
                new Vector2(gridSpacing * i + newOffset.x, position.height));
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector2(0, gridSpacing * j + newOffset.y), 
                new Vector2(position.width, gridSpacing * j + newOffset.y));
        }
        
        Handles.color = Color.white;
    }

    /// <summary>
    /// 新しくノードを作成する
    /// </summary>
    private void DrawNodes()
    {
        foreach (var node in _classNodes)
        {
            node.Draw();
        }
    }

    /// <summary>
    /// ノードの依存関係を描画する
    /// </summary>
    private void DrawConnections()
    {
        foreach (var node in _classNodes)
        {
            if (node.ParentIndex >= 0 && node.ParentIndex < _classNodes.Count)
            {
                Handles.DrawLine(node.Rect.center, _classNodes[node.ParentIndex].Rect.center);
            }
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        for (int i = _classNodes.Count - 1; i >= 0; i--)
        {
            if (_classNodes[i].ProcessEvents(e))
            {
                if (e.type == EventType.MouseDown && e.button == 1)
                {
                    _classNodes.RemoveAt(i);
                }
                GUI.changed = true;
            }
        }
    }


    private void ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDrag:
                if (e.button == 2)
                {
                    _drag = e.delta;
                    GUI.changed = true;
                }
                break;
            case EventType.MouseUp:
                _drag = Vector2.zero;
                break;
        }
    }
    
    private void SaveDiagram()
    {
        EditorPrefs.SetString("ClassDiagramData", JsonUtility.ToJson(new ClassDiagramData(_classNodes)));
    }

    private void LoadDiagram()
    {
        string json = EditorPrefs.GetString("ClassDiagramData", "");
        if (!string.IsNullOrEmpty(json))
        {
            ClassDiagramData data = JsonUtility.FromJson<ClassDiagramData>(json);
            _classNodes = data?.ToClassNodes() ?? new List<ClassNode>();
        }
        else
        {
            _classNodes = new List<ClassNode>();
        }
    }
}