using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 0.001f;
    [SerializeField]
    private float clampValue = 4.0f;
    Vector3 firstPos;
    Vector3 secondPos;
    Vector3 clampedFirstPos;
    Vector3 clampedSecondPos;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private bool firstTouch = false;
    [SerializeField]
    private Camera mainCamera;
    private float modifiedX, modifiedY, modifiedZ;
    [SerializeField]
    private Rigidbody boatRigidBody;

    private float firstPosX, secondPosX;
    private float clampedFirstPosX,clampedSecondPosX;
    private Vector3 directionX;

    private int currentMagnetude = 0;
    private float lastMagnetude = -1;
    private bool timerStarted = false;
    [SerializeField]
    private GameObject dropPrefab;
    [SerializeField]
    private GameObject whalePrefab;

    
    private LineRenderer line;
    public Material material;
 
    void Update() {
        SetMaxPosition(transform.position);
        GetDirection();
        SetDirection();
        IsBoatMoving();
    }

    void GetDirection()
    {
        if(Input.touchCount == 1)
        {
            Touch newTouch = Input.GetTouch(0);
            if (newTouch.phase == TouchPhase.Began)
            {
                if (line == null)
                {
                    createLine();
                }
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(newTouch.position);
                if (Physics.Raycast(ray,out hit) && firstTouch == false)
                {
                    firstPos = hit.point;
                    clampedFirstPos = hit.transform.InverseTransformPoint(firstPos);

                    secondPos = hit.point;

                    line.SetPosition(0, firstPos);
                    line.SetPosition(1, secondPos);



                    firstPosX = hit.point.x;
                    clampedFirstPosX = hit.transform.InverseTransformPoint(firstPos).x;
                    
                    secondPosX = hit.point.x;

                    firstTouch = true;
                }
                firstPos.x = transform.position.x;
            }
            if (newTouch.phase == TouchPhase.Moved)
            {
                RaycastHit hit2;
                Ray ray2 = mainCamera.ScreenPointToRay(newTouch.position);
                
                if (Physics.Raycast(ray2, out hit2) && firstTouch == true)
                {
                    /* secondPos = hit2.point;
                    clampedSecondPos = hit2.transform.InverseTransformPoint(secondPos);

                    Vector3 offset;
                    offset = clampedSecondPos - clampedFirstPos;
                    direction = Vector3.ClampMagnitude(offset,3.0f);
                    
                    modifiedX = Mathf.Round(direction.x * 100) / 100;
                    // modifiedY = Mathf.Round(direction.y * 100) / 100;
                    // modifiedZ = Mathf.Round(direction.z * 100) / 100;
                    direction = new Vector3(modifiedX,0,0);
                    if (direction.y <= 0)
                    {
                        direction.y = 0;
                    }
                    line.SetPosition(1, secondPos); */


                    secondPosX = hit2.point.x;
                    clampedSecondPosX = Mathf.Clamp(secondPosX,-1.5f,1.5f);

                    float offsetX;
                    offsetX = clampedSecondPosX - clampedFirstPosX;
                    directionX = Vector3.ClampMagnitude(new Vector3(offsetX,0,0),3.0f);

                    direction += directionX;
                    if (direction.y <= 0)
                    {
                        direction.y = 0;
                    }
                    line.SetPosition(1, secondPos);


                }
            }
            if(newTouch.phase == TouchPhase.Stationary){

                RaycastHit hit2;
                Ray ray2 = mainCamera.ScreenPointToRay(newTouch.position);
                
                if (Physics.Raycast(ray2, out hit2) && firstTouch == true)
                {
                    /* secondPos = hit2.point;
                    clampedSecondPos = hit2.transform.InverseTransformPoint(secondPos);
                    Vector3 offset;
                    offset = clampedSecondPos - clampedFirstPos;
                    direction = Vector3.ClampMagnitude(offset,3.0f);
                    
                    modifiedX = Mathf.Round(direction.x * 100) / 100;
                    // modifiedY = Mathf.Round(direction.y * 100) / 100;
                    // modifiedZ = Mathf.Round(direction.z * 100) / 100;
                    direction = new Vector3(modifiedX,0,0);
                    if (direction.y <= 0)
                    {
                        direction.y = 0;
                    }
                    line.SetPosition(0, firstPos);
                    line.SetPosition(1, secondPos); */

                    secondPosX = hit2.point.x;
                    clampedSecondPosX = Mathf.Clamp(secondPosX,-1.5f,1.5f);

                    float offsetX;
                    offsetX = clampedSecondPosX - clampedFirstPosX;
                    directionX = Vector3.ClampMagnitude(new Vector3(offsetX,0,0),3.0f);

                    direction += directionX;
                    if (direction.y <= 0)
                    {
                        direction.y = 0;
                    }
                    line.SetPosition(1, secondPos);

                }
            }
            if (newTouch.phase == TouchPhase.Ended)
            {
                firstTouch = false;
            }
        }
    }

    void SetMaxPosition(Vector3 position){
        position.x = Mathf.Clamp( position.x, -clampValue, clampValue);
        transform.position = position;
    }
    private void createLine()
    {
        //create a new empty gameobject and line renderer component
        line = new GameObject("Line").AddComponent<LineRenderer>();
        //assign the material to the line
        line.material = material;
        //set the number of points to the line
        line.SetVertexCount(2);
        //set the width
        line.SetWidth(0.15f, 0.15f);
        //render line to the world origin and not to the object's position
        line.useWorldSpace = true;

    }

    void SetDirection(){

        //direction.z = direction.y;
        direction.x = direction.x/2;
        direction.z = Mathf.Clamp(direction.z,0.75f,1.5f);
        //print("Direction X: " +  directionX + " | Position X: " + transform.position.x);
        if((direction.x > 0 && transform.position.x >= clampValue) || (direction.x < 0 && transform.position.x <= -clampValue))
        {
            /* direction.x = 0;
            firstPos.x = transform.position.x;
            firstPos.z = transform.position.z - 20; */

            direction.x = 0;
            directionX.x = 0;
            firstPosX = 0;
        }

        if (direction.x < 0.5f && direction.x > -0.5f)
        {
            rotationSpeed = 0.2f;
        }
        else if (direction.x < 0.8f && direction.x > -0.8f)
        {
            rotationSpeed = 0.3f;
        }else
        {
            rotationSpeed = 0.6f;
        }

        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed);

        boatRigidBody.velocity = transform.forward * speed;
        boatRigidBody.angularVelocity = Vector3.zero;


    }

    void IsBoatMoving(){
        currentMagnetude = Mathf.RoundToInt(transform.position.magnitude);
        print("Current Magnetude: " +currentMagnetude + "| Last Magnetude: " + lastMagnetude );
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
