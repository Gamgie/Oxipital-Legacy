using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BalletManager))]
[CanEditMultipleObjects]
public class BalletManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BalletManager balletMngr = (BalletManager)target;

        ///////////////////////////////////////////////

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Pattern"))
        {
            balletMngr.AddPattern();
        }

        if (GUILayout.Button("Remove Pattern"))
        {
            balletMngr.RemovePattern();
        }
        GUILayout.EndHorizontal();

        base.DrawDefaultInspector();
    }
}
