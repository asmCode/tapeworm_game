using UnityEngine;
using System.Collections;

public class Food : Collectable
{
	private float m_speed;

	public EventHandler ReachedToEnd;
	public EventHandler Collected;

	void Start()
	{

	}

	void Update()
	{
		Vector3 position = transform.position;
		position.z -= m_speed * Time.deltaTime;
		transform.position = position;

		if (transform.position.z <= 0.0f)
			if (ReachedToEnd != null)
				ReachedToEnd(this);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<TapewormSegment>() != null)
			if (Collected != null)
				Collected(this);
	}

	public void Initialize(float speed)
	{
		m_speed = speed;
	}
}
