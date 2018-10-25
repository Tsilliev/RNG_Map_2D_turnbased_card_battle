using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour {


	public GameObject GameManager;
	public GameObject Hero;
	public GameObject Enemy;
	public GameObject HeroShield;
	public GameObject TheMap;

	public List<GameObject> EnemyPosList = new List<GameObject>();
	public List<GameObject> SpawnedEnemies = new List<GameObject>();
	public List<int> EnemiesHP = new List<int>();
	public List<int> EnemiesDmg = new List<int>();
	public List<int> EnemiesShield = new List<int>();
	GameObject DraggedObj;


	public Image heroHPBar;
	public Image enemyHPBar;


	public Text HeroHPText;
	public Text HeroArmorText;
	public Text EnemyHPText;
	public Text EnemyArmorText;
	public Text HeroAPText;
	public bool AllbehavChosen;
	public float HeroArmor;
	float HeroStartHPbattle;
	public float HeroHPbattle;
	float EnemyStartHP;
	float EnemyHP;
	float EnemyArmor;

	public int HeroActionPoints;
	public int StartAP;
	int Diff;


	Vector3 DraggedObjPos;
	public Button EndTurn;

	public bool NewBattle;
	public bool EnemysTurn;
	//public bool behavChosen;
	public bool execDone = true;
	public bool rdmran; 
	public bool ChooseTarget;
	int behavs = 0;
	public bool forloopran;
	public bool AttackingHero;
	public bool ChoosingState;
	public bool FadingState;
	public bool TurnRunning;
	public bool AnimDone0;
	public bool AnimDone1;
	public bool AnimDone2;
	public bool AnimDone3;
	public bool AnimDone4;
	Vector3 chooseStateStartScale = new  Vector3 (1f, 1f, 1f);
	Vector3 chooseStateDestScale = new  Vector3 (10f, 10f, 10f);
	void Start () {
		  AnimDone0 = true;
		  AnimDone1 = true;
		  AnimDone2 = true;
		  AnimDone3 = true;
		  AnimDone4 = true;

		execDone = true;
		NewBattle = true;
	EnemysTurn = false;
		for (int i = 0; i < 5; i++)
		{
			EnemyPosList.Add (GameObject.Find ("EnemyPos" + i) );
		}

//	Enemy.transform.position = new Vector3 (EnemyPosList [Random.Range (0, EnemyPosList.Count)].transform.position.x, -2, -3);

	}

	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//if battle starts - Generate and Randomize Drawpile
			//drawcards if hand is empty

		if(GameManager.GetComponent<Cards> ().HandCards.Count == 0 & GameManager.GetComponent<Cards> ().DrawingCards == false)
		{
			//GameManager.GetComponent<Cards> ().DrawCardsF ();
			StartCoroutine (GameManager.GetComponent<Cards> ().DrawCardsF ());

		}


		if(NewBattle == true)
		{
		EnemysTurn = false;
		execDone = true;
		HeroShield.SetActive (false);
		HeroStartHPbattle = Hero.GetComponent<Stats> ().HeroHP;
		HeroHPbattle = HeroStartHPbattle;
		HeroHPText.text = HeroHPbattle + "/" + HeroStartHPbattle;
		HeroActionPoints = Hero.GetComponent<Stats> ().HeroAP;
		StartAP = Hero.GetComponent<Stats> ().HeroAP;;
		HeroAPText.text = HeroActionPoints + " / " + StartAP;

		Diff = Hero.GetComponent<Stats> ().DiffX1;
//		DiffX3 = Diff * 3;

		GenerateEnemies ();
		NewBattle = false;
		}

		if (HeroArmor > 0)
		{
		HeroShield.SetActive (true);
		//heroHPBar.color = Color.blue;
			heroHPBar.color = new Color(0f,0f,0.6f);
		}
		else
		{
		HeroShield.SetActive (false);
		//heroHPBar.color = Color.red;
			heroHPBar.color = new Color(0.6f,0f,0f);
		}


		if (EnemysTurn == false & forloopran == false & execDone == true)
		{
			for (int z = 0; z < SpawnedEnemies.Count; z++)
			{
				StartCoroutine(ChooseState0(SpawnedEnemies[z],z + 2));
			SpawnedEnemies [z].GetComponent<WStats> ().WAR = 0;
			}
		

			forloopran = true;
		StartCoroutine (NullifyHeroArmor ());
		}
		else if (EnemysTurn == true)
		{

	
			for (int i = 0; i < GameManager.GetComponent<Cards> ().HandCards.Count; i++)
			{

				StartCoroutine (GameManager.GetComponent<Cards> ().DiscardCardF (GameManager.GetComponent<Cards> ().HandCards [i], GameManager.GetComponent<Cards> ().DiscardPilePos));
				HeroActionPoints = StartAP;
				HeroAPText.text = StartAP + " / " + HeroActionPoints;
			}
			for (int z = 0; z < SpawnedEnemies.Count; z++)
			{
			if (SpawnedEnemies [z].GetComponent<WStats> ().prevState == "Attack")
			{


					StartCoroutine (NotifAnim0 (SpawnedEnemies [z], "Attack", z + 1));

			} else if (SpawnedEnemies [z].GetComponent<WStats> ().prevState == "Defend")
			{

					StartCoroutine (NotifAnim0 (SpawnedEnemies [z], "Defend", z + 1));
			}
			}

			AllbehavChosen = false;
			EnemysTurn = false;
			forloopran = false;
		}

	}

	public void EndTurnF()
	{

		EnemysTurn = true;
		/*
		for (int z = 0; z < SpawnedEnemies.Count; z++)
		{
			if (SpawnedEnemies [z].GetComponent<WStats> ().behavChosen)
			{
				behavs++;
			}
		}


		if(behavs == SpawnedEnemies.Count)
		{
			AllbehavChosen = true;
			behavs = 0;

*/
	}

	void Update () {
		//play and discard cards
		if (GameManager.GetComponent<Cards> ().DrawCards == false)
		{
			
			if (Input.GetMouseButtonDown (0))
			{
				//print ("button pressing down");
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if(hit.collider != null) //& GameManager.GetComponent<CardEffect> ().MoveCard == false)
				{
					if (hit.transform.gameObject.tag == "Card" & HeroActionPoints > -1)
					{

						DraggedObj = hit.transform.gameObject;
						DraggedObjPos = hit.transform.gameObject.transform.position;
					} 
					else if (hit.transform.gameObject.tag == "enemy" & ChooseTarget == true)
					{
						
						DamageMonster(6,hit.transform.gameObject);
						ChooseTarget = false;
					}

				} 

			}

		}
			if (Input.GetMouseButton(0))
			{
				if(DraggedObj != null)
				{

					OnMouseDrag (DraggedObj);
				}

			}
			else if (Input.GetMouseButtonUp(0))
			{

				OnMouseUp ();
				DraggedObj = null;
			}
	
		//if battle is over:
			//get winning screen to select rewards
			//if reward is selected background = false and reset/nullify values
		}

	//public void RandomBackground



	void OnMouseDrag(GameObject go)

	{

		Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		point.z = go.transform.position.z;

		go.transform.position = point;

		Cursor.visible = false;


	}



	void OnMouseUp()

	{

		Cursor.visible = true;
		if (DraggedObj != null)
		{
			if (DraggedObj.transform.position.y < 0.5)
			{
				DraggedObj.transform.position = DraggedObjPos;	
			}
			else if (DraggedObj.transform.position.y > 0.5)
			{

				GameManager.GetComponent<CardEffect> ().ActivateCard(DraggedObj,GameManager.GetComponent<CardEffect> ().DiscardPile);
			}

		}

	}

	IEnumerator NullifyHeroArmor()
	{
		yield return new WaitForSeconds(4f);
	HeroArmor = 0;
	}

	public void DamageHero(float dmgAmount, GameObject enemy)
	{
		
		
		StartCoroutine (AttackAnimation (enemy, -2f));

		GameObject Hero = GameObject.Find ("Hero");
		StartCoroutine (DamagedAnimation(Hero,-1f));
		float HeroHP = Hero.GetComponent<Stats> ().HeroHP;
		Image heroHPBar = Hero.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image> ();
		Text HeroHPText = Hero.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text> ();

		if(HeroArmor > 0 & dmgAmount <= HeroArmor)
		{
		HeroArmor -= dmgAmount;
		HeroArmorText.text = HeroArmor + "";

		}
		else if(HeroArmor > 0 & dmgAmount > HeroArmor)
		{
			float difference = dmgAmount - HeroArmor;
			HeroArmor = 0;
			HeroHPbattle -= difference;
		}
		else if(HeroArmor <= 0)
		{
			HeroHPbattle -= dmgAmount;
		}

		heroHPBar.fillAmount = HeroHPbattle / HeroHP;
		HeroHPText.text = HeroHP  + "/" + HeroHPbattle;
	




	}

	public void DamageMonster(int dmgAmount, GameObject target)
	{
		

		StartCoroutine (AttackAnimation (Hero, 2f));
		target.GetComponent<WStats> ().DamageMonsterF (dmgAmount);
		if(target != null)
		{
			StartCoroutine(DamagedAnimation (target, 0.5f));
		}

	}

	public IEnumerator AttackAnimation(GameObject attacker, float OffsetX)
	{
		execDone = false;
		Vector3 originalPosition = attacker.transform.position;
		Vector3 destinationPosition = new Vector3(attacker.transform.position.x + OffsetX, attacker.transform.position.y, attacker.transform.position.z);

		//print ("resizing");

		float currentTime = 0.0f;

		do
		{
			attacker.transform.position = Vector3.Lerp(originalPosition, destinationPosition, currentTime / 0.03f);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= 0.2f);

		do
		{
			attacker.transform.position = Vector3.Lerp(destinationPosition, originalPosition,   0.03f / currentTime);
			currentTime -= Time.deltaTime;
			yield return null;
		} while (0.0f <= currentTime);
		execDone = true;

	}

	public IEnumerator DamagedAnimation(GameObject attacker, float OffsetX)
	{

		if(attacker != null)
		{
			
		
		Vector3 originalPosition = attacker.transform.position;
		Vector3 destinationPosition = new Vector3(attacker.transform.position.x + OffsetX, attacker.transform.position.y, attacker.transform.position.z);

		//print ("resizing");

		float currentTime = 0.0f;

		do
		{
			attacker.transform.position = Vector3.Lerp(originalPosition, destinationPosition, currentTime / 0.03f);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= 0.2f);

		do
		{
			attacker.transform.position = Vector3.Lerp(destinationPosition, originalPosition,   0.03f / currentTime);
			currentTime -= Time.deltaTime;
			yield return null;
		} while (0.0f <= currentTime);

		}
	}
		

	public void GenerateEnemies()
	{
	int RandomEnemNum;
	RandomEnemNum = Random.Range (1, 5);
	GameObject spawnedEnemy;
	int RandomEnemFromList;

	RandomEnemFromList = Random.Range (0, GameManager.GetComponent<Enemies> ().WeaklingsObjList.Count);




	//	List<int> tempList = new List<int>();
		for (int i = 0; i < RandomEnemNum; i++)
		{

			spawnedEnemy = (GameObject)Instantiate (GameManager.GetComponent<Enemies> ().WeaklingsObjList[RandomEnemFromList], EnemyPosList[i].transform.position, EnemyPosList[i].transform.rotation);
			spawnedEnemy.SetActive (true);
			SpawnedEnemies.Add(spawnedEnemy);
			spawnedEnemy.transform.GetChild(0).transform.GetChild(2).GetComponent<SpriteRenderer> ().enabled = false;
			spawnedEnemy.transform.GetChild(0).transform.GetChild(3).GetComponent<SpriteRenderer> ().enabled = false;
			spawnedEnemy.GetComponent<WStats> ().ID = i;
			int RandomStateNum = Random.Range (0, 2);

			if(RandomStateNum == 0)
			{
				spawnedEnemy.GetComponent<WStats> ().prevState = "Defend";
			}
			else if(RandomStateNum == 1)
			{
				spawnedEnemy.GetComponent<WStats> ().prevState = "Attack";	
			}

		}

	}


	public IEnumerator ChooseState0(GameObject enemy, float delay)
	{

		

		while (AnimDone0 == false )
		{
			yield return null;
		}
		yield return new WaitForSeconds(delay);

		if (enemy.GetComponent<WStats> ().prevState == "Defend" )
		{
			enemy.transform.Find ("EnemyCanvas").transform.Find ("BlockNotif").transform.Find ("NotifARtext").gameObject.SetActive (false);
			enemy.transform.Find ("EnemyCanvas").transform.Find ("AttackNotif").transform.Find ("NBG1").gameObject.SetActive (true);

			GameObject atkobject = enemy.transform.GetChild(0).transform.GetChild(2).gameObject;

		atkobject.GetComponent<SpriteRenderer> ().enabled = true;
		float currentTime = 0.0f;

		do
		{
				atkobject.transform.localScale = Vector3.Lerp (chooseStateStartScale, chooseStateDestScale, currentTime / 1f);
			currentTime += Time.deltaTime;
				execDone = false;
			yield return null;
		} while (currentTime <= 0.7f);
		//yield return new WaitForSeconds (1.0f);

			enemy.GetComponent<WStats> ().behavChosen = true;
			enemy.GetComponent<WStats> ().prevState = "Attack";
	
			int dmgValue;
			if(enemy.name == "Skeleton(Clone)")
			{
				dmgValue = Random.Range (4, 8);
				EnemiesDmg.Add (dmgValue);
				enemy.transform.Find ("EnemyCanvas").transform.Find ("AttackNotif").transform.Find ("NBG1").transform.Find ("NBG2").transform.Find ("NotifATKtext").GetComponent<Text> ().text = dmgValue + "";

			}
			else if(enemy.name == "SkeletonMage(Clone)")
			{
				dmgValue = Random.Range (3, 6);
				EnemiesDmg.Add (dmgValue);
				enemy.transform.Find ("EnemyCanvas").transform.Find ("AttackNotif").transform.Find ("NBG1").transform.Find ("NBG2").transform.Find ("NotifATKtext").GetComponent<Text> ().text = dmgValue + "";
			}
			else if(enemy.name == "RedOgre(Clone)")
			{
				dmgValue = Random.Range (7, 14);
				EnemiesDmg.Add (dmgValue);
				enemy.transform.Find ("EnemyCanvas").transform.Find ("AttackNotif").transform.Find ("NBG1").transform.Find ("NBG2").transform.Find ("NotifATKtext").GetComponent<Text> ().text = dmgValue + "";
			}



		} else if (enemy.GetComponent<WStats> ().prevState == "Attack"  )
	{
			enemy.transform.Find ("EnemyCanvas").transform.Find ("BlockNotif").transform.Find ("NotifARtext").gameObject.SetActive (true);
			enemy.transform.Find ("EnemyCanvas").transform.Find ("AttackNotif").transform.Find ("NBG1").gameObject.SetActive (false);

			GameObject blkobject = enemy.transform.GetChild(0).transform.GetChild(3).gameObject;
			blkobject.GetComponent<SpriteRenderer> ().enabled = true;

	

		float currentTime = 0.0f;

		do
		{
				blkobject.transform.localScale = Vector3.Lerp (chooseStateStartScale, chooseStateDestScale, currentTime / 1f);
			currentTime += Time.deltaTime;

			yield return null;
		} while (currentTime <= 0.7f);

			enemy.GetComponent<WStats> ().behavChosen = true;
			enemy.GetComponent<WStats> ().prevState = "Defend";
			int shieldValue;
			if(enemy.name == "Skeleton(Clone)")
			{
				shieldValue = Random.Range (4, 8);
				EnemiesShield.Add (shieldValue);
				enemy.transform.Find ("EnemyCanvas").transform.Find ("BlockNotif").transform.Find ("NotifARtext").gameObject.GetComponent<Text> ().text = shieldValue + "";

			}
			else if(enemy.name == "SkeletonMage(Clone)")
			{
				shieldValue = Random.Range (3, 6);
				EnemiesShield.Add (shieldValue);
				enemy.transform.Find ("EnemyCanvas").transform.Find ("BlockNotif").transform.Find ("NotifARtext").gameObject.GetComponent<Text> ().text = shieldValue + "";

			}
			else if(enemy.name == "RedOgre(Clone)")
			{
				shieldValue = Random.Range (7, 14);
				EnemiesShield.Add (shieldValue);
				enemy.transform.Find ("EnemyCanvas").transform.Find ("BlockNotif").transform.Find ("NotifARtext").gameObject.GetComponent<Text> ().text = shieldValue + "";

			}

	} 
		//execDone = true;


	}



	public IEnumerator NotifAnim0(GameObject enemy, string Behav, float delay)
	{

	AnimDone0 = false;
		yield return new WaitForSeconds(delay);
	//	yield return new WaitForSeconds(6f);
		GameObject atkobject = enemy.transform.GetChild(0).transform.GetChild(2).gameObject;
		GameObject defobject = enemy.transform.GetChild(0).transform.GetChild(3).gameObject;

		if(Behav == "Attack" )
		{
			
		//	execDone = false;
			atkobject.GetComponent<SpriteRenderer> ().enabled = true;

			Vector3 originalScale = atkobject.transform.localScale;
			Vector3 destinationScale = new Vector3(7f, 7f, 1f);


			float currentTime = 0.0f;

			do
			{
				atkobject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / 1f);
				currentTime += Time.deltaTime;
				yield return null;
			} while (currentTime <= 0.7f);
			atkobject.transform.localScale = originalScale;
			DamageHero (EnemiesDmg[0],enemy);
			EnemiesDmg.Remove (EnemiesDmg[0]);
			execDone = true;
			StartCoroutine(NotifFadeOut (atkobject));
		}
		else if(Behav == "Defend")
		{
			execDone = false;
			defobject.GetComponent<SpriteRenderer> ().enabled = true;
			Vector3 originalScale = defobject.transform.localScale;
			Vector3 destinationScale = new Vector3(7f, 7f, 1f);


			float currentTime = 0.0f;

			do
			{
				defobject.gameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / 1f);
				currentTime += Time.deltaTime;
				yield return null;
			} while (currentTime <= 0.7f);

			defobject.transform.localScale = originalScale;
			enemy.GetComponent<WStats> ().WAR += EnemiesShield[0];
			EnemiesShield.Remove (EnemiesShield[0]);

			enemy.GetComponent<WStats> ().EnemyWARTextS.text = enemy.GetComponent<WStats> ().WAR + "";
			execDone = true;
			StartCoroutine(NotifFadeOut (defobject));

		}


		execDone = true;
		AnimDone0 = true;

	}

	public IEnumerator NotifFadeOut(GameObject img)
	{

		Vector3 originalScale = img.transform.localScale;
		Vector3 destinationScale = new Vector3(0.01f, 0.01f, 1f);

		//print ("resizing");

		float currentTime = 0.0f;

		do
		{
			img.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / 1f);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= 0.7f);


		img.transform.localScale = originalScale;

		img.GetComponent<SpriteRenderer> ().enabled = false;

		//execDone = true;
		//behavChosen = true;
	}


}
