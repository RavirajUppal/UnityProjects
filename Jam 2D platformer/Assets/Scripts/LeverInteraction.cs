using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
	public SpriteRenderer sprite;
	public string objectName;


	public void LeverChecker(string objectName)
	{
		this.objectName = objectName;
	}
}
