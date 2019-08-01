using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jumper : MonoBehaviour
{    
    [SerializeField]
    Vector3 firstPos,lastPos;
    Vector3 heightVector;
    [SerializeField]
    float height = 5f;
    Vector3 direction;
    private Transform newChild;
    float time;
    IEnumerator c1;

    [Range(0.0f,1.0f)]
    [SerializeField]
    float currentPos;
    Survivor survivor;
    public void UpdateLastPos(Vector3 newPos){
        lastPos = newPos;
        lastPos.y = 0.25f;
    }
    void Start(){
        firstPos = transform.position;
        direction = lastPos - firstPos;
        heightVector = firstPos + new Vector3(direction.x/2,height,direction.z/2);
        c1 = UpdatePos();
        StartCoroutine(c1);
        survivor = newChild.GetComponent<Survivor>();
    }
    public void SetNewChild(Transform child){
        newChild = child;
    }
    void ChangeParent(){
        survivor.SetNewParent(transform);
        GetComponent<Jumper>().enabled = false;
        GetComponent<RingMovement>().enabled = true;
    }
    IEnumerator UpdatePos(){
        while(transform.position != lastPos){
            transform.position = CalculateQuadraticBezierPoint(currentPos,firstPos,heightVector,lastPos);
            yield return new WaitForFixedUpdate();
        }
    }
    Vector3 CalculateQuadraticBezierPoint(float t,Vector3 p0, Vector3 p1, Vector3 p2){
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}
