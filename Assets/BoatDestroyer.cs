using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject boat;
    [SerializeField]
    private GameObject hook;
    [SerializeField]
    private Rigidbody boatRigidbody;
    [SerializeField]
    private BoatController boatController;
    
    void OnCollisionEnter(Collision other) {
        print(other.gameObject);
        if(other.gameObject == boat){
            boatController.enabled = false;
            hook.transform.SetParent(null);
            boatRigidbody.useGravity = true;
            boatRigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
