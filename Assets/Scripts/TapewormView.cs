using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TapewormView : MonoBehaviour
{
	private static readonly float SegmentsDistance = 0.3f;
	private static readonly float SegmentMovementDelay = 0.05f;
	private static readonly float TapewormShift = 1.8f;

	public ButtonBehavior m_down;
	public ButtonBehavior m_up;
	public GestureRecognizer m_gestureRecognizer;

	private Vector3 m_panStartPosition;
	private Vector3 m_lastPanPosition;

	public TapewormSegment m_tapewormSegmentPrefab;

	private List<TapewormSegment> m_segments;

	private float m_updateMovementCooldown;

	private AnimationCurveVector2 m_movementCurve;

	void Start()
	{
		m_segments = new List<TapewormSegment>();

		for (int i = 0; i < 20; i++)
			AddSegment();

		m_down.Pressed += HandleDownPressed;
		m_down.Released += HandleDownReleased;
		m_up.Pressed += HandleUpPressed;
		m_up.Released += HandleUpReleased;

		m_gestureRecognizer.PanGestureChanged += HandlePanGestureChanged;
		m_gestureRecognizer.PanGestureStarted += HandlePanGestureStarted;

		m_movementCurve = new AnimationCurveVector2();
		for (int i = 0; i < 200; i++)
		{
			m_movementCurve.AddKeyframe((float)i / 200.0f, Vector2.zero);
		}
	}

	void HandlePanGestureStarted (Vector3 position)
	{
		m_panStartPosition = position;
		m_lastPanPosition = position;
	}

	private void HandlePanGestureChanged(Vector3 position, float changeDeltaValue, float changeTotalValue)
	{
		float totalHoriDist = m_lastPanPosition.x - position.x;
		m_lastPanPosition = position;

		Move(-totalHoriDist / Screen.width);
	}

	private void Move(float value)
	{
		m_move += value;
	}

	float m_move;

	void HandleDownReleased (object sender)
	{
		m_move = 0.0f;
	}

	void HandleUpReleased (object sender)
	{
		m_move = 0.0f;
	}

	void HandleUpPressed (object sender)
	{
		m_move = 8.0f;
	}

	void HandleDownPressed (object sender)
	{
		m_move = -8.0f;
	}

	void Update()
	{
		float moveValue = m_move * 600.0f;


		/*if (Input.GetKey(KeyCode.UpArrow))
		{
			moveValue = 8.0f;
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			moveValue = -8.0f;
		}*/

		m_segments[0].transform.position = Quaternion.AngleAxis(moveValue, Vector3.forward) * Vector3.down +
			Vector3.forward * TapewormShift;

		m_movementCurve.AddKeyframe(Time.time, new Vector2(m_segments[0].transform.position.x, m_segments[0].transform.position.y));
		m_movementCurve.RemoveFirstKeyframe();
		
		m_updateMovementCooldown += Time.deltaTime;
		if (m_updateMovementCooldown >= SegmentMovementDelay)
		{
			m_updateMovementCooldown = 0.0f;
			//UpdateSegmentsMovement();
		}

		UpdateSegments();
		//FixSegmentsLength();
	}

	public void CutAt(int index)
	{
		if (index >= m_segments.Count)
			return;

		for (int i = index; i < m_segments.Count; i++)
			Destroy(m_segments[i].gameObject);

		m_segments.RemoveRange(index, m_segments.Count - index);
	}

	private void FixSegmentsLength()
	{
		for (int i = 1; i < m_segments.Count; i++)
		{
			Vector3 currentPosition = m_segments[i].transform.position;

			Vector3 direction = m_segments[i].transform.position - m_segments[i - 1].transform.position;
			
			m_segments[i].transform.position =
				m_segments[i - 1].transform.position + direction.normalized * SegmentsDistance;
		}
	}

	public void AddSegment()
	{
		TapewormSegment lastSegment = null;

		if (m_segments.Count > 0)
			lastSegment = m_segments[m_segments.Count - 1];

		TapewormSegment tapewormSegment = (TapewormSegment)Instantiate(m_tapewormSegmentPrefab);
		tapewormSegment.Index = m_segments.Count;
		tapewormSegment.transform.position =
			lastSegment != null ?
				lastSegment.transform.position + Vector3.forward * SegmentsDistance :
				new Vector3(0, 0, TapewormShift);


		tapewormSegment.transform.rotation = Quaternion.identity;

		m_segments.Add(tapewormSegment);
	}

	private void UpdateSegments()
	{
		//Debug.Log(string.Format("{0} {1}", Input.mousePosition.x, Input.mousePosition.y));
		//Debug.Log(string.Format("{0} {1}", Camera.main.ScreenToViewportPoint(Input.mousePosition).x, Camera.main.ScreenToViewportPoint(Input.mousePosition).y));

		/*
		for (int i = 1; i < m_segments.Count; i++)
		{
			Vector3 currentPosition = m_segments[i].transform.position;

			float heightDiff = m_segments[i - 1].transform.position.y - currentPosition.y;

			currentPosition.y += Mathf.Min(heightDiff * Mathf.Sign(heightDiff), Mathf.Sign(heightDiff) * 1.0f * Time.deltaTime);
			m_segments[i].transform.position = currentPosition;

			Vector3 direction = m_segments[i].transform.position - m_segments[i - 1].transform.position;

			m_segments[i].transform.position =
				m_segments[i - 1].transform.position + direction.normalized * SegmentsDistance;
		}
		*/

		float speed = 1.0f;

		float timeStep = 0.1f;
		float timeShift = Time.time - timeStep;

		for (int i = 1; i < m_segments.Count; i++)
		{
			/*
			Vector3 currentPosition = m_segments[i].transform.position;
			Vector3 destinationPosition = m_segments[i - 1].transform.position;
			destinationPosition.z = currentPosition.z;

			Vector3 currentVelocity = m_segments[i].CurrentVelocity;

			//currentPosition.y = Mathf.SmoothDampAngle(currentPosition.y, m_segments[i - 1].transform.position.y, ref currentVelocity, 0.08f);
			currentPosition = Vector3.SmoothDamp(currentPosition, destinationPosition, ref currentVelocity, 0.1f);

			m_segments[i].CurrentVelocity = currentVelocity;

			m_segments[i].transform.position = currentPosition;
			*/

			Vector2 position2 = m_movementCurve.Evaluate(timeShift);

			//Debug.Log(string.Format("{0}, {1}", position2.x, position2.y));

			timeShift -= timeStep;
			m_segments[i].transform.position = new Vector3(position2.x, position2.y, m_segments[i].transform.position.z);
			//m_segments[i].transform.position = new Vector3(position2.x, position2.y, SegmentsDistance * i);

			Vector3 directionToNextSegment = m_segments[i].transform.position - m_segments[i - 1].transform.position;
			m_segments[i - 1].transform.forward = directionToNextSegment.normalized;

			Vector3 scale = m_segments[i - 1].transform.localScale;
			scale.z = directionToNextSegment.magnitude;
			m_segments[i - 1].transform.localScale = scale;
		}

		Vector3 lasteSegmentScale = m_segments[m_segments.Count - 1].transform.localScale;
		lasteSegmentScale.z = SegmentsDistance;
		m_segments[m_segments.Count - 1].transform.localScale = lasteSegmentScale;
	}

	private void UpdateSegmentsMovement()
	{
		for (int i = m_segments.Count - 1; i > 0 ; i--)
		{
			Vector3 position = m_segments[i].transform.position;
			position.y = m_segments[i - 1].transform.position.y;
			m_segments[i].transform.position = position;
		}
	}
}
