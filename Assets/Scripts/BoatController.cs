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
    private bool firstTouch = false;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Rigidbody boatRigidBody;

    private int currentMagnetude = 0;
    private float lastMagnetude = -1;
    private bool timerStarted = false;
    [SerializeField]
    private GameObject whalePrefab;



    private Vector3 targetPosition;
    [SerializeField]
    private Vector3 targetOffset = new Vector3(0,0,4);
    [SerializeField]
    private Vector3 nutralTargetOffset = new Vector3(0,0,4);
    private Vector3 touchPosition;
    private float touchPositionX;
    private Vector3 newDirection;
    UIManager uiManager;
 
    void Awake(){
        uiManager = UIManager.Instance;
    }
    void Update() {
        uiManager.SetCurrentPosition(transform.position.z);
        SetMaxPosition(transform.position);
        GetDirection();
        SetDirection();
        IsBoatMoving();
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

    void IsBoatMoving(){
        currentMagnetude = Mathf.RoundToInt(transform.position.magnitude);
        //print("Current Magnetude: " +currentMagnetude + "| Last Magnetude: " + lastMagnetude );
        int tempMagnetude = Mathf.RoundToInt(lastMagnetude);
        if(currentMagnetude > lastMagnetude && !timerStarted){
            lastMagnetude = Mathf.MoveTowards(lastMagnetude,currentMagnetude,0.2f);
            StopCoroutine(Timer());
        }
        if(lastMagnetude == currentMagnetude && !timerStarted){
            timerStarted = true;
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer(){
        print("Timer Started");
        yield return new WaitForSeconds(1);
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
