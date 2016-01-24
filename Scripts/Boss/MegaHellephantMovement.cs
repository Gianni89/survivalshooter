using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class MegaHellephantMovement : MonoBehaviour
	{
		Transform player;              
		PlayerHealth playerHealth;      
		MegaHellephantHealth megaHellephantHealth;
		MegaHellephantAttack megaEllephantAttack;
		NavMeshAgent nav;              

		float chaseTimer = 0f;
		float chaseDuration = 15f;
		float specialTimer = 0f;
		float specialDuration = 15f;
		float numberOfSpecialAttacks = 60f;


		bool shouldSpecialAttack = false;
		
		void Awake ()
		{
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			playerHealth = player.GetComponent <PlayerHealth> ();
			megaHellephantHealth = GetComponent <MegaHellephantHealth> ();
			megaEllephantAttack = GetComponent<MegaHellephantAttack> ();
			nav = GetComponent <NavMeshAgent> ();
		}
		
		
		void Update ()
		{
			if (shouldSpecialAttack) 
			{
				DoSpecial();
			}
			else 
			{
				DoChase();
			}
		}

		void DoSpecial()
		{
			specialTimer += Time.deltaTime; 
		
			if (specialTimer > specialDuration)
			{
				specialTimer = 0f;
				chaseTimer = 0f;
				shouldSpecialAttack = false;
			}
		}

		void DoChase()
		{
			chaseTimer += Time.deltaTime;
			
			if (chaseTimer < chaseDuration) {
				ChasePlayer ();
			} 
			else 
			{
				nav.enabled = false;
				shouldSpecialAttack = true;
				StartCoroutine(SpecialAttack());
			}
		}

		void ChasePlayer()
		{
			nav.enabled = true;

			if (megaHellephantHealth.currentHealth > 0 && playerHealth.currentHealth > 0) 
			{
				nav.SetDestination (player.position);
			} 
			else 
			{
				nav.enabled = false;
			}
		}

		IEnumerator SpecialAttack()
		{
			gameObject.transform.position = new Vector3 (0, 0, 0);

			float timeBetweenSpecialAttacks = specialDuration/numberOfSpecialAttacks;

			for (int i = 0; i <= numberOfSpecialAttacks; i++)
			{
				yield return new WaitForSeconds (timeBetweenSpecialAttacks);
			
				megaEllephantAttack.DoSpecialAttack ();
			}
			megaEllephantAttack.doingSpecial = false;
		}
	}
}