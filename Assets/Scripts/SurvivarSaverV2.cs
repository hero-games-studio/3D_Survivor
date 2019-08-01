using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SurvivarSaverV2 : MonoBehaviour
{
    [SerializeField]
    private GameObject ringPrefab;
    /* [SerializeField]
    private GameObject smileyObject; */
    private int savedSurvivors = 0;
    private float radiusMultiplier = 0;
    private int cycle = 6;
    private int counter = 1;
    void Update(){

    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.parent != null){
            if(other.transform.parent.gameObject.tag == "survivor"){
                Survivor localSurvivor = other.transform.parent.GetComponent<Survivor>();
                if(localSurvivor.ringThrown == false){
                    GameObject savingRing = Instantiate(ringPrefab,transform.position,Quaternion.identity);
                    Jumper jumper = savingRing.GetComponent<Jumper>();
                    jumper.UpdateLastPos(other.transform.position);
                    jumper.SetNewChild(other.transform.parent.transform);
                    savingRing.SetActive(true);
                    savedSurvivors++;
                    localSurvivor.ringThrown = true;
                }
                /*other.transform.parent.gameObject.GetComponent<Survivor>().PickUp();
                if(smileyObject.gameObject.activeInHierarchy == false){
                    smileyObject.SetActive(true);
                } */
                /* other.transform.parent.transform.SetParent(ringPrefab.transform.GetChild(0).transform);
                other.transform.parent.transform.localPosition = new Vector3(0,0,0);
                other.transform.localPosition += new Vector3(0,0,radiusMultiplier);
                other.transform.parent.transform.localEulerAngles = new Vector3(90 - (savedSurvivors-1)*60,90,90);
                other.transform.parent.tag = "saved";
                if(savedSurvivors % cycle == 0 ){
                    radiusMultiplier = -0.3f * counter;
                    counter++;
                } */
            }
        }
        
    }
    
    public void ResetCount(){
        savedSurvivors = 0;
        radiusMultiplier = 0;
        counter = 1;
    }

    public int GetCountSavedSurvivors(){
        return savedSurvivors;
    }

    public GameObject GetRingPivotObject(){
        return ringPrefab;
    }
   
}
