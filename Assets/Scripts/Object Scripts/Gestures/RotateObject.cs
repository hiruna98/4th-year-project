using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public Camera cam;

    private bool rotating = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if(Physics.Raycast(ray, out hit)){
                            GameObject hitObject = hit.transform.gameObject;
                            if(hitObject.tag == "Object"){
                                rotating = true;
                            }
                        }
                        break;
                    case TouchPhase.Moved:
                        if(rotating){
                            transform.Rotate(-touch.deltaPosition.y,-touch.deltaPosition.x,0f, Space.Self);
                        }
                        break;
                    case TouchPhase.Ended:
                        rotating = false;
                        break;
                }
                
            }
        }   
    }
}
