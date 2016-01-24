using UnityEngine;

namespace CompleteProject
{
	public class ItemManager : MonoBehaviour 
	{
		public PlayerHealth playerHealth;
		public GameObject item;
		public Transform[] spawnPoints;
		public float spawnTime;

		float timer = 0f;

		void Update ()
		{
			if (timer < spawnTime && GameObject.FindGameObjectWithTag (item.tag) == null) 
			{
				timer += Time.deltaTime;
			} 
			else 
			{
				Spawn();
			}
		}

		void Spawn () 
		{
			if (playerHealth.currentHealth <= 0 || GameObject.FindGameObjectWithTag (item.tag) != null) 
			{
				return;
			} 

			int spawnPointIndex = Random.Range (0, spawnPoints.Length);

			Instantiate (item, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);

			timer = 0f;
		} 
	}
}
