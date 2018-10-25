using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WStats : MonoBehaviour {

	public int ID;
	public float WHP;
	public float WHPS;
	public float WAR = 0;
	public int WDMG;
	public bool behavChosen = false;
	public bool execDone = true;
	public Image enemyHPBarS;
	public Text EnemyHPTextS;
	public Text EnemyWARTextS;
	public GameObject EnemyShield;
	public GameObject GameManager;
	//private GameObject notifAtk;
	//private GameObject notifDef;
	public GameObject AttackNotif;
	public GameObject BlockNotif;
	public GameObject Hero;
	public int RandomStateNum;
	public float amplitude;
	public string prevState;
	public float rndFloatingSpeed;
	public List<string> tempBehav = new List<string>();
	public List<GameObject> IconsAboveHead = new List<GameObject>();

	// Use this for initialization
	void Start () {
		rndFloatingSpeed = Random.Range (0.5f, 1f);
		amplitude = 10f;
		enemyHPBarS = this.transform.Find ("EnemyCanvas").transform.Find ("EnemyHealthBG").transform.Find ("EnemyHealthBar").gameObject.GetComponent<Image> ();
		EnemyHPTextS = this.transform.Find ("EnemyCanvas").transform.Find ("EnemyHealthBG").transform.Find ("EnemyHPText").gameObject.GetComponent<Text> ();
		EnemyWARTextS = this.transform.Find ("EnemyCanvas").transform.Find ("EnemyShield").transform.Find ("EnemyArmorText").gameObject.GetComponent<Text> ();
		EnemyShield = this.transform.Find ("EnemyCanvas").transform.Find ("EnemyShield").gameObject;
		AttackNotif = this.transform.GetChild(0).transform.GetChild(2).gameObject;
		BlockNotif = this.transform.GetChild(0).transform.GetChild(3).gameObject;
		GameManager = GameObject.Find ("GameManager");
		if(this.name == "Skeleton(Clone)")
		{
			WHPS = Random.Range (10, 20);
			WHP = WHPS;
			this.transform.Find ("EnemyCanvas").transform.Find ("EnemyHealthBG").transform.Find ("EnemyHPText").gameObject.GetComponent<Text> ().text = WHP  + "/" + WHPS;

		}
		else if(this.name == "SkeletonMage(Clone)")
		{
			WHPS = Random.Range (7, 15);
			WHP = WHPS;
			this.transform.Find ("EnemyCanvas").transform.Find ("EnemyHealthBG").transform.Find ("EnemyHPText").gameObject.GetComponent<Text> ().text = WHP  + "/" + WHPS;

		}
		if(this.name == "RedOgre(Clone)")
		{
			WHPS = Random.Range (18, 40);
			WHP = WHPS;
			this.transform.Find ("EnemyCanvas").transform.Find ("EnemyHealthBG").transform.Find ("EnemyHPText").gameObject.GetComponent<Text> ().text = WHP  + "/" + WHPS;

		}


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (WAR > 0)
		{
			EnemyShield.SetActive (true);
			//enemyHPBarS.color = Color.blue;
			enemyHPBarS.color = new Color(0f,0f,0.6f);
		}
		else
		{
			EnemyShield.SetActive (false);
			//enemyHPBarS.color = Color.red;
			enemyHPBarS.color = new Color(0.6f,0f,0f);
		}

		float newY =  Mathf.Sin(Time.time * Mathf.PI * rndFloatingSpeed) * amplitude ;

		AttackNotif.transform.localPosition = new Vector3(AttackNotif.transform.localPosition.x,   230f + newY, AttackNotif.transform.localPosition.z) ;
		BlockNotif.transform.localPosition = new Vector3(BlockNotif.transform.localPosition.x,   230f + newY, BlockNotif.transform.localPosition.z) ;


	}

	public void DamageMonsterF(int dmgAmount ) 
	{
//		print ("executing DamageMonsterF");
		if (WAR > 0 & dmgAmount <= WAR)
		{
			WAR -= dmgAmount;
		EnemyWARTextS.text = WAR + "";
		} else if (WAR > 0 & dmgAmount > WAR)
		{
			float difference = dmgAmount - WAR;
			WAR = 0;
			WHP -= difference;
		} else if (WAR <= 0)
		{
			WHP -= dmgAmount;
		}

		if(WHP<1)
		{
		//GameManager.GetComponent<Battle> ().SpawnedEnemies.Remove (GameManager.GetComponent<Battle> ().SpawnedEnemies [ID]);
			GameManager.GetComponent<Battle> ().SpawnedEnemies.Remove (this.gameObject);
		StartCoroutine (Death ());
		
		}
		//print ("WHP " + WHP + " / WHPS " + WHPS);
		enemyHPBarS.fillAmount = WHP / WHPS;
		EnemyHPTextS.text = WHP + "/" + WHPS;

	}

	IEnumerator Death()
	{

		yield return new WaitForSeconds(0.5f);
		Destroy (this.gameObject);
	}





		



	public void CoChooseState()
	{
	//StartCoroutine (ChooseState ());
	}



}
