using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	public CollectablesManager m_collectablesManager;
	public TapewormView m_tapewormView;

	private int m_collectedFoodCount;

	void Awake()
	{
		m_collectablesManager.CollectedFood += HandleCollectedFood;
		m_collectablesManager.CollectedMedicine += HandleCollectedMedicine;
	}

	void HandleCollectedMedicine (object sender, TapewormSegment tapewormSegment)
	{
		if (tapewormSegment.Index != 0)
			m_tapewormView.CutAt(tapewormSegment.Index);
	}

	void HandleCollectedFood (object sender, TapewormSegment tapewormSegment)
	{
		m_collectedFoodCount++;
		if (m_collectedFoodCount == 5.0f)
		{
			m_collectedFoodCount = 0;
			m_tapewormView.AddSegment();
		}
	}

	void HandleUpClicked (object sender)
	{

	}

	void HandleDownClicked (object sender)
	{

	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
