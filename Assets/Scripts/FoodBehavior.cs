using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        transform.GetComponentInChildren<TextMesh>().text = "" + foodAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
