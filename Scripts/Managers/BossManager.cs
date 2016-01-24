using UnityEngine;

namespace CompleteProject
{
	public class BossManager : MonoBehaviour
	{
		public PlayerHealth playerHealth;       
		public GameObject enemy;                
		public Transform[] spawnPoints;         
		public UpdateSlider healthSlider;

		GameObject hudCanvas;
		Transform bossSlider;
		float spawnTime = 80f;            
		
		void Awake()
		{
			hudCanvas = GameObject.FindGameObjectWithTag("HUDCanvas");
			bossSlider = hudCanvas.transform.Find("BossSlider");
			healthSlider = bossSlider.GetComponent<UpdateSlider>();
		}

		void Start ()
		{
			InvokeRepeating ("Spawn", spawnTime, spawnTime);
		}
		
		
		void Spawn ()
		{
			if(playerHealth.currentHealth <= 0f)
			{
				return;
			}

			healthSlider.SetActive ();
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			
			Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		}
	}
}