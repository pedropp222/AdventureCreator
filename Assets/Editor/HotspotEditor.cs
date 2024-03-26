using UnityEditor;

[CustomEditor(typeof(Hotspot),true)]
public class HotspotEditor : Editor
{
    SerializedProperty ativado;

    SerializedProperty usarCustomCursor;
    SerializedProperty cursor;

    private void OnEnable()
    {
        ativado = serializedObject.FindProperty("ativado");
        usarCustomCursor = serializedObject.FindProperty("customStringMouseEnter");
        cursor = serializedObject.FindProperty("cursorValor");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        ativado.boolValue = EditorGUILayout.Toggle("Ativado: ",ativado.boolValue);

        usarCustomCursor.boolValue = EditorGUILayout.Toggle("Usar Outro Cursor: ",usarCustomCursor.boolValue);

        if (usarCustomCursor.boolValue)
        {
            cursor.stringValue = EditorGUILayout.TextField("Cursor: ",cursor.stringValue);
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}
