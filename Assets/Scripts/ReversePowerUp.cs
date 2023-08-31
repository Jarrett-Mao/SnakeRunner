using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePowerUp : MonoBehaviour
{
    public float powerUpDuration = 10;

    SnakeMovement SM;

    private void OnTriggerEnter2D(Collider2D collision){
        // ApplyPowerUp(collision.gameObject);
        
        //prevents stacking and instead refreshes time
        if (!SM.reversedControls){
            SM.ReverseActivate(powerUpDuration);
        }
        else {
            SM.TurnOffPowerUps();
            SM.ReverseActivate(powerUpDuration);
        }
        
        Destroy(this.gameObject);
    }

    // private void ApplyPowerUp(GameObject player){
    //     StartCoroutine(ReverseControls(player));
    // }

    // private IEnumerator ReverseControls(GameObject player){
    //     if (SM != null){
    //         SM.ReverseControls(true);
    //         yield return new WaitForSeconds(powerUpDuration);
    //         SM.ReverseControls(false);
    //     }
    // }

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
