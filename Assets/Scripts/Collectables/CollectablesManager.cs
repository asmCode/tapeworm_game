using UnityEngine;
using System.Collections;

public class CollectablesManager : MonoBehaviour
{
	private static readonly Vector3 InitialPosition = new Vector3(1.0f, 0.0f, 20.0f);

	public Food m_foodPrefab;
	public Medicine m_medicinePrefab;

	public event EventHandler<TapewormSegment> CollectedFood;
	public event EventHandler<TapewormSegment> CollectedMedicine;
	
	void Start()
	{
		StartCoroutine("SpawnerCoroutine");
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			SpawnFood();
	}

	public void SpawnFood()
	{
		Food food = (Food)Instantiate(m_foodPrefab);
		food.transform.position = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * InitialPosition;
		food.transform.rotation = Quaternion.identity;
		food.Initialize(6.0f);

		food.ReachedToEnd += HandleFoodReachedToEnd;
		food.Collected += HandleFoodCollected;
	}

	public void SpawnMedicine()
	{
		Medicine medicine = (Medicine)Instantiate(m_medicinePrefab);
		medicine.transform.position = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * InitialPosition;
		medicine.transform.rotation = Quaternion.identity;
		medicine.Initialize(6.0f);
		
		medicine.ReachedToEnd += HandleFoodReachedToEnd;
		medicine.Collected += HandleFoodCollected;
	}

	private void HandleFoodReachedToEnd(object sender)
	{
		Collectable food = sender as Collectable;

		Destroy(food.gameObject);
	}

	private void HandleFoodCollected(object sender, TapewormSegment tapewormSegment)
	{
		Collectable food = sender as Collectable;

		if (food is Food && CollectedFood != null)
			CollectedFood(this, tapewormSegment);
		else if (food is Medicine && CollectedMedicine != null)
			CollectedMedicine(this, tapewormSegment);
		
		Destroy(food.gameObject);
	}

	private IEnumerator SpawnerCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(0.0f, 1.5f));

			if (Random.value < 0.7f)
				SpawnFood();
			else
				SpawnMedicine();
		}
	}
}
