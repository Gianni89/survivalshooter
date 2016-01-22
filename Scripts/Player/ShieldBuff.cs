using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class ShieldBuff : MonoBehaviour {
		
		public float buffDuration;
		public GameObject player;
		public GameObject shield;

		public bool playerBuffed;

		float timer;
		
		void Update () 
		{
			if (playerBuffed) 
			{
				timer -= Time.deltaTime;
				if ( timer <= 0)
				{
					playerBuffed = false;
					timer = buffDuration;
					BuffPlayer();
				}
			}
		}		
		
		void BuffPlayer()
		{
			shield.SetActive (playerBuffed);
		}
		
		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "Shield") 
			{
				if (!playerBuffed) 
				{
					timer = buffDuration;
					playerBuffed = true;
					BuffPlayer();
				}
				else 
				{
					timer += buffDuration;
				}
				
				other.gameObject.SetActive(false);
			}
		}
	}
}
