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
    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.localPosition;
        parent = transform.parent.gameObject;
        isFinished=false;
    }

    // Update is called once per frame
    void Update()
    {
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
}
