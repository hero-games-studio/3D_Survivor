using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    [SerializeField]
    Vector3 firstPos;
    GameObject parent;
    public static bool isFinished = false;
    public static bool isLost = false;
    private GameObject boat;
    private Vector3 boatPos;
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private float offset = 10f;
    private float distanceToBoat;
    private bool calledForHelp = false;
    private bool showedYourSadness = false;
    private bool pickedUp = false;
    // Start is called before the first frame update
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

        if(distanceToBoat < offset && boat.transform.position.z < transform.position.z && calledForHelp == false && !pickedUp){
            print(distanceToBoat);
            print("Help me!");
            particle.SetActive(true);
            calledForHelp = true;
        }
        if(boat.transform.position.z > transform.position.z && showedYourSadness == false){
            print("Sad");
            showedYourSadness = true;
        }

        if (isFinished || isLost)
        {
            transform.SetParent(parent.transform);
            transform.localPosition = firstPos;
            Invoke("setIsFinishedFalse", .5f);
        }
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
