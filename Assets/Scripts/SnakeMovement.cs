using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SnakeMovement : MonoBehaviour {

	[Header("Managers")]
	public GameController GC;

	[Header("Some Snake Variables & Storing")]
	public List<Transform> BodyParts = new List<Transform>();
	public float minDistance = 0.25f;
	public int initialAmount;
	public float speed = 1;
	public float rotationSpeed = 50;
	public float LerpTimeX;
	public float LerpTimeY;
	public bool reversedControls = false;

	[Header("Snake Head Prefab")]
	public GameObject BodyPrefab;

	[Header("Parts Text Amount Management")]
	public TextMeshPro PartsAmountTextMesh;

	[Header("Private Fields")]
	private float distance;
	private Vector3 refVelocity;

	private Transform curBodyPart;
	private Transform prevBodyPart;

	private bool firstPart;

	private bool ghostOn = false;

	[Header("Mouse Control Variables")]
	Vector2 mousePreviousPos;
	Vector2 mouseCurrentPos;

	[Header("Particle System Management")]
	public ParticleSystem SnakeParticle;

	// Use this for initialization
	void Start () {
		SpawnBodyPart();
	}

	public void SpawnBodyPart()
	{
		firstPart = true;

		//Add the initial BodyParts
		for (int i = 0; i < initialAmount; i++)
		{
			//Use invoke to avoid a weird bug where the snake goes down at the beginning.
			Invoke("AddBodyPart", 0.1f);
		}
	}

    public void SpawnBodyPart(int amount)
	{
		//Add the initial BodyParts
		for (int i = 0; i < amount; i++)
		{
			//Use invoke to avoid a weird bug where the snake goes down at the beginning.
			Invoke("AddBodyPart", 0.1f);
		}
	}

	// Update is called once per frame
	void Update () {

		//We constantly move if the game is on
		if (GameController.gameState == GameController.GameState.GAME)
		{
			if (!reversedControls){
				Move();	
			}
			else {
				MoveReversed();
			}
			

			//Check if no more snake parts
			if (BodyParts.Count == 0)
				GC.SetGameOver();
		}

		//Update the Parts Amount text
		if(PartsAmountTextMesh != null){
			PartsAmountTextMesh.text = transform.childCount + "";
        }
	}

	private void Move()
	{
		float curSpeed = speed;

		//Always move the body Up
		if(BodyParts.Count > 0){
			for (int i = 1; i < BodyParts.Count; i++)
            {
                BodyParts[i].GetComponent<Collider2D>().enabled = false;
            }

            // Move the new head
            BodyParts[0].Translate(Vector2.up * curSpeed * Time.smoothDeltaTime);

            // Enable collider of the head
			if (!ghostOn){
				BodyParts[0].GetComponent<Collider2D>().enabled = true;
			}
            
        }

		//check if we are still on screen
		float maxX = Camera.main.orthographicSize * Screen.width / Screen.height;

		if (BodyParts.Count > 0)
		{
			if (BodyParts[0].position.x > maxX) //Right pos
			{
				BodyParts[0].position = new Vector3(maxX - 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
			}
			else if (BodyParts[0].position.x < -maxX) //Left pos
			{
				BodyParts[0].position = new Vector3(-maxX + 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
			}
		}

		//Move the snake on the Horizontal Axis with mouse control
		if (Input.GetMouseButtonDown(0))
		{
			mousePreviousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else if (Input.GetMouseButton(0))
		{
			//check if he is still in screen

			if ( BodyParts.Count > 0 && Mathf.Abs(BodyParts[0].position.x) < maxX)
			{
				mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				float deltaMousePos = Mathf.Abs(mousePreviousPos.x - mouseCurrentPos.x);
				float sign = Mathf.Sign(mousePreviousPos.x - mouseCurrentPos.x);


				//BodyParts[0].Translate(Vector3.left * rotationSpeed * Time.deltaTime * deltaMousePos * sign);
				BodyParts[0].GetComponent<Rigidbody2D>().AddForce
				(Vector2.right * rotationSpeed * deltaMousePos * -sign);

				mousePreviousPos = mouseCurrentPos;
			}
			else if (BodyParts.Count > 0 && BodyParts[0].position.x > maxX) //Right pos
			{
				BodyParts[0].position = new Vector3(maxX - 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
			} 
			else if (BodyParts.Count > 0 && BodyParts[0].position.x < maxX) //Left pos
			{
				BodyParts[0].position = new Vector3(-maxX + 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
			}
		}
		//Move the other body parts depending on the Head, that's why we start the loop at 1
		for (int i = BodyParts.Count - 1; i > 0; i--)
        {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            Vector3 newPos = prevBodyPart.position;
            // newPos.z = BodyParts[0].position.z;

            // Vector3 pos = curBodyPart.position;

            // pos.x = Mathf.Lerp(pos.x, newPos.x, LerpTimeX);
            // pos.y = Mathf.Lerp(pos.y, newPos.y, LerpTimeY);
            // curBodyPart.position = pos;

            Vector3 targetPosition = newPos;
            targetPosition.z = curBodyPart.position.z;

            curBodyPart.position = Vector3.MoveTowards(curBodyPart.position, targetPosition, Time.deltaTime * curSpeed);
        }

	}

	private void MoveReversed()
	{
		float curSpeed = speed;

		//Always move the body Up
		if(BodyParts.Count > 0){
			for (int i = 1; i < BodyParts.Count; i++)
            {
                BodyParts[i].GetComponent<Collider2D>().enabled = false;
            }

            // Move the new head
            BodyParts[0].Translate(Vector2.up * curSpeed * Time.smoothDeltaTime);

            // Enable collider of the head
            BodyParts[0].GetComponent<Collider2D>().enabled = true;
        }

		//check if we are still on screen
		float maxX = Camera.main.orthographicSize * Screen.width / Screen.height;

		if (BodyParts.Count > 0)
		{
			if (BodyParts[0].position.x > maxX) //Right pos
			{
				BodyParts[0].position = new Vector3(maxX - 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
			}
			else if (BodyParts[0].position.x < -maxX) //Left pos
			{
				BodyParts[0].position = new Vector3(-maxX + 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
			}
		}

		//Move the snake on the Horizontal Axis with mouse control
		if (Input.GetMouseButtonDown(0))
		{
			mousePreviousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else if (Input.GetMouseButton(0))
		{
			//check if he is still in screen

			if ( BodyParts.Count > 0 && Mathf.Abs(BodyParts[0].position.x) < maxX)
			{

				mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				float deltaMousePos = Mathf.Abs(mousePreviousPos.x - mouseCurrentPos.x);
				float sign = Mathf.Sign(mousePreviousPos.x - mouseCurrentPos.x);

				BodyParts[0].GetComponent<Rigidbody2D>().AddForce(Vector2.left * rotationSpeed * deltaMousePos * -sign);

				mousePreviousPos = mouseCurrentPos;
			}
			else if (BodyParts.Count > 0 && BodyParts[0].position.x > maxX) //Right pos
			{
				BodyParts[0].position = new Vector3(maxX - 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
			} 
			else if (BodyParts.Count > 0 && BodyParts[0].position.x < maxX) //Left pos	
			{
				BodyParts[0].position = new Vector3(-maxX + 0.01f, BodyParts[0].position.y, BodyParts[0].position.z);
			}
		}
		//Move the other body parts depending on the Head, that's why we start the loop at 1
		for (int i = BodyParts.Count - 1; i > 0; i--)
        {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            Vector3 newPos = prevBodyPart.position;
            // newPos.z = BodyParts[0].position.z;

            // Vector3 pos = curBodyPart.position;

            // pos.x = Mathf.Lerp(pos.x, newPos.x, LerpTimeX);
            // pos.y = Mathf.Lerp(pos.y, newPos.y, LerpTimeY);
            // curBodyPart.position = pos;

            Vector3 targetPosition = newPos;
            targetPosition.z = curBodyPart.position.z;

            curBodyPart.position = Vector3.MoveTowards(curBodyPart.position, targetPosition, Time.deltaTime * curSpeed);
        }

	}

    public void AddBodyPart()
    {
        Transform newPart;

        if (firstPart)
        {
            newPart = Instantiate(BodyPrefab, Vector3.zero, Quaternion.identity).transform;

            // Set this part as the parent of the Text Mesh
            PartsAmountTextMesh.transform.SetParent(newPart, false);

            // Reset local scale of the Text Mesh
            PartsAmountTextMesh.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f); 

            // Place it correctly relative to the newPart
            PartsAmountTextMesh.transform.localPosition = new Vector3(0, 0.5f, 0);

            // Assign the snake manager as the parent of the newPart
            newPart.SetParent(transform);

            firstPart = false;
        }
        else
        {
            if (BodyParts.Count > 0)
            {
                newPart = (Instantiate(BodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;
                newPart.SetParent(transform);
            }
            else
            {
                Debug.LogWarning("No previous body part to attach to.");
                return;
            }
        }

        BodyParts.Add(newPart);
    }

	public void ReverseActivate(float duration){
		StartCoroutine(ReverseControls(duration));
	}

	private IEnumerator ReverseControls(float duration){
		reversedControls = true;
		//change to green
		foreach(Transform bodyPart in BodyParts){
			Material bodyPartMaterial = bodyPart.GetComponent<Renderer>().material;
			bodyPartMaterial.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
		}
		yield return new WaitForSeconds(duration);

		//resets changes 
		reversedControls = false;
		foreach(Transform bodyPart in BodyParts){
			Material bodyPartMaterial = bodyPart.GetComponent<Renderer>().material;
			bodyPartMaterial.color = new Color(2.0f, 2.0f, 2.0f, 1.0f);
		}
	}

	public void ActivateGhost(float duration, float boost){
		StartCoroutine(GhostMode(duration, boost));
	}

	private IEnumerator GhostMode(float duration, float boost){
		ghostOn = true;

		//reduce the alpha 
		foreach(Transform bodyPart in BodyParts){
			Material bodyPartMaterial = bodyPart.GetComponent<Renderer>().material;
			//reduces alpha by half
			bodyPartMaterial.color = new Color(bodyPartMaterial.color.r, bodyPartMaterial.color.g, bodyPartMaterial.color.b, 0.5f);
		}

		speed += boost;
		BodyParts[0].GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(duration);
		ghostOn = false;
		foreach (Transform bodyPart in BodyParts)
        {
			Material bodyPartMaterial = bodyPart.GetComponent<Renderer>().material;
            bodyPartMaterial.color = new Color(bodyPartMaterial.color.r, bodyPartMaterial.color.g, bodyPartMaterial.color.b, 2.0f);
        }
		speed -= boost;
	}

	public void TurnOffPowerUps(){
		StopAllCoroutines();
		//resets changes 
		reversedControls = false;
		foreach(Transform bodyPart in BodyParts){
			Material bodyPartMaterial = bodyPart.GetComponent<Renderer>().material;
			bodyPartMaterial.color = new Color(2.0f, 2.0f, 2.0f, 1.0f);
		}
	}

}
