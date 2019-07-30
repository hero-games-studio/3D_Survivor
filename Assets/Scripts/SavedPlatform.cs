using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPlatform : MonoBehaviour
{    
    float offsetX = 1f, offsetY = 1f;
    int layers;
    int maxCountForLayer = 4;
    [SerializeField]
    GameObject spawnPrefab;
    Survivor survivor;
    public void GetSavedSurvivorsToPlatform(int count, GameObject pivot){
        layers = count / maxCountForLayer;
        /* for(int i = 0; i < 7; i++){
            for(int j = 0; j < maxCountForLayer; j++){
                if(pivot.transform.childCount > 1 && pivot.transform.GetChild(1).tag == "saved"){
                    print("platform");
                    GameObject newObj = pivot.transform.GetChild(1).gameObject;
                    newObj.transform.parent = gameObject.transform;
                    newObj.transform.localPosition = new Vector3(offsetX * j, 1 , offsetY * i + 0.5f);
                    newObj.transform.eulerAngles = new Vector3(0,180,0);
                }
            }
        } */
        StartCoroutine(Timer(count,pivot));
    }
    IEnumerator Timer(int countTimer, GameObject pivotTimer){
        for(int i = 0; i < 7; i++){
            for(int j = 0; j < maxCountForLayer; j++){
                yield return new WaitForSeconds(0.1f);
                if(pivotTimer.transform.childCount > 1 && pivotTimer.transform.GetChild(1).tag == "saved"){
                    GameObject newObj = pivotTimer.transform.GetChild(1).gameObject;
                    newObj.transform.parent = gameObject.transform;
                    newObj.transform.localPosition = new Vector3(offsetX * j, 1 , offsetY * i + 0.5f);
                    newObj.transform.eulerAngles = new Vector3(0,180,0);
                }
            }
        }
    }
}
