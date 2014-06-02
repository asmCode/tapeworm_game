using UnityEngine;
using System.Collections;

public class TapewormSegment : MonoBehaviour
{
	public TapewormSegment Predecessor;

	public int Index { get; set; }

	public Vector3 CurrentVelocity { get; set; }
}
