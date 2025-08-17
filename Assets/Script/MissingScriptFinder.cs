// File: Assets/Editor/MissingScriptFinder.cs
using UnityEngine;
using UnityEditor;

public class MissingScriptFinder : MonoBehaviour
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    static void FindMissingScripts()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int missingCount = 0;

        foreach (GameObject obj in allObjects)
        {
            Component[] components = obj.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing script found in: {GetFullPath(obj)}", obj);
                    missingCount++;
                }
            }
        }

        Debug.Log($"Search complete. Found {missingCount} missing scripts.");
    }

    static string GetFullPath(GameObject obj)
    {
        string path = obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = obj.name + "/" + path;
        }
        return path;
    }
}
