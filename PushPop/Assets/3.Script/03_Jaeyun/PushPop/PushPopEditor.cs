using UnityEditor; // Editor
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(PushPopTest))]
public class PushPopEditor : Editor
{
    // push pop canvas
    SerializedProperty pushPopCanvas;
    SerializedProperty pushPopButtonPrefab;
    SerializedProperty boardPrefabUI;

    // push pop board
    SerializedProperty boardPrefab;
    SerializedProperty boardSprite;
    SerializedProperty spriteName;
    SerializedProperty spriteAtlas;

    // grid size
    SerializedProperty buttonCanvas;
    SerializedProperty percentage;
    SerializedProperty buttonSize;

    // grid pos
    SerializedProperty posPrefab;
    SerializedProperty buttonCount;

    bool show = false;

    private void OnEnable()
    {
        // Inspector
        pushPopCanvas = serializedObject.FindProperty("pushPopCanvas");
        pushPopButtonPrefab = serializedObject.FindProperty("pushPopButtonPrefab");
        boardPrefabUI = serializedObject.FindProperty("boardPrefabUI");

        boardPrefab = serializedObject.FindProperty("boardPrefab");
        boardSprite = serializedObject.FindProperty("boardSprite");
        spriteName = serializedObject.FindProperty("spriteName");
        spriteAtlas = serializedObject.FindProperty("spriteAtlas");

        buttonCanvas = serializedObject.FindProperty("buttonCanvas");
        percentage = serializedObject.FindProperty("percentage");
        buttonSize = serializedObject.FindProperty("buttonSize");

        posPrefab = serializedObject.FindProperty("posPrefab");
        buttonCount = serializedObject.FindProperty("buttonCount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PushPopTest pushPopTest = (PushPopTest)target;

        show = EditorGUILayout.Foldout(show, "Don't Edit");

        if (show)
        {
            EditorGUI.indentLevel += 2;
            EditorGUILayout.PropertyField(pushPopCanvas);
            EditorGUILayout.PropertyField(pushPopButtonPrefab);
            EditorGUILayout.PropertyField(boardPrefabUI);
            EditorGUILayout.PropertyField(boardPrefab);
            EditorGUILayout.PropertyField(buttonCanvas);
            EditorGUILayout.PropertyField(posPrefab);
            EditorGUILayout.PropertyField(spriteAtlas);
            EditorGUI.indentLevel -= 2;
        }

        EditorGUILayout.PropertyField(percentage);
        EditorGUILayout.PropertyField(buttonSize);
        // EditorGUILayout.PropertyField(boardSprite);
        EditorGUILayout.PropertyField(spriteName);
        if (GUILayout.Button("PushPop"))
        {
            pushPopTest.PushPop();
        }
        EditorGUILayout.PropertyField(buttonCount);

        serializedObject.ApplyModifiedProperties();
    }
}
