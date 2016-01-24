using UnityEngine;
using System.Collections;

namespace CompleteProject
{
public class SplatSpawner : MonoBehaviour {

	public GameObject splat;
	public GameObject boss;

	float timer = 0f;
	float timeBetweenSpawns = 1f;

	MegaHellephantAttack specialAttack;

		void Awake()
		{
			specialAttack = boss.GetComponent<MegaHellephantAttack> ();
		}

		void Update()
		{
			timer += Time.deltaTime;

			if (timer > timeBetweenSpawns && !specialAttack.doingSpecial) 
			{
				SpawnSplat();
			}
		}

		void SpawnSplat()
		{
			timer = 0f;
			Instantiate (splat, gameObject.GetComponent<Transform> ().position, gameObject.GetComponent<Transform> ().rotation);
		}

	}
}
