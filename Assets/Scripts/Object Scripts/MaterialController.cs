using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController
{
    struct Object {
        public GameObject obj;
        public Material defaultMat;
    }

    private List<Object> gameObjects = new List<Object>();

    public MaterialController(List<GameObject> objects){
         objects.ForEach(obj =>
        {
            Object newObject = new Object();
            newObject.obj = obj;
            newObject.defaultMat = obj.GetComponent<Renderer>().material;
            gameObjects.Add(newObject);
        });
    }

    public void addMaterial(GameObject obj, Material mat){
        obj.GetComponent<MeshRenderer>().material = mat;
    }

    public void removeMaterial(GameObject obj){
        gameObjects.ForEach(item => {
            if(item.obj.name == obj.name){
                obj.GetComponent<MeshRenderer>().material = item.defaultMat;
            }
        });
    }

    public void removeMaterialOfAllObjects(){
        gameObjects.ForEach(item => {
            item.obj.GetComponent<MeshRenderer>().material = item.defaultMat;
        });
    }
}
