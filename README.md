#Survival Shooter

##Setting Up

In this repo you will find the [Survival Shooter folder](SurvivalShooter) which contains a zip file with the game executable. To run the game you will need to [**download the repo as a zip**](https://github.com/Gianni89/survivalshooter/archive/master.zip), navigate to the Surivival Shooter folder and unzip the Survival Shooter Game zip file, which contains the game files and executable.

##Introduction

For this project I decided to use one of the Unity tutorials as the base, and picked the [survival shooter tutorial](https://www.assetstore.unity3d.com/en/#!/content/40756). I completed the tutorial building the project from scratch to refresh using certain concepts and started adding my own contributions from there.

The tutorial ends with player character able to run around and shoot, with three types of enemies spawning repeatedly into the game.

##Expanding

Currently the player loses health when enemies collide with the player character and there is no way to recover. We will add med packs to the level that will spawn at a set interval, at one of a set of locations.

In [playerHealth.cs](Scripts/Player/PlayerHealth.cs) a HealPlayer method was added:

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

and a simple medpack object was made with a medpack [script](Scripts/Items/MedPack.cs):

```c#
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
```

To spawn the medpack a generic [Item Manager](Scripts/Managers/ItemManager.cs) was created, that could also spawn in other items we might create. The manager makes sure that only one item per type is in the map:

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

These work quite nicely, the player can now kite the mobs around and pick up health when needed. The mobs will still tend to pile up and overwhelm the player with time however, so it might be nice to have some buffs that spawn that can help the player out. As a test we'll add in a "double damage" buff that lasts for a short period of time, changes the gun's color to red and increases the player's speed.

##Adding Double Damage Buff

For the damage buff, we want to be able to set how long the buff will last for after the player has collected it, and give some visual feedback to the player that the buff is either active or ended. This was done in the Double Damage buff [script:](Scripts/Player/DoubleDamageBuff.cs)

```c#
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
```

The player can now run around, find buffs and medpacks and survive for quite a while. However, the game can become quite stale, so lets add a boss encounter that adds a bit more challenge for the player. The boss will chase the player leaving behind a toxic trail that will damage the player if they stand in it. It will also periodically teleport to the centre of the map and perform a special attack, which shoots out dangerous particles in a spiral.

##Adding a Boss Encounter


