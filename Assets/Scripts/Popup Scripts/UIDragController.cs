using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    private UIRotateController uIRotateController;

    private bool rotationEnable = true;

    public void dragHandler(BaseEventData data){
        uIRotateController = UIRotateController.Instance;
        PointerEventData pointerData = (PointerEventData)data;

        Vector3 position;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position
        );

        transform.position = canvas.transform.TransformPoint(position.x,position.y,0);
        //uIRotateController.Rotate(transform);
    }

    public void changeRotationEnable(){
        if(rotationEnable){
            rotationEnable = false;
        }else{
            rotationEnable = true;
        }
    }
}
