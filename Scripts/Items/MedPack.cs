using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class MedPack : MonoBehaviour {

		int amountToHeal = 40;

		GameObject player;
		PlayerHealth playerhealth;

		void Awake()
		{
			player = GameObject.FindGameObjectWithTag ("Player");
			playerhealth = player.GetComponent<PlayerHealth>();
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag ("Player"))
			{
				playerhealth.HealPlayer(amountToHeal);	
				gameObject.SetActive(false);
			}
		}
	}
}
