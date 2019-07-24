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
    StageManager stageManager;
    UIManager uiManager;

    void Awake() {
        stageManager = StageManager.Instance;
        uiManager = UIManager.Instance;
    }
    
    void OnCollisionEnter(Collision other) {
        if(other.gameObject == boat){
            boatController.enabled = false;
            hook.transform.SetParent(null);
            boatRigidbody.useGravity = true;
            boatRigidbody.constraints = RigidbodyConstraints.None;
            stageManager.SetGameStarted(false);
            stageManager.SetGameFailed(true);
            stageManager.SetGameFinished(false);
            uiManager.SetInGamePanelActive(false);
            uiManager.SetOverLevelText("You Failed");
            uiManager.SetTapToText("Tap to Restart");
            boatController.RestartTimer();
        }
    }
}
