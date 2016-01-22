using UnityEngine;
using System.Collections;

namespace CompleteProject
{
public class DoubleDamageBuff : MonoBehaviour {

		public float buffDuration;
		public GameObject player;
		public GameObject gunBarrellEnd;
		public Material buffMaterial;

		PlayerShooting playerShooting;
		PlayerMovement playerMovement;

		Light gunColor;
		Color startLightColor;

		LineRenderer lineRendererMaterial;
		Material startLineRendererMaterial;

		ParticleSystem particleSystem;
		Color particleSystemStartColor;

		float timer;
		bool playerBuffed;

		void Awake()
		{
			playerShooting = gunBarrellEnd.GetComponent<PlayerShooting> ();
			playerMovement = player.GetComponent<PlayerMovement> ();

			gunColor = gunBarrellEnd.GetComponent<Light> ();
			startLightColor = gunColor.color;

			lineRendererMaterial = gunBarrellEnd.GetComponent<LineRenderer> ();
			startLineRendererMaterial = lineRendererMaterial.material;

			particleSystem = gunBarrellEnd.GetComponent<ParticleSystem> ();
			particleSystemStartColor = particleSystem.startColor;
		}

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
			if (playerBuffed) 
			{
				playerShooting.damagePerShot += playerShooting.damagePerShot;
				playerMovement.speed += 5f;
				gunColor.color = new Color(1,0,0,0.3f);
				lineRendererMaterial.material = buffMaterial;
				particleSystem.startColor = new Color(1,0,0);
			}
			else 
			{
				playerShooting.damagePerShot = playerShooting.damagePerShot / 2;
				playerMovement.speed -= 5f;
				gunColor.color = startLightColor;
				lineRendererMaterial.material = startLineRendererMaterial;
				particleSystem.startColor = particleSystemStartColor;
			}
		}


		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "DoubleDamage") 
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
