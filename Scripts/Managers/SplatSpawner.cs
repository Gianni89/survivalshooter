using UnityEngine;
using System.Collections;

namespace CompleteProject
{
public class SplatSpawner : MonoBehaviour {

	public GameObject splat;

	void Start()
		{
			InvokeRepeating ("SpawnSplat", 1f, 1f);
		}

		void SpawnSplat()
		{
			Instantiate (splat, gameObject.GetComponent<Transform> ().position, gameObject.GetComponent<Transform> ().rotation);
		}

	}
}
