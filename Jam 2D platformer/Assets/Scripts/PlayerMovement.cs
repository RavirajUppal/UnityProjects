using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	public ItemHealth treeHealth;
	float inputDelay = 0.1f;
	[SerializeField] bool isJumping;

	private int ctr = 0;
	bool isTreeHit;

	public float speed = 2f;
	public float jumpForce = 50f;
	public Transform camera;
	//public tutorialScript ts;
	//public bool woodExtraction;


	SpriteRenderer sr;
	Rigidbody2D playerRb;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		playerRb = GetComponent<Rigidbody2D>();
		//ts = GetComponent<tutorialScript>();

	}

	// Update is called once per frame
	void Update()
	{
		Moving();
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			Mele();
		}
		if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
		{
			Jumping();
		}
	}

	void Moving()
	{
		float input = Input.GetAxis("Horizontal");

		if (Mathf.Abs(input) >= inputDelay)
		{
			Vector2 move = new Vector2(input, 0) * Time.deltaTime * speed;
			transform.Translate(move);
			if (input < 0)
			{
				sr.flipX = true;
			}
			else if (input > 0)
			{
				sr.flipX = false;
			}
		}


	}

	void Mele()
	{

		if (treeHealth != null && treeHealth.isTriggered)
		{
			treeHealth.itemHealth -= 1;
			if (treeHealth.itemHealth == 0)
			{
				treeHealth.gameObject.SetActive(false);
				//woodExtraction = true;
				//ts.popupIndex = 3;
			}
		}
	}

	void Jumping()
	{

		playerRb.AddForce(Vector2.up * jumpForce);

		isJumping = true;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		treeHealth = collider.GetComponent<ItemHealth>();
		if (treeHealth != null)
			treeHealth.isTriggered = true;

	}
	private void OnTriggerExit2D(Collider2D collider)
	{
		treeHealth = collider.GetComponent<ItemHealth>();
		if (treeHealth != null)
			treeHealth.isTriggered = false;

	}
}
