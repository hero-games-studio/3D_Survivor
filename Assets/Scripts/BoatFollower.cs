using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFollower : MonoBehaviour
{
    [SerializeField]
    private Transform boatPosition;
    private Vector3 targetPosition;
    [SerializeField]
    private float maxSpeed = 20f;
    [SerializeField]
    private float timeToArive = 0.1f;
    private float targetPositionZ;

    private float velocity = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        targetPositionZ = Mathf.SmoothDamp(transform.position.z,boatPosition.position.z,ref velocity,timeToArive,maxSpeed,Time.deltaTime);
        targetPosition = new Vector3(transform.position.x,transform.position.y,targetPositionZ);
        transform.position =  targetPosition;
    }
}
