using Unity.Hierarchy;
using UnityEngine;

public class MedSpawnerScript : MonoBehaviour
{
    public GameObject healthPickup;
    float spawnTimer = 10;
    bool canSpawn = true;
    Vector3  offset = new Vector3(0,1,0);
    public Player plr;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
       
            spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && canSpawn == true)
        {
            // Instantiate the bullet at the position and rotation of the player
            GameObject clone;
            print("spawner:" + gameObject.name);
            clone = Instantiate(healthPickup, transform.position + offset, transform.rotation);
            print("spawned medkit");
            canSpawn = false;
            if (clone == null)
            {
                canSpawn = true;
            }




            spawnTimer = 10;
        }
    }
}
