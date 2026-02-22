
using UnityEngine;

public class MedSpawnerScript : MonoBehaviour
{
    public GameObject healthPickup;
    float spawnTimer = 10;
    bool canSpawn = true;
    Vector3  offset = new Vector3(0,1,0);
    GameObject clone;
    public Player plr;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clone == null)
        {
            canSpawn = true;
        }


        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && canSpawn == true)
        {
            // Instantiate the bullet at the position and rotation of the player
            if (clone != null)
            {
                Destroy(clone);
            }
            print("spawner:" + gameObject.name);
            clone = Instantiate(healthPickup, transform.position + offset, transform.rotation);
            print("spawned medkit");
            canSpawn = false;
            




            spawnTimer = 10;
        }
    }
    
}
