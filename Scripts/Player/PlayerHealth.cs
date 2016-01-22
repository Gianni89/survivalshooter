using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
	public class PlayerHealth : MonoBehaviour
	{
		public int startingHealth = 100;
		public int currentHealth;
		public Slider healthSlider;
		public Image damageImage;
		public AudioClip deathClip;
		public float flashSpeed = 5f;
		public Color flashColour = new Color (1f, 0f, 0f, 0.1f);
		public ShieldBuff shieldBuff;


		Animator anim;
		AudioSource playerAudio;
		PlayerMovement playerMovement;
		PlayerShooting playerShooting;
		bool isDead;
		bool damaged;
		UpdateSlider updateSlider;


		void Awake ()
		{
			anim = GetComponent <Animator> ();
			playerAudio = GetComponent <AudioSource> ();
			playerMovement = GetComponent <PlayerMovement> ();
			playerShooting = GetComponentInChildren <PlayerShooting> ();
			shieldBuff = GetComponent<ShieldBuff> ();
			updateSlider = healthSlider.GetComponent<UpdateSlider> ();

			currentHealth = startingHealth;
		}


		void Update ()
		{
			if (damaged) 
			{
				damageImage.color = flashColour;
			} 
			else 
			{
				damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
			}

			damaged = false;
		}


		public void TakeDamage (int amount)
		{
			if (!shieldBuff.playerBuffed) 
			{
				damaged = true;
				currentHealth -= amount;
				updateSlider.SetSlider (currentHealth);
				playerAudio.Play ();

				if (currentHealth <= 0 && !isDead) 
				{
					Death ();
				}
			}
		}


		void Death ()
		{
			isDead = true;

			playerShooting.DisableEffects ();

			anim.SetTrigger ("Die");

			playerAudio.clip = deathClip;
			playerAudio.Play ();

			playerMovement.enabled = false;
			playerShooting.enabled = false;
		}


		public void RestartLevel ()
		{
			Application.LoadLevel (Application.loadedLevel);
		}

		public void HealPlayer (int amount)
		{
			if (currentHealth + amount > startingHealth) 
			{
				currentHealth = startingHealth;
			} 
			else 
			{
				currentHealth += amount;
			}

			updateSlider.SetSlider (currentHealth);
		}
	}
}