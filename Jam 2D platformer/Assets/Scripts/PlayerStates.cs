using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
	Walking,
	Dead,
	Boating,
	Repairing,
	Climbing,
	HoldingBox
};

public enum PlayerHoldingStates
{
	Nothing,
	Wood
};

public enum GearShapes
{
	None,
	Circle,
	Square,
	Triangle
}
