using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class WallRunning : MonoBehaviour
{
    [Header("WallRunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("WallRemembering")]
    private string currWall = "NoCurrWall";
    private string lastWall = "NoLastWall";
    private bool touchedAWallAlready = false;

    [Header("References")]
    public Transform orientation;
    public Camera_RigidBody cam;
    private PlayerMovement playerMovement;
    private Rigidbody rb;
    public VisualEffect fastVFX;

    [Header("Exiting")]
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        fastVFX = fastVFX.GetComponent<VisualEffect>();
        
    }
    private void Start()
    {
        fastVFX.gameObject.SetActive(false);
    }
    private void Update()
    {
        CheckForWall();
        StateMachine();
        Debug.Log(currWall+ " " + lastWall);
    }
    private void FixedUpdate()
    {
        if (playerMovement.wallRunning)
        {
            WallRunningMovement();
        }

    }
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, whatIsWall);
        if (wallRight) currWall = "RightWall";
        else currWall = "LeftWall";

    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }
    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //State 1 : Wall Running
        if ((wallLeft || wallRight) && verticalInput > 0 && !playerMovement.grounded && !exitingWall)
        {
            if (currWall != lastWall)
            {
                if (!playerMovement.wallRunning)
                {

                    StartWallRun();
                    fastVFX.gameObject.SetActive(true);
                    touchedAWallAlready = true;
                    StartCoroutine("DecreaseGravityForce");
                    

                }
                if (wallRunTimer > 0)
                {
                    wallRunTimer -= Time.deltaTime;
                }
                if (wallRunTimer <= 0 && playerMovement.wallRunning)
                {
                    exitingWall = true;
                    exitWallTimer = exitWallTime;
                }


                if (Input.GetKeyDown(jumpKey))
                {
                    WallJump();
                }

            }
        }
        // State 2 : Exiting Wall
        else if (exitingWall)
        {
            if (playerMovement.wallRunning)
            {
                StopWallRun();
            }
            if (exitWallTimer > 0)
            {
                exitWallTimer -= Time.deltaTime;
            }
            if (exitWallTimer <= 0)
            {
                exitingWall = false;
            }
        }

        else
        {
            if (playerMovement.wallRunning)
            {
                StopWallRun();
            }
        }
    }

    private void StartWallRun()
    {
        playerMovement.wallRunning = true;

        wallRunTimer = maxWallRunTime;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //Apply Camera Effects
        cam.DoFov(90f);
        if (wallLeft) cam.DoTilt(-5);

        if (wallRight) cam.DoTilt(5);

    }
    private void WallRunningMovement()
    {
        rb.useGravity = useGravity;
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);

        }
        if (useGravity)
        {
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        }
    }

    private void StopWallRun()
    {
        playerMovement.wallRunning = false;
        rb.useGravity = true;
        cam.DoFov(60);
        cam.DoTilt(0f);
        lastWall = currWall;
        StartCoroutine("UnrememberWall");
        fastVFX.gameObject.SetActive(false);
    }

    private void WallJump()
    {
        //enter exiting wall State.
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        //reset y velocity and add force.
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
        StartCoroutine("UnrememberWall");
        fastVFX.gameObject.SetActive(false);

    }

    IEnumerator UnrememberWall()
    {
        yield return new WaitForSeconds(1f);
        currWall = "NoCurrWall";
        lastWall = "NoLastWall";
    }

    IEnumerator DecreaseGravityForce()
    {
        gravityCounterForce = 12;
        yield return new WaitForSeconds(0.3f);
        useGravity = false;
        yield return new WaitForSeconds(0.3f);
        useGravity = true;
        gravityCounterForce = 7;
    }
}

