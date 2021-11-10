using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{

	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public TextMeshProUGUI timeText;
	//public TextMeshProUGUI timeText;
	public GameObject winTextObject;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;

	// At the start of the game..
	void Start()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;
		SetCountText();
		SetTimeText();
		// Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winTextObject.SetActive(false);
	}
	bool isPickedUp;
	float timer = 0f;
	void FixedUpdate()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
	}
	void Update()
	{
		float timer = 0f;
		bool isPickedUp = false;
		if (timer > 0f)
		{
			SetTimeText();
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				isPickedUp = false;
			}
			if (timer > 0f)
			{

			}
		}
	}
		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("PickUp"))
			{
				other.gameObject.SetActive(false);
				isPickedUp = true;
				count = count + 1;
				SetCountText();
			}
			if (other.gameObject.CompareTag("Pickup Special") && isPickedUp == true)
			{
				other.gameObject.SetActive(false);
			}
		}

		void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("PickUp") && isPickedUp == false)
			{

			}
			if (collision.gameObject.CompareTag("Pickup Special") && isPickedUp == true)
			{
				collision.collider.isTrigger = true;
			}
		}
	
	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 8)
		{
			// Set the text value of your 'winText'
			winTextObject.SetActive(true);
		}
	}
	void SetTimeText()
	{
		timeText.text = "Timer: " + timer.ToString();

	}
}