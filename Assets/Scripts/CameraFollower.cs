using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    Camera mainCamera;
    void Start(){
        mainCamera = Camera.main;
    }
    void Update() {
        transform.LookAt(mainCamera.transform.position, Vector3.up);
    }
}
