using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyBar : MonoBehaviour
{
    SnakeMovement SM;

    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.FindGameObjectWithTag("SnakeManager").GetComponent<SnakeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SM.transform.childCount > 0 && transform.position.y - SM.transform.GetChild(0).position.y < -10){
            Destroy(this.gameObject);
        }
    }
}
