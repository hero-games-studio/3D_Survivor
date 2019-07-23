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
    void OnTriggerEnter(Collider other) {
       if(other.transform.parent.gameObject == boat){
            boatController.enabled = false;
            boatRigidbody.drag = 1f;
            boatRigidbody.angularDrag = 5f;
        }
    }
}
