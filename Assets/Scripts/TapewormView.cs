using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TapewormView : MonoBehaviour
{
	private static readonly float SegmentsDistance = 2.0f;

	public TapewormSegment m_tapewormSegmentPrefab;

	private List<TapewormSegment> m_segments;

	void Start()
	{
		m_segments = new List<TapewormSegment>();

		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
		AddSegment();
	}

	void Update()
	{
		float moveValue = 0;

		if (Input.GetKey(KeyCode.UpArrow))
		{
			moveValue = 8.0f;
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			moveValue = -8.0f;
		}

		/*
		Vector3 firstSegPos = m_segments[0].transform.position;
		firstSegPos.y += moveValue * Time.deltaTime;
		m_segments[0].transform.position = firstSegPos;
		*/

		if (Input.GetMouseButton(0))
		{
			Vector3 firstSegPos = m_segments[0].transform.position;
			firstSegPos.y = (Input.mousePosition.y - (Screen.width / 2)) * 0.05f;
			m_segments[0].transform.position = firstSegPos;
		}

		UpdateSegments();
	}

	private void AddSegment()
	{
		TapewormSegment lastSegment = null;

		if (m_segments.Count > 0)
			lastSegment = m_segments[m_segments.Count - 1];

		TapewormSegment tapewormSegment = (TapewormSegment)Instantiate(m_tapewormSegmentPrefab);
		tapewormSegment.transform.position =
			lastSegment != null ?
				lastSegment.transform.position + Vector3.forward * SegmentsDistance :
				Vector3.zero;


		tapewormSegment.transform.rotation = Quaternion.identity;

		m_segments.Add(tapewormSegment);
	}

	private void UpdateSegments()
	{
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

		for (int i = 1; i < m_segments.Count; i++)
		{
			Vector3 currentPosition = m_segments[i].transform.position;
			
			float heightDiff = m_segments[i - 1].transform.position.y - currentPosition.y;

			speed = heightDiff * heightDiff * 10.0f;
			
			currentPosition.y += Mathf.Min(heightDiff * Mathf.Sign(heightDiff), speed * Mathf.Sign(heightDiff) * Time.deltaTime);
			m_segments[i].transform.position = currentPosition;
		}
	}
}
