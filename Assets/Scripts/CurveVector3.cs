using UnityEngine;
using System.Collections;

public class AnimationCurveVector3
{
	private AnimationCurve[] m_coordinateCurve = new AnimationCurve[]
	{
		new AnimationCurve(),
		new AnimationCurve(),
		new AnimationCurve()
	};
	
	public void AddKey(float time, Vector3 vector)
	{
		for (int i = 0; i < 3; i++)
		{
			m_coordinateCurve[i].AddKey(new Keyframe(time, vector[i]));
		}
	}
	
	public Vector3 Evaluate(float time)
	{	
		Vector3 vector = new Vector3();

		for (int i = 0; i < 3; i++)
			vector[i] = m_coordinateCurve[i].Evaluate(time);
		
		return vector;
	}
}

