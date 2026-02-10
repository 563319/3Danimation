using UnityEngine;

public class healthPickup : MonoBehaviour
{
    Player plr;
    Vector3 rotSpeed = new Vector3(0, 100, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plr = GameObject.Find("player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (plr.playerHealth < plr.playerMaxHealth)
            {
                plr.playerHealth += 50;
                Destroy(gameObject);
            }
            
        }
        
    }
}
