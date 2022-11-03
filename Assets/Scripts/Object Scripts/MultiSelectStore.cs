using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MultiSelectStore
{
    private List<GameObject> selectedObjects;

    private MultiSelectStore()
    {
        selectedObjects = new List<GameObject>();
    }

    private static MultiSelectStore instance = null;
    public static MultiSelectStore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MultiSelectStore();
            }
            return instance;
        }
    }

    public void addObject(GameObject obj)
    {
        selectedObjects.Add(obj);
        Debug.Log(obj.name + "Added");
    }

    public void removeObject(GameObject obj)
    {
        selectedObjects.Remove(obj);
    }

    public void removeAllObject()
    {
        selectedObjects.Clear();
    }

    public List<GameObject> getSelectedObjects()
    {
        return selectedObjects;
    }

    public bool findObject(GameObject obj)
    {
        if (selectedObjects.Contains(obj))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
