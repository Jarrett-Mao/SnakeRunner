using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePowerUp : MonoBehaviour
{
    [SerializeField] 
    float powerUpDuration;

    [SerializeField]
    SnakeMovement SM;

    private void OnTriggerEnter2D(Collider2D collision){
        // ApplyPowerUp(collision.gameObject);
        SM.ReverseActivate(powerUpDuration);
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
