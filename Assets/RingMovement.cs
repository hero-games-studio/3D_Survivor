using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMovement : MonoBehaviour
{    
    private GameObject target;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float deadZone = 3f;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float deadAngle = 45f;
    private float distance;
    private Vector3 direction;

    void Start(){
        target = GameObject.FindGameObjectWithTag("offsetRing");
    }
    void Update()
    {
        distance = Vector3.Distance(target.transform.position,transform.position);
        direction = target.transform.position - transform.position;
    }
    void FixedUpdate() {
        transform.forward = Vector3.Lerp(transform.forward, direction, 0.5f * Time.deltaTime);
        if(distance > deadZone){
            print("Saved Distance: " + distance);
            rb.velocity = direction.normalized * distance * 4;
        }
    }
}
