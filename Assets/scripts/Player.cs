using System;
using UnityEngine;



public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
   

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    public float speed = 5;
    public float jumpSpeed = 10f;
    //bool isGrounded = false;
    public float gravity = -9f;
    bool isJumping = false;
    Vector3 playerVelocity;
    bool isGrounded = false;
    
    public float rayCastOffset = 1f;



    void Start()
    {

    }
    void Update()
    {
        PlayerUpdate();
        DoGravity();
        RaycastIsGrounded();
        print(isGrounded);
        //controller.velocity.y += gravity;
    }
   
    void PlayerUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0, v).normalized;
        float yvel = controller.velocity.y;
        
        if (h > 0 || h < 0 || v > 0 || v < 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }
        if (yvel > 0.1 && isJumping == true)
        {

            anim.SetBool("isJumping", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", false);


        }
        else
        {
            anim.SetBool("isJumping", false);
        }


        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            isJumping = true;
            playerVelocity.y = jumpSpeed - gravity * Time.deltaTime;
        }
        if (isJumping == true && controller.velocity.y < 0)
        {
            isJumping = false;
        }
        if (isGrounded == true)
        {
            playerVelocity.y = 0;
        }
        if (anim.GetBool("isIdle") == true)
        {
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            Vector3 vel = moveDir.normalized * speed;

            playerVelocity.x = vel.x;
            playerVelocity.z = vel.z;

            //rb.linearVelocity = new Vector3( moveDir.x*speed, yvel, moveDir.z*speed);
        }


        
    }

    void DoGravity()
    {
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    private void RaycastIsGrounded()
    {
        Vector3 offset = new Vector3(0, rayCastOffset, 0);
        var ray = new Ray(transform.position + offset, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1) == true)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;

        }
        Debug.DrawRay(transform.position + offset, Vector3.down, Color.red);
    }


















    /*
    private void Raycast()
    {
        Vector3 offset = new Vector3(0, -1.74f, 0);
        var ray = new Ray(transform.position + offset, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5f))
        {
            //isGrounded = true;
            //isRayColliding = true;
            if (hitInfo.collider.gameObject.tag == "slope" && rb.linearVelocity.y < 0)
            {
                //isRaySlope = true;
            }
            else
            {
                //isRaySlope = false;
            }

            if (hitInfo.collider.gameObject.layer == 6)
            {
                // isGrounded = true;
            }
            else
            {
                // isGrounded = false;
            }

        }
        else
        {
            //isGrounded = false;
            //isRayColliding = false;
        }
        Debug.DrawRay(transform.position + offset, Vector3.down, Color.red);
    }
    */



}
