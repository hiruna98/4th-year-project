using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEventHandler : MonoBehaviour
{
    //root object
    private GameObject obj; 

    private MultiSelectStore multiSelectStore;
    private IsolationController isolationController;
    private ViewController viewController;

    private GameObject viewPopup;
    private void Awake()
    {
        obj = GameObject.Find("root_object");
        multiSelectStore = MultiSelectStore.Instance;
        viewController = ViewController.Instance;
        isolationController = new IsolationController();
        viewPopup = GameObject.Find("View Popup");
    }
    public void onIsolateBtnClick()
    {
        isolationController.IsolateObjects(multiSelectStore.getSelectedObjects());
    }

    public void onUndoBtnClick()
    {
        isolationController.undo();
    }

    public void onRedoBtnClick()
    {
        isolationController.redo();
    }

    public void onViewPopupOpenClick(){
        string prefName = viewPopup.name + "DefaultZ";
        PlayerPrefs.SetInt(prefName,0);
        PlayerPrefs.Save();
        viewPopup.SetActive(true);
    }

    public void onViewPopupCloseClick(){
        viewPopup.SetActive(false);
    }

    public void onCenterClicked(){
        viewController.centerPos(obj);
    }

    public void onAnteriorClicked(){
        viewController.antRotation(obj);
    }

    public void onPosteriorClicked(){
        viewController.posRotation(obj);
    }

    public void onLateralClicked(){
        viewController.latRotation(obj);
    }

    public void onMedialClicked(){
        viewController.medRotation(obj);
    }

    public void onSuperiorClicked(){
        viewController.supRotation(obj);
    }

    public void onInferiorClicked(){
        viewController.infRotation(obj);
    }
}
