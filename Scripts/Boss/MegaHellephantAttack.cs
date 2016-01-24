using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class MegaHellephantAttack : MonoBehaviour
	{
		public GameObject specialAttackParticle;
		public GameObject[] particleSpawnPoint;

		public bool doingSpecial = false;

		float timeBetweenAttacks = 0.5f;     
		int attackDamage = 20; 

		Animator anim;                              
		GameObject player;                          
		PlayerHealth playerHealth;                 
		MegaHellephantHealth megaHellephantHealth;
		bool playerInRange;
		float timer; 
		Vector3 specialAttackSpawnPosition;
		GameObject[] specialAttackParticles;

		void Awake ()
		{
			player = GameObject.FindGameObjectWithTag ("Player");
			playerHealth = player.GetComponent <PlayerHealth> ();
			megaHellephantHealth = GetComponent<MegaHellephantHealth>();
			anim = GetComponent <Animator> ();
		}

 		void FixedUpdate()
		{
			gameObject.transform.Rotate(0,1,0);
		}   
		
		void OnTriggerEnter (Collider other)
		{
			if(other.gameObject == player)
			{
				playerInRange = true;
			}
		}
		
		
		void OnTriggerExit (Collider other)
		{
			if(other.gameObject == player)
			{
				playerInRange = false;
			}
		}
		
		
		void Update ()
		{
			timer += Time.deltaTime;

			if(timer >= timeBetweenAttacks && playerInRange && megaHellephantHealth.currentHealth > 0)
			{
				Attack ();
			}

			if(playerHealth.currentHealth <= 0)
			{
				anim.SetTrigger ("PlayerDead");
			}
		}
		
		
		void Attack ()
		{
			timer = 0f;

			if(playerHealth.currentHealth > 0)
			{
				playerHealth.TakeDamage (attackDamage);
			}
		}

		public void DoSpecialAttack()
		{
			doingSpecial = true;
			for (int i = 0; i < particleSpawnPoint.Length; i++) 
			{
				Transform particleTransform = particleSpawnPoint [i].GetComponent<Transform> ();
				Instantiate (specialAttackParticle, particleTransform.position, particleTransform.rotation);
			}
		}
	}
}