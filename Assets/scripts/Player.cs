using System;
using UnityEngine;




public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public Transform targetTransform;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    public float speed = 5;
    public float jumpSpeed = 10f;
    //bool isGrounded = false;
    public float gravity = -9f;
    //bool isJumping = false;
    Vector3 playerVelocity;
    bool isGrounded = false;
    bool enemyInRadius = false;
    public int playerHealth;

    public enemy Enemy;
    GameObject targetEnemy;

    public bool isDead = false;
    public int playerDamage = 100;
    public int score = 0;
    public int playerMaxHealth = 200;
    
    public float rayCastOffset = 1f;
    Vector3 raycastForwardDir = new Vector3(0, 0, 0);
    Vector3 raycastForwardOffset = new Vector3(0,1,0);

    public enemySpawn EnemySpawn;




    void Start()
    {
        
        playerHealth = 200;
    }
    void Update()
    {
        PlayerUpdate();
        DoGravity();
        RaycastIsGrounded();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        //RaycastForward();
        //controller.velocity.y += gravity;
    }
   
    void PlayerUpdate()
    {
        print(playerHealth);

        if (isDead == false)
        {
            
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(h, 0, v).normalized;
            float yvel = playerVelocity.y;
            if (h > 0 || h < 0 || v > 0 || v < 0)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isPunching", false);
            }
            else
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);
            }



            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                anim.SetBool("isJumping", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown("f") && anim.GetBool("isJumping") == false)
            {
                punch();
            }



            if (playerVelocity.y > 0)
            {
                anim.SetBool("isRunning", false);
            }

            if (isGrounded == true && playerVelocity.y < 0)
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
                //raycastForwardDir =

                Vector3 vel = moveDir.normalized * speed;

                playerVelocity.x = vel.x;
                playerVelocity.z = vel.z;

                //rb.linearVelocity = new Vector3( moveDir.x*speed, yvel, moveDir.z*speed);
            }
            if (playerHealth <= 0)
            {
                isDead = true;
            }
            if (anim.GetBool("isPunching") == true)
            {
                if (targetTransform != null)
                {
                    gameObject.transform.LookAt(targetTransform);
                }
                else
                {
                    print("looking at null target");
                }
                return;
            }
            if (playerHealth > 200)
            {
                playerHealth = 200;
            }


        }
        else
        {
            playerVelocity = new Vector3(0,0,0);
            anim.SetBool("isDead", true);
            anim.SetBool("isJumping", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isPunching", false);
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
    private bool RaycastForward()
    {
        bool HitEnemy = false;
        Vector3 offset = raycastForwardDir + raycastForwardOffset;
        var ray = new Ray(transform.position + offset, Vector3.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1) == true)
        {
            if (hitInfo.collider.CompareTag("enemy"))
            {
                HitEnemy = true;
            }
            
        }
        else
        {
            HitEnemy = false;

        }
        Debug.DrawRay(transform.position + offset, Vector3.forward, Color.black);
        return HitEnemy;
    }
    */
    void StartJumpUp()
    {
        playerVelocity.y = jumpSpeed - gravity * Time.deltaTime;
    }
    void RemoveXZvel()
    {
        playerVelocity.x = 0;
        playerVelocity.z = 0;
    }
    void StopJump()
    {
        anim.SetBool("isJumping", false);
        
    }
    void EndPunch()
    {
        anim.SetBool("isPunching", false);
    }

    void punch()
    {
        if (enemyInRadius == true)
        {
            anim.SetBool("isPunching", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            
        }
        /*
        if (RaycastForward() == true)
        {
            anim.SetBool("isPunching", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);

        }
        */
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            if (col.gameObject.GetComponent<enemy>().enemyHealth > 0)
            {
                enemyInRadius = true;
                targetTransform = col.gameObject.transform;
                targetEnemy = col.gameObject;
            }
            
        }
        else
        {
            enemyInRadius = false;
        }

    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            if (targetEnemy != null)
            {
                if (targetEnemy.GetComponent<enemy>().enemyHealth <= 0)
                {
                    targetTransform = null;
                    targetEnemy = null;
                    enemyInRadius = false;
                }
            }
            if ((targetEnemy == null || targetEnemy.GetComponent<enemy>().enemyHealth <= 0) && col.GetComponent<enemy>().enemyHealth > 0)
            {
                targetEnemy = col.gameObject;
                targetTransform = col.gameObject.transform;
                enemyInRadius = true;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {

            targetTransform = null;
            targetEnemy = null;
            enemyInRadius = false;
        }
        

    }
    void DoDamagePlayer()
    {
        print("trying to do damge");
        if (enemyInRadius == true)
        {
            if (targetEnemy != null )
            {
                targetEnemy.GetComponent<enemy>().enemyHealth -= playerDamage;
                //Enemy.enemyHealth -= playerDamage;
                print(Enemy.enemyHealth);
            }
            else
            {
                print("no traget");
            }
            
        }
    }

    private void OnGUI()
    {
        
        if (isDead)
        {
            //debug text
            string deathText = "YOU DIED!";

            // define debug text area
            GUILayout.BeginArea(new Rect(250f, 250f, 5000f, 5000f));
            GUILayout.Label($"<size=100>{deathText}</size>");
            GUILayout.EndArea();
        }
        else
        {
            //debug text
            string text = "HEALTH = " + playerHealth;
            text += "\nSCORE = " + score;
            text += "\nTIME UNTIL WAVE = " + EnemySpawn.spawnTimer;
            text += "\nWAVE = " + EnemySpawn.waveCounter;


            // define debug text area
            GUILayout.BeginArea(new Rect(10f, 450f, 1600f, 1600f));
            GUILayout.Label($"<size=16>{text}</size>");
            GUILayout.EndArea();
        }
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
