using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodBehavior : MonoBehaviour
{
    //snake manager
    SnakeMovement SM;
    public int foodAmount;

    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMovement>();

        foodAmount = Random.Range(1, 10);
        transform.GetComponentInChildren<TextMeshPro>().text = "" + foodAmount.ToString(); //fixed
    }

    // Update is called once per frame
    void Update()
    {
        if (SM.transform.childCount > 0 && transform.position.y - SM.transform.GetChild(0).position.y < -10){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        Destroy(this.gameObject);
    }
}
