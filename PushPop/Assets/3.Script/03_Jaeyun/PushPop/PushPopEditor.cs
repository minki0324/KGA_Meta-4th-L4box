using UnityEditor; // Editor
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(PushPop))]
public class PushPopEditor : Editor
{
    // Inspector 창에 나타낼 요소
    SerializedProperty pushPopCanvas;
    SerializedProperty pushPopButton;
    SerializedProperty boardObject;
    SerializedProperty boardSprite;
    SerializedProperty boardSize;
    SerializedProperty grid;
    SerializedProperty percentage;
    SerializedProperty posPrefab;
    SerializedProperty buttonSize;

    private void OnEnable()
    {
        // Inspector
        pushPopCanvas = serializedObject.FindProperty("pushPopCanvas");
        pushPopButton = serializedObject.FindProperty("pushPopButton");

        boardObject = serializedObject.FindProperty("boardObject");
        boardSprite = serializedObject.FindProperty("boardSprite");
        boardSize = serializedObject.FindProperty("boardSize");
        buttonSize = serializedObject.FindProperty("buttonSize");

        grid = serializedObject.FindProperty("grid");

        percentage = serializedObject.FindProperty("percentage");
        posPrefab = serializedObject.FindProperty("posPrefab");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(pushPopCanvas);
        EditorGUILayout.PropertyField(pushPopButton);

        EditorGUILayout.PropertyField(boardObject);
        EditorGUILayout.PropertyField(boardSprite);

        EditorGUILayout.PropertyField(grid);

        EditorGUILayout.PropertyField(boardSize);
        EditorGUILayout.PropertyField(buttonSize);
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

        if (GUILayout.Button("Button Setting"))
        {
            pushPop.SettingPushPopButton();
        }

        if (GUILayout.Button("Delete PushPop"))
        {
            pushPop.DestroyObject();
        }
    }
}
