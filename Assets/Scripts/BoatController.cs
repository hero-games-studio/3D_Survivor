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



    
    private LineRenderer line;
    public Material material;
 
    void FixedUpdate() {
        SetMaxPosition(transform.position);
        GetDirection();
        SetDirection();
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
                    secondPos = hit2.point;
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
                    line.SetPosition(1, secondPos);
                }
            }
            if(newTouch.phase == TouchPhase.Stationary){

                RaycastHit hit2;
                Ray ray2 = mainCamera.ScreenPointToRay(newTouch.position);
                
                if (Physics.Raycast(ray2, out hit2) && firstTouch == true)
                {
                    secondPos = hit2.point;
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
                    line.SetPosition(1, secondPos);
                }
                CheckSidePosition();
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

    void CheckSidePosition(){
        print("Direction X: " +  direction.x + " | Position X: " + transform.position.x);
        if((direction.x > 0 && transform.position.x >= clampValue) || (direction.x < 0 && transform.position.x <= -clampValue))
        {
            direction.x = 0;
            firstPos.x = transform.position.x;
            firstPos.z = transform.position.z - 20;
        }
    }
    void SetDirection(){

        //direction.z = direction.y;
        direction.x = direction.x/2;
        direction.z = Mathf.Clamp(direction.z,0.75f,1.5f);
        

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
