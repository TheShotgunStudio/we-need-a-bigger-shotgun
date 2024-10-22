using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//This is a custom editor that modifies the inspector view of the static Gravity field. It hides the "Direction" inspector property if the mode is set to Rotational, and will also set the rotation to 0,0,0.
[CustomEditor(typeof(StaticGravityField))]
public class StaticGravFieldCustomEditor : Editor
{
    SerializedProperty Mode;
    SerializedProperty Direction;
    private void OnEnable()
    {
        Mode = serializedObject.FindProperty(nameof(StaticGravityField.Mode));
        Direction = serializedObject.FindProperty(nameof(StaticGravityField.GravityFacingDirection));
    }
    public override void OnInspectorGUI()
    {
        StaticGravityField gravityField = (StaticGravityField)target;
        EditorGUILayout.PropertyField(Mode);

        if (gravityField.Mode == GravityMode.CARDINAL)
        {
            EditorGUILayout.PropertyField(Direction);
            gravityField.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        DrawPropertiesExcluding(serializedObject, nameof(StaticGravityField.GravityFacingDirection), nameof(StaticGravityField.Mode));
        serializedObject.ApplyModifiedProperties();
    }
}
