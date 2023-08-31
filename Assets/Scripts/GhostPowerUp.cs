using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPowerUp : MonoBehaviour
{
    public float powerUpDuration = 5;
    public float addedSpeed = 2;

    SnakeMovement SM;

    private void OnTriggerEnter2D(Collider2D collision){
        // ApplyPowerUp(collision.gameObject);
        SM.ActivateGhost(powerUpDuration, addedSpeed);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
