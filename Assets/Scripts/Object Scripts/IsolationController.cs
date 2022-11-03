using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsolationController
{
    private List<GameObject> allGameObjects = new List<GameObject>();
    private Stack<GameObject> undoStack = new Stack<GameObject>();
    private Stack<GameObject> redoStack = new Stack<GameObject>();
    private Stack<int> undoSequance = new Stack<int>();
    private Stack<int> redoSequance = new Stack<int>();
    List<GameObject> unSelected = new List<GameObject>();

    public IsolationController()
    {
        //get all 3d objects in the scene
        allGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Object"));
    }

    public void IsolateObjects(List<GameObject> selectedObjects)
    {

        if (selectedObjects.Count > 0)
        {
            allGameObjects.ForEach(obj =>
            {
                if (!selectedObjects.Contains(obj) && !undoStack.Contains(obj))
                {
                    unSelected.Add(obj);
                }else{
                    Debug.Log(obj.name + "Isolated");
                }
            });

            //update undoStack and undoSequance with unselected objects
            undoSequance.Push(unSelected.Count);

            unSelected.ForEach(obj =>
            {
                undoStack.Push(obj);
                obj.SetActive(false);
            });

            //clear redoStack ans redoSequance for each new action
            redoStack.Clear();
            redoSequance.Clear();
        }
        unSelected.Clear();
    }

    public void undo()
    {
        if(undoSequance.Count > 0)
        {
            int undoObjectCount = undoSequance.Pop();
            for(int i = 0; i < undoObjectCount; i++)
            {
                GameObject obj = undoStack.Pop();
                obj.SetActive(true);
                redoStack.Push(obj);
            }

            redoSequance.Push(undoObjectCount);
        }
    }
    public void redo()
    {
        if(redoSequance.Count > 0)
        {
            int redoObjectCount = redoSequance.Pop();

            for (int i = 0; i < redoObjectCount; i++)
            {
                GameObject obj = redoStack.Pop();
                obj.SetActive(false);
                undoStack.Push(obj);
            }

            undoSequance.Push(redoObjectCount);
        }
    }
}
