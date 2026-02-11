using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    
    private NavMeshAgent agent;
    bool isFollowing = true;
    bool isAttacking = false;
    bool isIdle = false;
    bool playerInRadius = false;
    float attackTimer = 4;
    public Animator anim;
    float idleCounter = 4;
    

    public int enemyHealth = 100;
    bool isDead = false;
    float deathTimer = 3;
    public int enemyDamage = 20;

    Player plr;
    Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth = 100;
        agent = GetComponent<NavMeshAgent>();
        plr = GameObject.Find("player").GetComponent<Player>();
        player = GameObject.Find("player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        print(enemyHealth);
        if (isDead == false)
        {
            if (isAttacking)
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);

            }

            if (isIdle)
            {
                idleCounter = 0;
                anim.SetBool("isIdle", true);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isRunning", false);
                idleCounter -= Time.deltaTime;
                if (idleCounter <= 0)
                {
                    isIdle = false;
                    isFollowing = true;
                }
            }
            if (isFollowing)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isIdle", false);
                agent.destination = player.position;
            }
            if (playerInRadius == false)
            {
                attackTimer = 0;
                isIdle = false;
                isAttacking = false;
                isFollowing = true;
            }
            if (playerInRadius == true)
            {
                attackTimer -= Time.deltaTime;
                isIdle = true;
                isAttacking = false;
                isFollowing = false;
            }
            if (playerInRadius == true && attackTimer <= 0 && plr.isDead == false)
            {
                isAttacking = true;
                isIdle = false;
                isFollowing = false;
            }
            /*
            print("enemy touching player" + playerInRadius);
            print("following" + isFollowing);
            print("attacking" + isAttacking);
            print("idle" + isIdle);
            */

            if (enemyHealth <= 0)
            {
                plr.score += 1;
                isDead = true;
            }
        }
        else
        {
            anim.SetBool("isDead", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttacking", false);
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                Destroy(gameObject);

            }
        }
        
        
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerInRadius = true;
            
        }
        else
        {
            playerInRadius = false;
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerInRadius = false;

        }
        
    }
    void DoDamage()
    {
        if (playerInRadius == true)
        {
            plr.playerHealth -= enemyDamage;
        }
        
            
        
    }

}
