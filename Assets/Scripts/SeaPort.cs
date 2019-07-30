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
    StageManager stageManager;
    SavedPlatform savedPlatform;
    SurvivarSaver survivarSaver;

    void Awake() {
        uiManager = UIManager.Instance;  
        stageManager = StageManager.Instance;  
    }

    void Start(){
        boat = GameObject.FindGameObjectWithTag("boat");
        boatRigidbody = boat.GetComponent<Rigidbody>();
        boatController =  boat.GetComponent<BoatController>();
        savedPlatform = GameObject.FindGameObjectWithTag("platform").GetComponent<SavedPlatform>();
        survivarSaver = GameObject.FindGameObjectWithTag("boat").GetComponent<SurvivarSaver>();
    }
    void OnTriggerEnter(Collider other) {
       if(other.transform.parent.gameObject == boat){
            Survivor.isFinished = true;
            boatController.enabled = false;
            boatRigidbody.drag = 1f;
            boatRigidbody.angularDrag = 5f;
            uiManager.SetInGamePanelActive(false);
            uiManager.SetTapToText("Tap to Continue");
            stageManager.SetGameStarted(false);
            stageManager.SetGameFailed(false);
            stageManager.SetGameFinished(true);
            savedPlatform.GetSavedSurvivorsToPlatform(survivarSaver.GetCountSavedSurvivors(),survivarSaver.GetRingPivotObject());
        }
    }
}
