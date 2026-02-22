using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class enemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTimer = 15;
    
    public float waveCounter = 1;
    bool canSpawn = true;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && canSpawn == true)
        {
            
            GameObject clone;
            print("spawner:" + gameObject.name);
            clone = Instantiate(enemy, transform.position, transform.rotation);
            waveCounter += 1;



            spawnTimer = 25;
        }

    }
}
