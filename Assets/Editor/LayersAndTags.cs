using UnityEditor;
using UnityEngine;
using System.Collections;

public class LayerAndTags : Editor
{
    #region Load Variables
    public int LayerCheckClear = 0;
    public float[] requiredLayers;
    public string[] s; //tags
    #endregion

    public void createTagsandLayers()
    {
        #region Check Layers & Add Layers
        string[] currentLayerNames = new string[32];
        requiredLayers = new float[5]; //0=Lower, 1=Middle, 2=Above,
        bool hasLower = false, hasMiddle = false, hasAbove = false, addLayers = false;
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layersProp = tagManager.FindProperty("layers");
        for (int a = 0; a <= 31; a++)
        {
            SerializedProperty layerVal = layersProp.GetArrayElementAtIndex(a);
            currentLayerNames[a] = layerVal.stringValue;
            if (currentLayerNames[a] == "2DLower")
            {
                hasLower = true;
                requiredLayers[0] = a;
            }
            else if (currentLayerNames[a] == "2DMid")
            {
                hasMiddle = true;
                requiredLayers[1] = a;
            }
            else if (currentLayerNames[a] == "2DAbove")
            {
                hasAbove = true;
                requiredLayers[2] = a;
            }
        }
        if (hasLower && hasMiddle && hasAbove)
        {
            LayerCheckClear = 1;
        }
        else
        {
            addLayers = true;
        }
        if (addLayers)
        {
            for (int a = 8; a <= 31; a++)
            {
                SerializedProperty layerVal = layersProp.GetArrayElementAtIndex(a);
                if (currentLayerNames[a] == "")
                {
                    if (!hasLower)
                    {
                        layerVal.stringValue = "2DLower";
                        hasLower = true;
                        requiredLayers[0] = a;
                    }
                    else if (!hasMiddle)
                    {
                        layerVal.stringValue = "2DMid";
                        hasMiddle = true;
                        requiredLayers[1] = a;
                    }
                    else if (!hasAbove)
                    {
                        layerVal.stringValue = "2DAbove";
                        hasAbove = true;
                        requiredLayers[2] = a;
                    }
                }
            }
            addLayers = false;
        }
        if (!hasLower || !hasMiddle || !hasAbove)
        {
            Debug.LogError("Not enough Layers available to create required. Please make 3 blank Layers available in the Layer Manager!");
            LayerCheckClear = -1;
        }
        #endregion

        #region Add Tags
        /*
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        bool hasFloorsTag = false, hasWallsTag = false, hasObjectsTag = false, hasNPCsTag = false;
        s = new string[4];
        s[0] = "Floors";
        s[1] = "Walls";
        s[2] = "Objects";
        s[3] = "NPCs";
        // Check Tag doesn't already exist
        for (int a = 0; a < tagsProp.arraySize; a++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(a);
            string b = t.stringValue.ToString();
            if (b == s[0])
            {
                hasFloorsTag = true;
            }
            if (b == s[1])
            {
                hasWallsTag = true;
            }
            if (b == s[2])
            {
                hasObjectsTag = true;
            }
            if (b == s[3])
            {
                hasNPCsTag = true;
            }
        }
        if (!hasFloorsTag)
        {
            tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
            n.stringValue = s[0];
            hasFloorsTag = true;
        }
        if (!hasWallsTag)
        {
            tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize-1);
            n.stringValue = s[1];
            hasWallsTag = true;
        }
        if (!hasObjectsTag)
        {
            tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
            n.stringValue = s[2];
            hasObjectsTag = true;
        }
        if (!hasNPCsTag)
        {
            tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
            n.stringValue = s[3];
            hasNPCsTag = true;
        }
        if (hasFloorsTag && hasWallsTag && hasObjectsTag && hasNPCsTag)
        {
            //Debug.Log("All Tags have been created.");
            LayerCheckClear = 1;
        }
        else
        {
            Debug.LogError("Not enough Tags available to create required. Please make 4 blank Tags available in the Tag Manager!");
            LayerCheckClear = -1;
        }
        */
        #endregion

        // Save changes.
        tagManager.ApplyModifiedProperties();
       
    }
}
