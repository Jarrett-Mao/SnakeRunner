using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform SnakeContainer;
    Vector3 initialCameraPos;
    
    // Start is called before the first frame update
    void Start()
    {
        initialCameraPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(SnakeContainer.childCount > 0){
            //spherical linear interpolation used to interpolate between 2 values (usually vectors) along the shorter path on a sphere
            transform.position = Vector3.Slerp(transform.position, 
            (initialCameraPos + new Vector3(0, SnakeContainer.GetChild(0).position.y - Camera.main.orthographicSize/2, 0)), 0.1f);
        }
    }
}
