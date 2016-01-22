using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class SplatDamage : MonoBehaviour {
		
		public int damagePerTick;
		public float tickTime;
		
		GameObject player;
		PlayerHealth playerHealth;

		bool shouldDamage = false;
		float timer;
		
		void Awake () {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerHealth = player.GetComponent<PlayerHealth> ();
		}

		void Update()
		{
			timer += Time.deltaTime;
			if (timer >= tickTime && shouldDamage) 
			{
				Damage();
			}
		}
		
		void OnTriggerEnter (Collider other)
		{
			if (other.gameObject == player) 
			{
				shouldDamage = true;
			}
		}

		void OnTriggerExit (Collider other)
		{
			if (other.gameObject == player) 
			{
				shouldDamage = false;
			}
		}

		
		void Damage()
		{
			timer = 0f;

			if (playerHealth.currentHealth > 0) 
			{
				playerHealth.TakeDamage(damagePerTick);
			}
		}
	}
}
