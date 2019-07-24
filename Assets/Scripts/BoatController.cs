using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 0.1f;
    [SerializeField]
    private float clampValue = 4.0f;
    [SerializeField]
    private Rigidbody boatRigidBody;

    private bool timerStarted = false;
    [SerializeField]
    private GameObject whalePrefab;
    private int velocityZ;



    private Vector3 targetPosition;
    [SerializeField]
    private Vector3 targetOffset = new Vector3(0,0,4);
    private Vector3 newDirection;
    UIManager uiManager;

 
    void Awake(){
        uiManager = UIManager.Instance;
    }
    void Update() {
        uiManager.SetCurrentPosition(transform.position.z);
        SetMaxPosition(transform.position);
        GetDirection();
        IsBoatMoving();
    }
    void FixedUpdate() {
        SetDirection();
    }

    void GetDirection()
    {
        targetPosition = new Vector3(0,0,transform.position.z) + targetOffset;
        if(Input.touchCount == 1)
        {
            Touch newTouch = Input.GetTouch(0);
           
            if (newTouch.phase == TouchPhase.Moved)
            {
                Vector3 localPosition = transform.position;
                targetOffset.x += newTouch.deltaPosition.x * 0.02f;
                targetOffset.x = Mathf.Clamp(targetOffset.x,-clampValue,clampValue);
            }
        }
            newDirection = targetPosition - transform.position;
    }

    void SetMaxPosition(Vector3 position)
    {
        position.x = Mathf.Clamp( position.x, -clampValue, clampValue);
        transform.position = position;
    }

    void SetDirection()
    {
        transform.forward = Vector3.Lerp(transform.forward, newDirection, rotationSpeed);
        
        boatRigidBody.velocity = transform.forward * speed;
        boatRigidBody.angularVelocity = Vector3.zero;
    }

    public void SetKinematic(bool isKinematic){
        boatRigidBody.isKinematic = isKinematic;
    }
    public void SetGravity(bool useGravity){
        boatRigidBody.useGravity = useGravity;
    }
    public void ResetConstraints(){
        boatRigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }


    void IsBoatMoving(){
        velocityZ = Mathf.RoundToInt(boatRigidBody.velocity.z);
        print(velocityZ);
        if(velocityZ < 7 && !timerStarted){
            timerStarted = true;
            StartCoroutine(Timer());
        }else if(velocityZ > 7 && timerStarted){
            StopCoroutine(Timer());
        }

    }
    public void RestartTimer(){
        timerStarted = false;
        StopCoroutine(Timer());
    }

    IEnumerator Timer(){
        print("Timer Started");
        yield return new WaitForSeconds(3);
        Vector3 offset = transform.position + new Vector3(0,10,0);
        whalePrefab.SetActive(true);
        whalePrefab.transform.position = offset;
        whalePrefab.transform.localEulerAngles = new Vector3(-100,-90,0);
    }


#region Backup
/*     
    private Vector3 firstInteractionPos;
    private Vector3 lastInteractionPos;

    private Vector3 targetDirection;
    private Vector3 currentDirection;
    [SerializeField]
    private float clampValue = 4.0f;

    public float speed = 5f;
    public float rotationSpeed = 0.001f;
    [SerializeField]
    private Rigidbody boatRigidBody;
    [SerializeField]
    private GameObject shovel;
    private float boatRotationY;
    private void Start() {
        targetDirection = new Vector3(0,0,1);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstInteractionPos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            lastInteractionPos = GetMouseLastPos();
            targetDirection = GetDirection(lastInteractionPos - firstInteractionPos);
        }
        ChangeDirection();
        boatRigidBody.velocity = transform.forward * speed;
        SetMaxPosition(transform.position);

    }

    
    void ChangeDirection(){
        if(targetDirection != Vector3.zero){
            currentDirection = Vector3.Lerp(currentDirection,targetDirection,rotationSpeed * Time.deltaTime);
            currentDirection.z = 8;
            transform.forward = currentDirection;
            boatRotationY = transform.rotation.y;
            shovel.transform.eulerAngles = new Vector3(-90,-90 - boatRotationY*5,-0);
            print(currentDirection);
        }
    }

    Vector3 GetMouseLastPos(){
        return Input.mousePosition;
    }
    Vector3 GetDirection(Vector3 directionArg){
        directionArg = Vector3.ClampMagnitude(directionArg,8);
        directionArg.y = Mathf.Clamp(targetDirection.y, 0.75f, 1);
        directionArg.z = directionArg.y;
        directionArg.y = 0;
        return directionArg;
    }

    void SetMaxPosition(Vector3 position){
        position.x = Mathf.Clamp( position.x, -clampValue, clampValue);
        transform.position = position;
    }
 */
 #endregion
}
