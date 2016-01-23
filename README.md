#Survival Shooter

##Setting Up

In this repo you will find the [Survival Shooter folder](SurvivalShooter) which contains a zip file with the game executable. To run the game you will need to [**download the repo as a zip**](https://github.com/Gianni89/survivalshooter/archive/master.zip), navigate to the Survival Shooter folder and unzip the Survival Shooter Game zip file, which contains the game files and executable.

##Introduction

For this project I decided to use one of the Unity tutorials as the base, and picked the [survival shooter tutorial](https://www.assetstore.unity3d.com/en/#!/content/40756). I completed the tutorial building the project from scratch to refresh my use of certain concepts and started adding my own contributions from there.

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
				gameObject.SetActive(false);
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

![](https://raw.githubusercontent.com/Gianni89/survivalshooter/master/gifs/Medpack.gif)

These work quite nicely, the player can now kite the mobs around and pick up health when needed. The mobs will still tend to pile up and overwhelm the player with time however, so it might be nice to have some buffs that spawn that can help the player out. As a test we'll add in a "double damage" buff that lasts for a short period of time, changes the gun's color to red and increases the player's speed.

##Adding Double Damage Buff

For the damage buff, we want to be able to set how long the buff will last for after the player has collected it, and give some visual feedback to the player that the buff is either active or ended. This was done in the Double Damage buff [script:](Scripts/Player/DoubleDamageBuff.cs)

![](https://raw.githubusercontent.com/Gianni89/survivalshooter/master/gifs/DoubleDamage.gif)

The player can now run around, find buffs and medpacks and survive for quite a while. However, the game can become quite stale, so lets add a boss encounter that adds a bit more challenge for the player. The boss will chase the player leaving behind a toxic trail that will damage the player if they stand in it. It will also periodically teleport to the centre of the map and perform a special attack, which shoots out dangerous particles in a spiral.

##Adding a Boss Encounter

For the boss encounter we needed a [boss movement script](Scripts/Boss/MegaHellephantMovement.cs) to change the behavior of the boss depending on which attack type it is performing. During the chase phase, the boss should navigate towards the player, and during the special attack phase, should teleport to the centre of the map and perform the special attack at repeated intervals for its duration. 

###Making the Special Attack

For the special attack a simple method was added to the [boss attack script](Scripts/Boss/MegaHellephantAttack.cs) which looked at an array of spawn points and spawn an orb at each point:

```c#
public void DoSpecialAttack()
		{
			doingSpecial = true;
			for (int i = 0; i < particleSpawnPoint.Length; i++) 
			{
				Transform particleTransform = particleSpawnPoint [i].GetComponent<Transform> ();
				Instantiate (specialAttackParticle, particleTransform.position, particleTransform.rotation);
			}
		}
```

The behaviour of the orbs was controlled by the [orb damage script](Scripts/Objects/OrbDamage.cs) which moved the orbs forward, destroying them and causing damage if they hit the player, and destroying them after a set time.

A rotation was also added to the boss during this attack to achieve the spiral of orbs.

![](https://raw.githubusercontent.com/Gianni89/survivalshooter/master/gifs/orb.gif)

###Making the Toxic Trail

For the toxic trail that the boss would leave behind a spawn point was added slightly behind the boss and a script added that would spawn the patches per set period of time. We wanted the patches to deal damage to the player if they are standing in them, and this behaviour was handled by the [splat damage script](scripts/objects/splatdamage.cs)

![](https://raw.githubusercontent.com/Gianni89/survivalshooter/master/gifs/splat.gif)

###Making the Shield

The shield behviour was made in much the same way as the double damage buff, and was handled by the [shield buff script](scripts/player/shieldbuff.cs). Some physics interactions were changed such that enemies bumped into the shield but environmental pieces did not.

![](https://raw.githubusercontent.com/Gianni89/survivalshooter/master/gifs/shield.gif)

##Additions

- Tweaks to spawn times, score value of enemies and enemy health to set difficulty
- Update Slider [script](scripts/managers/updateslider.cs) was created to dynamically change colour of slider based on health that will work for the player and boss. Health slider for bosses were added
- Some environmental props moved around to prevent boss enemies becoming stuck
- Added an introduction splash screen that triggers when players move for the first time handled by an [intro script](scripts/managers/intro.cs)
