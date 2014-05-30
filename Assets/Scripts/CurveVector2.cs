using UnityEngine;
using System.Collections;

public class AnimationCurveVector2
{
	private AnimationCurve[] m_coordinateCurve = new AnimationCurve[]
	{
		new AnimationCurve(),
		new AnimationCurve()
	};
	
	public Vector2 Evaluate(float time)
	{	
		Vector3 vector = new Vector2();

		for (int i = 0; i < 2; i++)
			vector[i] = m_coordinateCurve[i].Evaluate(time);
		
		return vector;
	}

	public void RemoveFirstKeyframe()
	{
		if (m_coordinateCurve[0].length == 0 ||
		    m_coordinateCurve[1].length == 0)
			throw new System.Exception();


		m_coordinateCurve[0].RemoveKey(0);
		m_coordinateCurve[1].RemoveKey(0);
	}

	public void AddKeyframe(float time, Vector3 value)
	{
		m_coordinateCurve[0].AddKey(time, value.x);
		m_coordinateCurve[1].AddKey(time, value.y);
	}
}

