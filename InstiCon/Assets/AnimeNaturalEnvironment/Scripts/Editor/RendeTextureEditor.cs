using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(RenderTextureBaker))]
public class RenderTextureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RenderTextureBaker renderMap = (RenderTextureBaker)target;

        if (GUILayout.Button("Set up Miminap Camera"))

        {
            renderMap.GetBounds();
            renderMap.SetUpCam();
            
        }
        if (GUILayout.Button("Draw Diffuse Map")) renderMap.ReDrawDiffuseMap();
    }

}
