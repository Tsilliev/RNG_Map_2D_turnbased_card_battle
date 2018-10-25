using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour {
	//public int CardEffectInt;
	// Use this for initialization
	public GameObject GameManager;
	public GameObject DrawPile;
	public GameObject DiscardPile;
	public GameObject ExhaustPile;
	public GameObject HandPile;
	public GameObject Hero;
	int endloop;
	GameObject CardDestination;

	public List<GameObject> MovingCardList = new List<GameObject>();
	public List<GameObject> DiscardPosList = new List<GameObject>();
	int WeaklingHP;
	int HeroHP;

	public bool MoveCard;
	public bool CoroutineRunning;
	public GameObject LastCardObject;
	float offsetsc;


	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(MoveCard == true)
		{
			 

			//MoveCardF (cardObjcardsc,CardDestination);


			//MovingCardList.Clear();
			//DiscardPosList.Clear();
		}
	}

	public void ActivateCard(GameObject cardObjsc,GameObject cardDes)
	{
	
		if(cardObjsc.name == "Attack")
		{
			if(GameManager.GetComponent<Battle> ().SpawnedEnemies.Count > 1)
			{

			GameManager.GetComponent<Battle> ().ChooseTarget = true;

			}
			else if (GameManager.GetComponent<Battle> ().SpawnedEnemies.Count == 1)
			{
			GameManager.GetComponent<Battle> ().DamageMonster(6,GameManager.GetComponent<Battle> ().SpawnedEnemies[0]);

			}

		GameManager.GetComponent<Battle> ().HeroActionPoints -= 1;
		GameManager.GetComponent<Battle> ().HeroAPText.text = GameManager.GetComponent<Battle> ().StartAP + " / " + GameManager.GetComponent<Battle> ().HeroActionPoints;

		}
		if(cardObjsc.name == "Block")
		{
		GameManager.GetComponent<Battle> ().HeroArmor += 6;
		GameManager.GetComponent<Battle> ().HeroArmorText.text = GameManager.GetComponent<Battle> ().HeroArmor + "";
		GameManager.GetComponent<Battle> ().HeroActionPoints -= 1;
		GameManager.GetComponent<Battle> ().HeroAPText.text = GameManager.GetComponent<Battle> ().StartAP + " / " + GameManager.GetComponent<Battle> ().HeroActionPoints;
		}

		//discard card
		StartCoroutine(GameManager.GetComponent<Cards> ().DiscardCardF (cardObjsc,cardDes));

	}


}
