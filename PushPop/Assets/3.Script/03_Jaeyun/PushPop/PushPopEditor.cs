/*using UnityEditor; // Editor
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(PushPopTest))]
public class PushPopEditor : Editor
{
    // push pop canvas
    SerializedProperty pushPopCanvas;
    SerializedProperty pushPopButtonPrefab;

    // push pop board
    SerializedProperty boardPrefab;
    SerializedProperty boardSprite;

    // grid size
    SerializedProperty percentage;
    SerializedProperty buttonSize;

    // grid pos
    SerializedProperty posPrefab;

    private void OnEnable()
    {
        // Inspector
        pushPopCanvas = serializedObject.FindProperty("pushPopCanvas");
        pushPopButtonPrefab = serializedObject.FindProperty("pushPopButtonPrefab");

        boardPrefab = serializedObject.FindProperty("boardPrefab");
        boardSprite = serializedObject.FindProperty("boardSprite");

        percentage = serializedObject.FindProperty("percentage");
        buttonSize = serializedObject.FindProperty("buttonSize");

        posPrefab = serializedObject.FindProperty("posPrefab");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PushPopTest pushPopTest = (PushPopTest)target;
        EditorGUILayout.PropertyField(pushPopCanvas);
        EditorGUILayout.PropertyField(pushPopButtonPrefab);
        if (GUILayout.Button("Create Board"))
        {
            pushPopTest.CreatePushPopBoard();
        }

        EditorGUILayout.PropertyField(boardPrefab);
        EditorGUILayout.PropertyField(boardSprite);
        if (GUILayout.Button("CreateGrid"))
        {
            pushPopTest.CreateGrid();
        }

        EditorGUILayout.PropertyField(percentage);
        EditorGUILayout.PropertyField(buttonSize);
        if (GUILayout.Button("PushPopButtonSetting"))
        {
            pushPopTest.PushPopButtonSetting();
        }

        EditorGUILayout.PropertyField(posPrefab);
        if (GUILayout.Button("DestroyObject"))
        {
            pushPopTest.DestroyObject();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
*/