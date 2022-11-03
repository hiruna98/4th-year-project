using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController 
{
    
    private Quaternion defaultRotation;
    
    private ViewController()
    {
        
    }

    private static ViewController instance = null;
    public static ViewController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ViewController();
            }
            return instance;
        }
    }

    public void initializeRotation(GameObject obj)
    {
        defaultRotation = obj.transform.rotation;
    }

    public void antRotation(GameObject obj)
    {
        obj.transform.localRotation = Quaternion.Euler(0f,180f,0f);
    }

    public void posRotation(GameObject obj)
    {
        obj.transform.localRotation= Quaternion.Euler(0f,0f,0f);
    }

    public void latRotation(GameObject obj)
    {
        obj.transform.localRotation= Quaternion.Euler(0f,-90f,0f);
    }

    public void medRotation(GameObject obj)
    {
        obj.transform.localRotation= Quaternion.Euler(0f,90f,0f);
    }

    public void supRotation(GameObject obj)
    {
        obj.transform.localRotation= Quaternion.Euler(90f,0f,0f);
    }

    public void infRotation(GameObject obj)
    {
        obj.transform.localRotation= Quaternion.Euler(-90f,0f,0f);
    }

    public void centerPos(GameObject obj){
        obj.transform.position = new Vector3(0.0f,0.0f,0.0f);
        obj.transform.localScale = new Vector3(300.0f,300.0f,300.0f);
    }
}
