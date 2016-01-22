#Survival Shooter

##Setting Up

In this repo you will find the Survival Shooter folder which contains a zip file with the game executable. Unzipping and running the file will launch the game.

##Introduction

For this project I decided to use one of the Unity tutorials as the base, and picked the [survival shooter tutorial](https://www.assetstore.unity3d.com/en/#!/content/40756). I completed the tutorial building the project from scratch to refresh using certain concepts and started adding my own contributions from there.

The tutorial ends with player character able to run around and shoot, with three types of enemies spawning repeatedly into the game.

##Expanding

Currently the player loses health when enemies collide with the player character and there is no way to recover. We will add med packs to the level that will spawn at a set interval, at one of a set of locations.

In [playerHealth.cs](link) a HealPlayer method was added:

```c#
public void HealPlayer(int amount)
		{	
			if (currentHealth + amount > startingHealth) 
			{
				currentHealth = startingHealth;
			}
			else
			{
				currentHealth += amount;
			}
			healthSlider.value = currentHealth;
		}
    }
```

and a simple medpack object was made with a medpack [script](link):

```c#
{
	public class MedPack : MonoBehaviour {

		public int amountToHeal;

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
			}
		}
	}
}
```

To spawn the medpack a generic [Item Manager](link) was created, that could also spawn in other items we might create. The manager makes sure that only one item per type is in the map:

```c#
public class ItemManager : MonoBehaviour 
	{
		public PlayerHealth playerHealth;
		public GameObject item;
		public float spawnTime = 20f;
		public Transform[] spawnPoints;


		void Start ()
		{
			InvokeRepeating ("Spawn", spawnTime, spawnTime);
		}

		void Spawn () 
		{
			if(playerHealth.currentHealth <= 0 || GameObject.FindGameObjectWithTag (item.tag) != null)
			{
				return;
			}

			int spawnPointIndex = Random.Range (0, spawnPoints.Length);

			Instantiate (item, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		} 
	}
```

These work quite nicely, the player can now kite the mobs around and pick up health when needed. The mobs will still tend to pile up and overwhelm the player with time however, so it might be nice to have some buffs that spawn that can help the player out. As a test we'll add in a "double damage" buff that lasts for a short period of time, changes the gun colors to red and increases the player's speed.

##Adding Double Damage Buff


