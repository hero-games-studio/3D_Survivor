using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    [SerializeField]
    Vector3 firstPos;
    [SerializeField]
    GameObject parent;
    public static bool isFinished = false;
    public static bool isLost = false;
    public static bool clickedToContinue = false;
    public bool isFinishedApplied = false;
    private GameObject boat;
    private Vector3 boatPos;
    [SerializeField]
    private GameObject HelpObj;
    [SerializeField]
    private GameObject SmileyObj;
    [SerializeField]
    private float offset = 10f;
    private float distanceToBoat;
    private bool calledForHelp = false;
    private bool showedYourSadness = false;
    private bool pickedUp = false;
    StageManager stageManager;
    // Start is called before the first frame update

    void Awake(){
        stageManager = StageManager.Instance;
    }
    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("boat");
        firstPos = transform.localPosition;
        parent = transform.parent.gameObject;
        isFinished=false;
    }
    
    // Update is called once per frame
    void Update()
    {
        boatPos = boat.transform.position;
        distanceToBoat = Vector3.Distance(boatPos,transform.position);

        if(distanceToBoat < offset && boat.transform.position.z < transform.position.z && !calledForHelp && !pickedUp && !isFinished){
            print(distanceToBoat);
            print("Help me!");
            HelpObj.SetActive(true);
            calledForHelp = true;
        }
        if(boat.transform.position.z > transform.position.z && showedYourSadness == false){
            print("Sad");
            showedYourSadness = true;
        }

        if (isLost)
        {
            transform.SetParent(parent.transform);
            transform.localPosition = firstPos;
            Invoke("setIsFinishedFalse", .5f);
        }

        if(isFinished && !isFinishedApplied && !clickedToContinue){
            OnGameFinish();
        }

    }
    public void OnGameFinish(){
            transform.SetParent(parent.transform);
            transform.localPosition = firstPos;
            Invoke("setIsFinishedFalse", .5f);
            transform.gameObject.tag = "survivor";
            isFinishedApplied = true;
    }

    void setIsFinishedFalse()
    {
        isFinished = false;
        isLost = false;
    }

    public void PickUp(){
        pickedUp = true;
    }
}
