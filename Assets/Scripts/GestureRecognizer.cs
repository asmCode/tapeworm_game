using UnityEngine;
using System.Collections;

public class GestureRecognizer : MonoBehaviour
{
	private bool m_duringGesture;
	private Vector3 m_startPosition;
	private Vector3 m_lastPosition;

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !m_duringGesture)
		{
			m_duringGesture = true;
			m_startPosition = Input.mousePosition;
			m_lastPosition = m_startPosition;

			if (PanGestureStarted != null)
				PanGestureStarted(m_startPosition);
		}
		else if (Input.GetMouseButtonUp(0) && m_duringGesture)
		{
			m_duringGesture = false;

			if (PanGestureEnded != null)
				PanGestureEnded(m_lastPosition);
		}
		else if (m_duringGesture && Input.GetMouseButton(0))
		{
			if (m_lastPosition != Input.mousePosition)
			{
				float changeDeltaValue = (Input.mousePosition - m_lastPosition).magnitude;
				float changeTotalValue = (Input.mousePosition - m_startPosition).magnitude;
				m_lastPosition = Input.mousePosition;

				if (PanGestureChanged != null)
					PanGestureChanged(Input.mousePosition, changeDeltaValue, changeTotalValue);
			}
		}
	}

	public delegate void PanGestureStartedDelegate(Vector3 position);
	public delegate void PanGestureChangedDelegate(Vector3 position, float changeDeltaValue, float changeTotalValue);
	public delegate void PanGestureEndedDelegate(Vector3 position);

	public event PanGestureStartedDelegate PanGestureStarted;
	public event PanGestureChangedDelegate PanGestureChanged;
	public event PanGestureEndedDelegate PanGestureEnded;
}
