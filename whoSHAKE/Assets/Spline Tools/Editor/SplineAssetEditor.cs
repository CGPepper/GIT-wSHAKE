// Copyright (c) 2015 Raed Abdullah
// this version is 1.4
// this class makes the drag and drop the spline asset from the project window to the scene possible

using UnityEngine;
using UnityEditor;

namespace SplineTool
{
    [CustomEditor(typeof(Spline))]
    public class SplineAssetEditor : Editor
    {
        static SplineAssetEditor()
        {
            DragAndDrop.AcceptDrag();
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            SceneView.onSceneGUIDelegate += SceneUpdate;
        }

        void OnEnable()
        {
            RenderStaticPreview("Assets/Spline Tools/imgs/Spline Icon.png", null, 32, 32);
            EditorUtility.SetDirty(target);
            
        }

        public override void OnInspectorGUI()
        {
            Spline spline = target as Spline;
            if (spline == null)
            {
                EditorGUILayout.HelpBox("spline is missing", MessageType.Error);
                return;
            }
            GUILayout.Label("number of points : " + spline.Length);
            spline.resolution = EditorGUILayout.FloatField("Resolution", spline.resolution);
        }

        static void SceneUpdate(SceneView sceneView)
        {
            if (DragAndDrop.objectReferences.Length == 1)
            {
                Spline spline = DragAndDrop.objectReferences[0] as Spline;
                if (spline != null)
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Move;
                    if (Event.current.type == EventType.DragPerform)
                    {
                        SplineCreator.CreateSplineGameObject(spline);
                    }
                }
            }
        }

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            Texture2D tex = new Texture2D(width, height);
            Texture2D t = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Spline Tools/imgs/Spline Icon.png");
            EditorUtility.CopySerialized(t, tex);
            return tex;
        }
    }
}
