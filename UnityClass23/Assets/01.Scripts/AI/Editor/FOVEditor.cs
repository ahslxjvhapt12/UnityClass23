using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIBrain))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        float offset = 90f;

        AIBrain brain = target as AIBrain;

        float rad = (offset + -brain.ViewAngle * 0.5f) * Mathf.Deg2Rad;

        Vector3 angle = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0);

        Handles.color = Color.white;
        Handles.DrawWireDisc(brain.transform.position, Vector3.forward, brain.ViewRange);

        Handles.color = new Color(1, 1, 1, 0.5f);
        Handles.DrawSolidArc(brain.transform.position, Vector3.forward * -1,
            angle,
            brain.ViewAngle,
            brain.ViewRange);

        //NGUI
        GUIStyle style = new GUIStyle();
        style.fontSize = 35;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.black;
        Handles.Label(brain.transform.position + brain.transform.up * 2f, brain.ViewAngle.ToString(), style);
    }
}
