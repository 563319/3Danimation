using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject enemy;
    float spawnTimer = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            // Instantiate the bullet at the position and rotation of the player
            GameObject clone;
            print("spawner:" + gameObject.name);
            clone = Instantiate(enemy, transform.position, transform.rotation);



            
            spawnTimer = 10;
        }

    }
}
