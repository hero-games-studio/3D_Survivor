using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaPort : MonoBehaviour
{
    [SerializeField]
    private GameObject boat;
    [SerializeField]
    private Rigidbody boatRigidbody;
    [SerializeField]
    private BoatController boatController;
    [SerializeField]
    UIManager uiManager;

    void Awake() {
        uiManager = UIManager.Instance;    
    }

    void Start(){
        boat = GameObject.FindGameObjectWithTag("boat");
        boatRigidbody = boat.GetComponent<Rigidbody>();
        boatController =  boat.GetComponent<BoatController>();
    }
    void OnTriggerEnter(Collider other) {
       if(other.transform.parent.gameObject == boat){
            boatController.enabled = false;
            boatRigidbody.drag = 1f;
            boatRigidbody.angularDrag = 5f;
            uiManager.SetInGamePanelActive(false);
            uiManager.SetTapToText("Tap to Continue");
        }
    }
}
