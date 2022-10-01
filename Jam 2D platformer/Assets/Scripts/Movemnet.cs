using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movemnet : MonoBehaviour
{
	[SerializeField]
	public ItemHealth itemHealth;
	[SerializeField]
	public BrokenItem brokenItem;
	[SerializeField]
	public ItemTransfer itemTransfer;
	[SerializeField]
	public InsertItem insertItem;
	[SerializeField]
	iTween.EaseType easeType;
	[SerializeField]
	float jumpHeight = 8.5f;
	[SerializeField]
	float movementSpeed = 5f;
	[SerializeField]
	float inputDelay = 0.1f;
	[SerializeField]
	float deccelTime = 2;
	[SerializeField]
	float raycastLength = 10000f;
	[SerializeField]
	LayerMask wall;
	public Transform feetPos;
	public LayerMask layerMask;
	public GameObject box;
	public GameObject rope;
	public GameObject craneHandle;

	#region GEAR SHAPES MEMBERS 

	private GearShapes currentGearShape;
	private Sprite currentGearSprite;
	private int doorFilled = 0;

	private List<InsertItem> doorList = new List<InsertItem>();
	private List<ItemTransfer> gearList = new List<ItemTransfer>();
	private List<GearShapes> targetGearShapes = new List<GearShapes>() { GearShapes.Square, GearShapes.Triangle, GearShapes.Circle };

	#endregion

	#region Lever InterAction Member

	public LeverInteraction GetLever;
	private bool isLeverPulling = false;

	#endregion

	#region POP UP 

	public GameObject popUpPrefab;
	public Sprite log, lever, circleCog, squareCog, triangleCog;

	#endregion
	SpriteRenderer sr;
	Animator anim;

	Vector2 velocity;
	Rigidbody2D playerRb;
	[SerializeField] bool grounded;

	public GameObject boat;
	public GameObject collectable;

	public PlayerStates playerStates;
	public PlayerHoldingStates playerHoldingStates;
	//public bool woodExtract;

	void Start()
	{
		playerRb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

	}

	void Update()
	{
		grounded = Physics2D.OverlapCircle(feetPos.position, .2f, layerMask);
		Jump(Input.GetAxis("Jump"));

		OnMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (playerStates != PlayerStates.Repairing)
			{
				Mele();
			}
			if (GetLever != null)
			{
				GetLever.sprite.flipX = true;
				LeverLogic();
				GetLever = null;
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (playerStates == PlayerStates.Repairing)
				anim.SetTrigger("Repair");

			Repair();
		}

		playerRb.velocity = velocity;
	}

	private void OnMove(float inputX, float inputY)
	{
		anim.SetFloat("Walk", Mathf.Abs(inputX));
		if (playerStates == PlayerStates.Climbing)
		{
			anim.SetFloat("Climb", Mathf.Abs(inputY));
		}
		if (Mathf.Abs(inputX) >= inputDelay || Mathf.Abs(inputY) >= inputDelay)
		{
			if (playerStates == PlayerStates.Boating)
			{
				if (inputX > 0)
				{

					transform.position += Vector3.right * Time.deltaTime * inputX * movementSpeed;

				}

			}
			else
			{
				transform.position += Vector3.right * Time.deltaTime * inputX * movementSpeed;
			}
			if (inputX < 0)
				sr.flipX = true;
			else if (inputX > 0)
				sr.flipX = false;

			if (playerStates == PlayerStates.Climbing)
			{
				float ypos = transform.position.y;
				ypos += Time.deltaTime * inputY * 5f;
				ypos = Mathf.Clamp(ypos, -1.909492f, 2.5205f);
				transform.position = new Vector3(transform.position.x, ypos, transform.position.z);
			}
		}

		else if (Input.GetKey(KeyCode.LeftControl) && Vector3.Distance(transform.position, box.transform.position) < 1.5f && playerStates == PlayerStates.Walking)
		{
			box.transform.SetParent(transform);
			playerStates = PlayerStates.HoldingBox;
		}
		else if (!Input.GetKey(KeyCode.LeftControl) && playerStates == PlayerStates.HoldingBox)
		{
			playerStates = PlayerStates.Walking;
			box.transform.SetParent(null);
		}
	}

	void Jump(float input)
	{

		if (input == 1 && grounded)
		{
			velocity.y = jumpHeight;
			anim.SetTrigger("Jump");
		}
		else if (!grounded && playerStates != PlayerStates.Climbing)
		{
			velocity.y += 9.8f * Time.deltaTime * -2;
		}
		else
			velocity.y = 0;
	}

	void Mele()
	{
		if (itemHealth != null && itemHealth.isTriggered)
		{
			itemHealth.itemHealth -= 1;
			if (itemHealth.itemHealth == 0)
			{
				if (itemHealth.gameObject.name == "Tree" || itemHealth.gameObject.name == "Boat")
				{
					var obj = Instantiate(popUpPrefab, transform);
					obj.GetComponent<PopUpController>().ShowPopUp(log);
				}
				else if (itemHealth.gameObject.name == "broken_Lever1" || itemHealth.gameObject.name == "broken_Lever2")
				{
					var obj = Instantiate(popUpPrefab, transform);
					obj.GetComponent<PopUpController>().ShowPopUp(lever);
				}

				itemHealth.gameObject.SetActive(false);
				itemHealth.brokenItem.enabled = true;
				playerStates = PlayerStates.Repairing;
				//woodExtract = true;
			}
		}

		if (itemTransfer != null && itemTransfer.isTriggered)
		{
			itemTransfer.itemHealth -= 1;
			if (itemTransfer.itemHealth == 0)
			{
				if (itemTransfer.gameObject.name == "CircleGear")
				{
					var obj = Instantiate(popUpPrefab, transform);
					obj.GetComponent<PopUpController>().ShowPopUp(circleCog);
				}
				else if (itemTransfer.gameObject.name == "DiamondGear")
				{
					var obj = Instantiate(popUpPrefab, transform);
					obj.GetComponent<PopUpController>().ShowPopUp(triangleCog);
				}
				else if (itemTransfer.gameObject.name == "SquareGear")
				{
					var obj = Instantiate(popUpPrefab, transform);
					obj.GetComponent<PopUpController>().ShowPopUp(squareCog);
				}
				playerStates = PlayerStates.Repairing;
				//itemTransfer.collectedGameObj = collectable;
				//itemTransfer.gameObject.SetActive(false);

				currentGearShape = itemTransfer.shape;
				currentGearSprite = itemTransfer.GetSpriteRenderer.sprite;

				itemTransfer.gameObject.SetActive(false);

			}
		}


	}

	void Repair()
	{
		if (brokenItem != null && brokenItem.isTriggered)
		{
			brokenItem.repair -= 1;
			if (brokenItem.repair == 0)
			{
				brokenItem.gameObject.SetActive(false);
				brokenItem.objectToActivate.SetActive(true);

				playerStates = PlayerStates.Walking;
			}

		}

		if (insertItem != null && insertItem.isTriggered)
		{


			insertItem.repair -= 1;
			if (insertItem.repair == 0)
			{
				//collectable.transform.position = insertItem.insertPos.position;
				//Debug.Log(collectable.transform.position);
				//collectable.gameObject.SetActive(true);
				//insertItem.colliderToenable.enabled = true;

				insertItem.Item.sprite = currentGearSprite;
				insertItem.fixedGearShape = currentGearShape;



				currentGearSprite = null;
				currentGearShape = GearShapes.None;
				//itemTransfer.gameObject.SetActive(false);
				playerStates = PlayerStates.Walking;

				doorFilled++;
				insertItem = null;
			}

		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		itemHealth = collision.gameObject.GetComponent<ItemHealth>();
		if (itemHealth != null)
			itemHealth.isTriggered = true;


		switch (collision.gameObject.tag)
		{
			case "Ground":
				break;
			case "Boat":
				playerStates = PlayerStates.Boating;
				float xPosition = collision.transform.lossyScale.x + .25f + collision.transform.lossyScale.x * 6;
				transform.SetParent(collision.transform);
				iTween.MoveTo(collision.gameObject, iTween.Hash("x", xPosition, "time", 3, "easetype", easeType));
				collision.transform.tag = "Ground";
				StartCoroutine(EnableScript());
				break;
			case "Box":
				playerStates = PlayerStates.HoldingBox;
				break;
			case "PondBase":
				GameOver();
				break;
		}
	}

	void GameOver()
	{
		SceneManager.LoadScene("GameOver");
	}

	void OnCollisionExit2D(Collision2D collider)
	{
		itemHealth = collider.gameObject.GetComponent<ItemHealth>();
		if (itemHealth != null)
			itemHealth.isTriggered = false;

		if (collider.gameObject.tag == "Ground")
		{

			if (collider.gameObject.GetComponent<Rigidbody2D>())
			{
				collider.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}
		if (collider.gameObject.tag == "Boat")
		{
			if (collider.gameObject.GetComponent<Rigidbody2D>())
			{
				playerStates = PlayerStates.Walking;
				collider.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}
		if (collider.gameObject.tag == "Box")
		{

			playerStates = PlayerStates.Walking;
		}
	}
	IEnumerator EnableScript()
	{
		yield return new WaitForSeconds(3);
		playerStates = PlayerStates.Walking;
		transform.SetParent(null);
	}


	public void LeverLogic()
	{
		if (doorFilled == 3)
		{
			int count = 0;
			for (int i = 0; i < doorList.Count; i++)
			{
				if (doorList[i].doorID == i + 1)
				{
					if (doorList[i].fixedGearShape == targetGearShapes[i])
					{
						count++;
					}
				}


			}

			if (count == 3)
			{
				// PLATFORM WILL BE ALIVATED
				foreach (var door in doorList)
				{
					door.transform.GetComponent<BoxCollider2D>().isTrigger = false;
				}
				DoorAviation();
			}
			else
			{
				count = 0;
				GetLever.sprite.flipX = false;
				foreach (var gear in gearList)
				{
					gear.gameObject.SetActive(true);
					gear.itemHealth = 3;
				}
				foreach (var door in doorList)
				{
					door.fixedGearShape = GearShapes.None;
					door.Item.sprite = null;
					door.repair = 3;
				}
				doorFilled = 0;
			}
		}
		else if (GetLever.objectName == "Lever2")
		{
			Debug.Log("Done");
			ElevatePlatform();
		}

	}

	private void ElevatePlatform()
	{
		Debug.LogError("ElevatePlatform");
		Vector3 scale = new Vector3(rope.transform.localScale.x, .25f, rope.transform.localScale.z);
		Vector3 position = new Vector3(craneHandle.transform.position.x, -2.6f, craneHandle.transform.position.z);
		iTween.ScaleTo(rope, iTween.Hash("y", .06f, "time", 5f, "easetype", easeType));
		iTween.MoveTo(craneHandle, iTween.Hash("y", 0.75f, "time", 5f, "easetype", easeType));
	}

	public void DoorAviation()
	{

		iTween.MoveTo(doorList[1].gameObject, iTween.Hash("y", -1f, "time", 1f, "easetype", easeType));

		iTween.MoveTo(doorList[2].gameObject, iTween.Hash("y", -0.3f, "time", 1f, "easetype", easeType));

	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		itemHealth = collider.GetComponent<ItemHealth>();
		if (itemHealth != null)
		{
			itemHealth.isTriggered = false;
		}

		playerRb.velocity = Vector3.zero;

		if (collider.transform.tag == "Ladder")
		{
			playerStates = PlayerStates.Walking;
			playerRb.gravityScale = 1;

		}

		itemTransfer = null;
		GetLever = null;
	}
	private void OnTriggerEnter2D(Collider2D collider)
	{
		itemHealth = collider.GetComponent<ItemHealth>();
		if (itemHealth != null)
			itemHealth.isTriggered = true;

		switch (collider.transform.tag)
		{
			case "Ladder":
				playerStates = PlayerStates.Climbing;
				playerRb.gravityScale = 0;
				break;
			case "BrokenItem":
				brokenItem = collider.GetComponent<BrokenItem>();

				if (brokenItem.repair != 0)
				{
					if (brokenItem != null)
					{
						brokenItem.isTriggered = true;
					}
				}
				break;
			case "Gear":
				itemTransfer = collider.GetComponent<ItemTransfer>();
				if (itemTransfer.isTriggered != null)
					itemTransfer.isTriggered = true;

				if (itemTransfer.itemHealth != 0)
				{
					if (itemTransfer != null)
					{
						if (gearList.Count > 0)
						{
							bool isRepeating = false;
							for (int i = 0; i < gearList.Count; i++)
							{
								if (itemTransfer != gearList[i])
								{

								}
								else
								{
									isRepeating = true;
									break;
								}
							}
							if (!isRepeating)
								gearList.Add(itemTransfer);
						}
						else if (doorList.Count == 0)
						{
							gearList.Add(itemTransfer);
						}

					}
				}
				break;
			case "Door":
				insertItem = collider.GetComponent<InsertItem>();
				if (insertItem.repair != 0)
				{
					if (insertItem != null)
					{
						insertItem.isTriggered = true;
						if (doorList.Count > 0)
						{
							bool isRepeating = false;
							for (int i = 0; i < doorList.Count; i++)
							{
								if (insertItem != doorList[i])
								{

								}
								else
								{
									isRepeating = true;
									break;
								}
							}
							if (!isRepeating)
								doorList.Add(insertItem);
						}
						else if (doorList.Count == 0)
						{
							doorList.Add(insertItem);
						}
					}
				}
				break;
			case "Lever":
				GetLever = collider.GetComponent<LeverInteraction>();
				if (collider.gameObject.name == "Lever2")
				{
					doorFilled = 0;
					GetLever.LeverChecker("Lever2");
				}

				break;
			case "Pond":
				Drown();
				break;
		}



	}
	void Drown()
	{
		Debug.Log("Called");
		anim.SetTrigger("Climb");
		playerRb.velocity = Vector3.zero;
		playerRb.gravityScale = 0;
		Debug.Log(playerRb.velocity);
	}
}


