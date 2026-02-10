using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class enemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTimer = 25;
    float counter2 = 100;
    public float waveCounter = 1;
    bool canSpawn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveCounter % 3 == 0)
        {
            canSpawn = false;
            counter2 -= Time.deltaTime;
            if (counter2 == 0)
            {
                canSpawn = true;
            }


        }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && canSpawn == true)
        {
            // Instantiate the bullet at the position and rotation of the player
            GameObject clone;
            print("spawner:" + gameObject.name);
            clone = Instantiate(enemy, transform.position, transform.rotation);
            waveCounter += 1;



            spawnTimer = 10;
        }

    }
}
