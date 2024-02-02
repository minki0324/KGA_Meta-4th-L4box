using UnityEditor; // Editor
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(PushPop))]
public class PushPopEditor : Editor
{
    // Inspector 창에 나타낼 요소
    SerializedProperty boardObject;
    SerializedProperty boardSprite;
    SerializedProperty boardSize;
    SerializedProperty grid;
    SerializedProperty percentage;
    SerializedProperty posPrefab;
    SerializedProperty spacing;

    private void OnEnable()
    {
        // Inspector
        boardObject = serializedObject.FindProperty("boardObject");
        boardSprite = serializedObject.FindProperty("boardSprite");
        boardSize = serializedObject.FindProperty("boardSize");

        grid = serializedObject.FindProperty("grid");

        percentage = serializedObject.FindProperty("percentage");
        posPrefab = serializedObject.FindProperty("posPrefab");

        spacing = serializedObject.FindProperty("spacing");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(boardObject);
        EditorGUILayout.PropertyField(boardSprite);

        EditorGUILayout.PropertyField(grid);
        EditorGUILayout.PropertyField(spacing);

        EditorGUILayout.PropertyField(boardSize);
        EditorGUILayout.PropertyField(percentage);

        EditorGUILayout.PropertyField(posPrefab);


        serializedObject.ApplyModifiedProperties();

        PushPop pushPop = (PushPop)target;

        if (GUILayout.Button("Create Board"))
        {
            pushPop.CreateGameObject();
        }

        if (GUILayout.Button("Set Board Size"))
        {
            pushPop.SetBoardSize();
        }

        if (GUILayout.Button("Create Grid"))
        {
            pushPop.DrawGrid();
        }
    }
}
