using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivarSaver : MonoBehaviour
{
    [SerializeField]
    private GameObject ringPivot;
    private int savedSurvivors = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.parent != null){

            if(other.transform.parent.gameObject.tag == "survivor"){
                other.transform.parent.transform.SetParent(ringPivot.transform);
                other.transform.parent.transform.localPosition = new Vector3(0,0,0);
                other.transform.parent.transform.localEulerAngles = new Vector3(90 - savedSurvivors*60,90,90);
                other.transform.parent.tag = "saved";
                savedSurvivors++;
            }
        }
        
    }
    public void ResetCount(){
        savedSurvivors = 0;
    }

   
}
