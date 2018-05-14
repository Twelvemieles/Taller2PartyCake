using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
//[CustomEditor(typeof(Campanero))]
public class campanerovieweditor// : Editor
{
   /** void OnSceneGUI()
    {
        Campanero cp = (Campanero)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(cp.transform.position, Vector3.up, Vector3.forward, 360, cp.viewRadius);
        Vector3 cpviewAngleA = cp.DirFromAngle(-cp.viewAngle / 2, false);
        Vector3 cpviewAngleB = cp.DirFromAngle(cp.viewAngle / 2, false);

        Handles.DrawLine(cp.transform.position, cp.transform.position + cpviewAngleA * cp.viewRadius);
        Handles.DrawLine(cp.transform.position, cp.transform.position + cpviewAngleB * cp.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in cp.VisibleTargets)
        {
            Handles.DrawLine(cp.transform.position, visibleTarget.position);
        }
    }*/
}
