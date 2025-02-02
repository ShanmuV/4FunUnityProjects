using UnityEditor;


[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{

    SerializedProperty type;
    SerializedProperty lines;
    SerializedProperty choices;
    SerializedProperty sceneName;
    SerializedProperty hasLines;
    SerializedProperty hasChoices;
    SerializedProperty dialogueEvents;
    SerializedProperty hasEventStopper;


    private void OnEnable()
    {
        type = serializedObject.FindProperty("type");
        lines = serializedObject.FindProperty("lines");
        choices = serializedObject.FindProperty("choices");
        sceneName = serializedObject.FindProperty("sceneName");
        hasLines = serializedObject.FindProperty("hasLines");
        hasChoices = serializedObject.FindProperty("hasChoices");
        dialogueEvents = serializedObject.FindProperty("dialogueEvent");
        hasEventStopper = serializedObject.FindProperty("hasEventStopper");
    }

    public override void OnInspectorGUI()
    {
        Dialogue _dialogue = (Dialogue)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(type);

        switch (_dialogue.type)
        {
            case Dialogue.Type.Normal:
                EditorGUILayout.PropertyField(lines);
                EditorGUILayout.PropertyField(hasEventStopper);
                if (_dialogue.hasEventStopper)
                {
                    EditorGUILayout.PropertyField(dialogueEvents);
                }
                else
                {
                    _dialogue.dialogueEvent = DialogueEvents.None;
                }
                break;

            case Dialogue.Type.Choices:

                EditorGUILayout.PropertyField(lines);
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(choices);
                EditorGUILayout.PropertyField(hasEventStopper);
                if (_dialogue.hasEventStopper)
                {
                    EditorGUILayout.PropertyField(dialogueEvents);
                }
                else
                {
                    _dialogue.dialogueEvent = DialogueEvents.None;
                }
                break;

            case Dialogue.Type.SceneChange:

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(hasLines);
                EditorGUILayout.PropertyField(hasChoices);
                EditorGUILayout.EndHorizontal();
                if (_dialogue.hasLines)
                {
                    EditorGUILayout.PropertyField(lines);
                    EditorGUILayout.Space(5);
                }
                else
                {
                    _dialogue.lines = null;
                }
                if (_dialogue.hasChoices)
                {
                    EditorGUILayout.PropertyField(choices);
                    EditorGUILayout.Space(5);
                }
                else
                {
                    _dialogue.choices = null;
                }
                EditorGUILayout.PropertyField(sceneName);
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(hasEventStopper);
                if (_dialogue.hasEventStopper)
                {
                    EditorGUILayout.PropertyField(dialogueEvents);
                }
                else
                {
                    _dialogue.dialogueEvent = DialogueEvents.None;
                }
                break;

            case Dialogue.Type.FireEvent:

                EditorGUILayout.PropertyField(dialogueEvents);
                break;
        }



        serializedObject.ApplyModifiedProperties();
    }
}
