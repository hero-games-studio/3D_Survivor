using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRePlacer : MonoBehaviour
{
    private float tileStep = 120f;
    private Vector3 firstPos = new Vector3(0.86f,0.11f,0);
    [SerializeField]
    private GameObject nextBlock;
    private void OnTriggerEnter(Collider other) {
        if(other.transform.parent.gameObject.tag == "boat"){
            nextBlock.transform.position = new Vector3(0,0,transform.position.z + tileStep); 
        }
    }
    public void ResetPosition(){
        if(gameObject.name == "0"){
            transform.position = firstPos;
            nextBlock.transform.position = firstPos + new Vector3(0,0,-120);
        }
    }
}
