using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TouchController : MonoBehaviour
{
    public enum SelMode : int
    {
        OnlyParent = 0,
        AndChildren = 1
    }
    public SelMode SelectionMode = SelMode.AndChildren;

    public Material selectionMat;

    private Vector2 startPos;
    private Vector2 endPos;

    //to identify long press
    private float timePressed = 0.0f;
    private float timeLastPress = 0.0f;
    public float timeDelayThreshold = 1.0f;

    private bool multisilectEnable = false;

    //root of the object hierarchy
    public GameObject root;
    //public GameObject rotationPoint;

    private MultiSelectStore multiSelectStore;

    private MaterialController materialController;

    private ViewController viewController;

    
    private List<GameObject> allGameObjects = new List<GameObject>();

    private Camera cam;

    //to capture double tap
    private int tapCount = 0;
    public float doubleTapDelayThershold = 0.5f;
    private float firstTapTime = 0.0f;
    private float secondTimeTap = 0.0f;

    private GameObject viewPopup;


    void OnEnable()
    {

        Inital();
    }

    void Inital()
    {
        cam = GetComponent<Camera>();
        multiSelectStore = MultiSelectStore.Instance;
        allGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Object"));
        materialController = new MaterialController(allGameObjects);
        viewController = ViewController.Instance;
        viewController.initializeRotation(root);
        viewPopup = GameObject.Find("View Popup");
        viewPopup.SetActive(false);

    }

    private void Awake()
    {
        Inital();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        timePressed = Time.time;
                        break;
                    case TouchPhase.Ended:
                        endPos = touch.position;
                        timeLastPress = Time.time;

                        if (endPos == startPos)
                        {
                            tapCount++;
                            if(tapCount == 1){
                                firstTapTime = Time.time;
                            }else if(tapCount >=2){
                                secondTimeTap = Time.time;
                                if((secondTimeTap - firstTapTime)<= doubleTapDelayThershold){
                                    tapCount = 0;
                                    viewController.antRotation(root);
                                }else{
                                    tapCount = 1;
                                    firstTapTime = secondTimeTap;
                                }
                            }
                            if ((timeLastPress - timePressed) > timeDelayThreshold)
                            {
                                multisilectEnable = true;   //If loag press enable multiple selection option
                            }
                            //Select touched object
                            SelectObject(touch);
                        }
                        break;
                }
                
            }

        }
    }


    
    public void SelectObject(Touch touch)
    {
        Ray ray = cam.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (multiSelectStore.findObject(hitObject))
            {
                //if touch already selected object un select it
                multiSelectStore.removeObject(hitObject);
                materialController.removeMaterial(hitObject);
                if (SelectionMode == SelMode.AndChildren)
                {
                    List<GameObject> childrenRenderers = new List<GameObject>();
                    foreach (Transform child in hit.transform){
                        childrenRenderers.Add(child.gameObject);
                    }
                    childrenRenderers.ForEach(r =>
                    {
                        multiSelectStore.removeObject(r);
                        materialController.removeMaterial(r);
                    });
                }
            }
            else
            {
                if (multisilectEnable)
                {
                    // if multi select enabled and hit an object -> add it and its children to the multi select store and add selection material
                    multiSelectStore.addObject(hitObject);
                    materialController.addMaterial(hitObject,selectionMat);
                    if (SelectionMode == SelMode.AndChildren)
                    {
                        List<GameObject> childrenRenderers = new List<GameObject>();
                        foreach (Transform child in hit.transform){
                            childrenRenderers.Add(child.gameObject);
                        }
                        childrenRenderers.ForEach(r =>
                        {
                            multiSelectStore.addObject(r);
                            materialController.addMaterial(r,selectionMat);
                        });
                    }
                }
                else
                {
                    // if multiselect not enabled remove all selected objects from list and add newly touched object
                    multiSelectStore.removeAllObject();
                    materialController.removeMaterialOfAllObjects();
                    multiSelectStore.addObject(hitObject);
                    materialController.addMaterial(hitObject,selectionMat);
                    if (SelectionMode == SelMode.AndChildren)
                    {
                        List<GameObject> childrenRenderers = new List<GameObject>();
                        foreach (Transform child in hit.transform){
                            childrenRenderers.Add(child.gameObject);
                        }
                        childrenRenderers.ForEach(r =>
                        {
                            multiSelectStore.addObject(r);
                            materialController.addMaterial(r,selectionMat);
                        });
                    }
                }
            }

        }
        else
        {
            // if hit outside un select all
            if (multisilectEnable)
            {
                multiSelectStore.removeAllObject();
                materialController.removeMaterialOfAllObjects();
                multisilectEnable = false;
            }
            else
            {
                multiSelectStore.removeAllObject();
                materialController.removeMaterialOfAllObjects();
            }
        }
    }

}

