using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertItem : MonoBehaviour
{
	public int doorID;

	public bool isTriggered;
	public float repair = 3;
	public Transform insertPos;
	public Collider2D colliderToenable;

	public SpriteRenderer Item;
	[HideInInspector]
	public int itemHP = 0;

	[HideInInspector]
	public GearShapes fixedGearShape = GearShapes.None;
}
