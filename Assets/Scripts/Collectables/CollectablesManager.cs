using UnityEngine;
using System.Collections;

public class CollectablesManager : MonoBehaviour
{
	private static readonly Vector3 InitialPosition = new Vector3(1.0f, 0.0f, 20.0f);

	public Food m_foodPrefab;
	
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
		food.Initialize(3.0f);

		food.ReachedToEnd += HandleFoodReachedToEnd;
		food.Collected += HandleFoodCollected;
	}

	private void HandleFoodReachedToEnd(object sender)
	{
		Collectable food = sender as Collectable;

		Destroy(food.gameObject);
	}

	private void HandleFoodCollected(object sender)
	{
		Collectable food = sender as Collectable;
		
		Destroy(food.gameObject);
	}

	private IEnumerator SpawnerCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(1.0f);

			SpawnFood();
		}
	}
}
