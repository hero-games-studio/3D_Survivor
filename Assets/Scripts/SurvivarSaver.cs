using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivarSaver : MonoBehaviour
{
    [SerializeField]
    private GameObject ringPivot;
    private int savedSurvivors = 0;
    private float radiusMultiplier = 0;
    private int cycle = 6;
    private int counter = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.parent != null){

            if(other.transform.parent.gameObject.tag == "survivor"){
                savedSurvivors++;
                other.transform.parent.transform.SetParent(ringPivot.transform);
                print("Multipier: " + radiusMultiplier + "| saved: " + savedSurvivors);
                other.transform.parent.transform.localPosition = new Vector3(0,0,0);
                other.transform.localPosition += new Vector3(0,0,radiusMultiplier);
                other.transform.parent.transform.localEulerAngles = new Vector3(90 - (savedSurvivors-1)*60,90,90);
                other.transform.parent.tag = "saved";
                if(savedSurvivors % cycle == 0 ){
                    radiusMultiplier = -0.3f * counter;
                    counter++;
                }
            }
        }
        
    }
    public void ResetCount(){
        savedSurvivors = 0;
        radiusMultiplier = 0;
        counter = 1;
    }

   
}
