using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PathSystem
{
    [CustomEditor(typeof(PathNode))]
    [CanEditMultipleObjects]
    public class NodeConnectorEditor: Editor
    {
        private const string NearNodesProperty = "<NearNodes>k__BackingField";
        
        private void OnSceneGUI()
        {
            Handles.BeginGUI();
            GUILayout.BeginArea(new Rect(10, 10, 250, 80));
            EditorGUILayout.BeginVertical();
            
            bool isConnectButtonClicked = GUILayout.Button("ConnectNodes");
            bool isUnConnectButtonClicked =GUILayout.Button("UnConnectNodes");
            bool isClearButtonClicked = GUILayout.Button("RemoveConnections");
        
            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
            Handles.EndGUI();
            
            if (!isConnectButtonClicked && !isUnConnectButtonClicked && !isClearButtonClicked) return;
            List<PathNode> pathNodes;
            pathNodes = GetSelectedNodes();
            if (isConnectButtonClicked) NodeConnect(pathNodes);
            if (isUnConnectButtonClicked) NodeUnConnect(pathNodes);
            if (isClearButtonClicked) ClearNode(pathNodes);
        }

        private void ClearNode(List<PathNode> pathNodes)
        {
            foreach (var node1 in pathNodes)
            {
                var serialzedObject = new SerializedObject(node1);
                var array = serialzedObject.FindProperty(NearNodesProperty);

                for (int i = 0; i < array.arraySize; i++)
                {
                    var node2 = array.GetArrayElementAtIndex(i).objectReferenceValue;
                    var serialzedObject2 = new SerializedObject(node2);
                    var array2 = serialzedObject2.FindProperty(NearNodesProperty);
                    for (int j = 0; j < array2.arraySize; j++)
                    {
                        if (array2.GetArrayElementAtIndex(j).objectReferenceValue == node1)
                        {
                            array2.GetArrayElementAtIndex(j).objectReferenceValue = null;
                            array2.DeleteArrayElementAtIndex(j);
                            break;
                        }
                    }

                    serialzedObject2.ApplyModifiedProperties();
                }

                array.ClearArray();

                serialzedObject.ApplyModifiedProperties();
            }
        }

        private void NodeConnect(List<PathNode> pathNodes)
        {
            if (pathNodes.Count < 2) return;
            foreach (var node1 in pathNodes)
            {
                var serialzedObject = new SerializedObject(node1);
                var array = serialzedObject.FindProperty(NearNodesProperty);

                foreach (var node2 in pathNodes)
                {
                    if (node1 == node2) continue;
                    bool isAdded = false;
                    for (int i = 0; i < array.arraySize; i++)
                    {
                        if (array.GetArrayElementAtIndex(i).objectReferenceValue == node2)
                        {
                            isAdded = true;
                            break;
                        }
                    }

                    if (isAdded) continue;
                    array.InsertArrayElementAtIndex(array.arraySize);
                    array.GetArrayElementAtIndex(array.arraySize - 1).objectReferenceValue = node2;
                }

                serialzedObject.ApplyModifiedProperties();
            }
        }

        private void NodeUnConnect(List<PathNode> pathNodes)
        {
            if (pathNodes.Count < 2) return;
            foreach (var node1 in pathNodes)
            {
                var serialzedObject = new SerializedObject(node1);
                var array = serialzedObject.FindProperty(NearNodesProperty);

                foreach (var node2 in pathNodes)
                {
                    if (node1 == node2) continue;
                    bool isAdded = false;
                    for (int i = 0; i < array.arraySize; i++)
                    {
                        if (array.GetArrayElementAtIndex(i).objectReferenceValue == node2)
                        {
                            array.GetArrayElementAtIndex(i).objectReferenceValue = null;
                            array.DeleteArrayElementAtIndex(i);
                            break;
                        }
                    }
                }

                serialzedObject.ApplyModifiedProperties();
            }
        }

        private List<PathNode> GetSelectedNodes()
        {
            GameObject[] selected = Selection.gameObjects;
            List<PathNode> pathNodes = new List<PathNode>();
            foreach (var node in selected)
            {
                PathNode pathnode;
                if (node.TryGetComponent(out pathnode))
                    pathNodes.Add(pathnode);
            }

            return pathNodes;
        }
    }
}
