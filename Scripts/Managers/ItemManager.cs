using UnityEngine;

namespace CompleteProject
{
	public class ItemManager : MonoBehaviour 
	{
		public PlayerHealth playerHealth;
		public GameObject item;
		public float spawnTime = 20f;
		public Transform[] spawnPoints;


		void Start ()
		{
			InvokeRepeating ("Spawn", spawnTime, spawnTime);
		}

		void Spawn () 
		{
			if(playerHealth.currentHealth <= 0 || GameObject.FindGameObjectWithTag (item.tag) != null)
			{
				return;
			}

			int spawnPointIndex = Random.Range (0, spawnPoints.Length);

			Instantiate (item, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		} 
	}
}
