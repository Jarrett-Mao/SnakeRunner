using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    //managers
    public GameController GC;

    //variables and storing
    public List<Transform> BodyParts = new List<Transform>();
    public float minDistance = 0.25f;   
    public int initialAmount;
    public float speed = 1;
    public float rotationSpeed = 50;
    public float lerpTimeX;
    public float lerpTimeY;

    //snake head prefab
    public GameObject BodyPrefab; 

    //number of body parts
    public TextMesh PartAmountTextMesh;

    //private fields
    private float distance;
    private Vector3 refVelocity;

    private Transform curBodyPart;
    private Transform prevBodyPart;

    private bool firstpart;

    Vector2 mousePreviousPos;
    Vector2 mouseCurrentPos;

    //particle system
    public ParticleSystem SnakeParticle;
    
    // Start is called before the first frame update
    void Start()
    {
        firstPart = true;

        for (int i = 0; i < initialAmount; i++){
            Invoke ("AddBodyPart, 0.1f");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamestate == GameController.GameState.GAME){
            Move();

            //end condition
            if (BodyParts.Count == 0){
                GC.SetGameOver();
            }

            if (PartAmountTextMesh != null){
                PartsAmountTextMesh.text = transform.childCount + "";
            }
        }
    }

    public void SpawnBodyPart(){
        firstPart = true;

        for (int i = 0; i < initialAmount; i++){
            Invoke ("AddBodyPart, 0.1f");
        }
    }

    public void Move(){
        float curSpeed = speed;
        if(BodyParts.Count > 0){
            BodyParts[0].Translate(Vector2.up * curSpeed * Time.smoothDeltaTime);

            float maxX = Camera.main.orthographicSize * Screen.width / Screen.height;

            if(BodyParts.Count > 0){
                if (BodyParts[0].position > maxX){
                    BodyParts[0].position = new Vector3(maxX - 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
                }
                else if (BodyParts[0].position.x < -maxX){
                    BodyParts[0].position = new Vector3(-maxX + 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
                }
            }
        }

        //get mouse button down may need to find an alternative
        if(Input.GetMouseButtonDown(0)){
            mousePreviousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonDown(0)){
            if(BodyParts.Count > 0 && Mathf.Abs(BodyParts[0].position.x) < maxX){
                mouseCurrentPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
                float deltaMousePos = Mathf.Abs(mousePrevious.x - mouseCurrentPos.x);
                float sign = Mathf.Sign(mousePreviousPos.x - mouseCurrentPos.x);

                BodyParts[0].GetComponent<Rigidbody2D>().AddForce(Vector2.right * rotationSpeed * deltaMousePos * - sign);
                mousePreviousPos = mouseCurrentPos; 
            }
            else if (BodyParts.Count > 0 && BodyParts[0].position.x > maxX){
                BodyParts[0].position = new Vector3(maxX - 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
            }
            else if (BodyParts.Count > 0 && BodyParts[0].position.x < maxX){
                BodyParts[0].position = new Vector3(-maxX + 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
            }
        }

        for (int i = 1; i < BodyParts.Count; i++){
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i-1];

            distance = Vector3.Distance (prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            newPos.z = BodyParts[0].position.z;

            Vector3 pos = curBodyPart.position;

            pos.x = Mathf.Lerp(pos.x, newPos.x, LerpTimeX);
            pos.y = Mathf.Lerp(pos.y, newPos.y, LerpTimeY);

            curBodyPart.position = pos;
        }
    }

    public void AddBodyPart(){

        Transform newPart;

        if (firstPart){
            newPart = (Instantiate(BodyPrefab, new Vector3(0,0,0), Quarternion.identity) as GameObject).transform;

            PartAmountTextMesh.transform.parent = newPart.position + new Vector3 (0, 0.5f, 0);

            firstPart = false;
        }
        else {
            newPart = (Instantiate(BodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation as GameObject).transform);
            newPart.SetParent(transform);
            BodyParts.Add(newPart);
        }
    }


}
