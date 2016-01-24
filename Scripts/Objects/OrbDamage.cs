using UnityEngine;
using System.Collections;

namespace CompleteProject
{
public class OrbDamage : MonoBehaviour {

	public int damagePerHit;
	public int lifeTime;

	GameObject player;
	Transform shield;
	PlayerHealth playerHealth;
	
	float timer;
	float particleSpeed = 5f;
	bool timerRunning = true;


	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		shield = player.transform.Find ("Shield");
		playerHealth = player.GetComponent<PlayerHealth> ();
	}

	void Update() 
	{
		if(timerRunning)
		{
			timer += Time.deltaTime;

			if(timer > lifeTime)
			{
				Destroy(gameObject);
				timerRunning = false;
			}
		} 
	}

	void FixedUpdate()
	{
		Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
		rb.MovePosition (rb.position + rb.transform.forward * particleSpeed * Time.deltaTime);
	} 

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) 
		{
			Explode();
		}

		if (other.transform == shield) 
		{
			Destroy(gameObject);
		}
	}

	void Explode()
	{
		if (playerHealth.currentHealth > 0) 
		{
			playerHealth.TakeDamage(damagePerHit);			
				Destroy(gameObject);
		}
	}
}
}
