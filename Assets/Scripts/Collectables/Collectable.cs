using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
	private float m_speed;
	
	public EventHandler ReachedToEnd;
	public EventHandler<TapewormSegment> Collected;
	
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
		TapewormSegment tpSegment = other.gameObject.GetComponent<TapewormSegment>();

		if (tpSegment != null)
			if (Collected != null)
				Collected(this, tpSegment);
	}
	
	public void Initialize(float speed)
	{
		m_speed = speed;
	}
}
