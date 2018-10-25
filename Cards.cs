using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour {

	public List<GameObject> NormalCards;
	public List<GameObject> SpecialCards;
	public List<GameObject> EquipCards;
	public List<GameObject> Deck = new List<GameObject>();
	public List<GameObject> DrawPile = new List<GameObject>();
	public List<GameObject> HandCards = new List<GameObject>();
	public List<GameObject> DiscardPile = new List<GameObject>();
	public List<GameObject> ExhaustPile = new List<GameObject>();
	public List<GameObject> tempDrawList = new List<GameObject>();
	GameObject Attack;
	GameObject Block;
	GameObject CardObject;
	GameObject DraggedObj;
	public GameObject DrawPilePos;
	public GameObject DiscardPilePos;
	public GameObject ExhaustPilePos;
	public GameObject HandPilePos;
	public GameObject GameManager;
	public GameObject TheMap;
	Vector3 DraggedObjPos;
	public bool beginBattle;
	bool deckIsGen;
	bool battleEnd;
	public bool DrawCards;

	public bool DrawingCards;
	int CardDrawLimit;
	int CardDrawMaxLimit;
	int endloop;


	void Start () {
	TheMap.SetActive(false);
	CardDrawLimit = 5;
	CardDrawMaxLimit = 15;
		Attack = GameObject.Find ("Attack");
		Block= GameObject.Find ("Block");

				if(Attack != null)
				{
					for (int b = 0; b <= 4; b++)
					{
					CardObject = (GameObject)Instantiate (Attack, DrawPilePos.transform.position, DrawPilePos.transform.rotation);
					CardObject.name = "Attack";
					CardObject.tag = "Card";
					CardObject.SetActive (false);
					Deck.Add (CardObject);
					}
				}

	
				if(Block != null)
				{
					for (int b = 0; b <= 4; b++)
					{
					CardObject = (GameObject)Instantiate (Block, DrawPilePos.transform.position, DrawPilePos.transform.rotation);
					CardObject.name = "Block";
					CardObject.tag = "Card";
					CardObject.SetActive (false);
					Deck.Add (CardObject);
					}
				}





		if(Deck.Count >= 10)
		{
			//populate drawpile form deck and randomize drawpile
			List<int> tempList = new List<int>();
			for (int i = 0; i < Deck.Count + 1; i++) {
				int randomIndex = Random.Range(0, Deck.Count);
				if (!tempList.Contains (randomIndex))
				{
					
					tempList.Add (randomIndex);
					DrawPile.Add(Deck[randomIndex]);
				}
				else if (tempList.Contains (randomIndex) & tempList.Count < Deck.Count )
				{
					
				i--;
				}


			}

		deckIsGen = true;
		beginBattle = true;
		DrawCards = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		

	}


	public IEnumerator DiscardCardF(GameObject CardObjectF, GameObject cardDesF)
	{
		Quaternion originalRotation = CardObjectF.transform.rotation;
		while (Vector3.Distance (CardObjectF.transform.position, cardDesF.transform.position) > 0.05f & endloop < 500)
		{

			endloop++;
			if (endloop > 450)
			{
				print ("stopping infinite loop, look for error");
			}

			CardObjectF.transform.position = Vector3.MoveTowards (CardObjectF.transform.position, cardDesF.transform.position, 25f * Time.deltaTime);
			CardObjectF.transform.Rotate(Vector3.forward * -1000f *Time.deltaTime);

			yield return null;
		}

		CardObjectF.transform.rotation = originalRotation;

		if (cardDesF.name == "DiscardPileImg")
		{
			StartCoroutine(ScalePile (DiscardPilePos));
			CardObjectF.SetActive (false);
			GameManager.GetComponent<Cards> ().DiscardPile.Add (CardObjectF);
			GameManager.GetComponent<Cards> ().HandCards.Remove (CardObjectF);

		}
		else if (cardDesF.name == "HandPilePos")
		{

		}

		else if (cardDesF.name == "DrawPileImg")
		{

			CardObjectF.SetActive (false);

		}

		else if (cardDesF.name == "ExhaustPilePos")
		{

			GameManager.GetComponent<Cards> ().ExhaustPile.Add (CardObjectF);
			CardObjectF.SetActive (false);
			GameManager.GetComponent<Cards> ().HandCards.Remove (CardObjectF);

			//}
		}

		yield return null;

	}

	public IEnumerator DrawCardsF()
	{
		if(deckIsGen == true & beginBattle == true & battleEnd == false)
		{
			DrawingCards = true;
	//	print ("starting DrawCardsF");
	//if drawpile and hand is empty, get all cards from discard pile to drawpile
			//move cards from discard pile to draw pile if drawpile and hand are empty
			if (DrawPile.Count == 0 & HandCards.Count == 0) 
			{	
				int discardPileint = DiscardPile.Count;
				//int inc = 0;
				//populate drawpile form deck and randomize drawpile
				List<GameObject> tempListGO = new List<GameObject>();
				List<int> tempList = new List<int>();
				for (int i = 0; i < DiscardPile.Count + 1 ; i++) {
					int randomIndex = Random.Range(0, DiscardPile.Count );
					if (!tempList.Contains (randomIndex))
					{

						tempList.Add (randomIndex);
					tempListGO.Add (DiscardPile [randomIndex]);

//					print (randomIndex);
						//DrawPile.Add(Deck[randomIndex]);
					}
					else if (tempList.Contains (randomIndex) & tempList.Count < DiscardPile.Count )
					{

						i--;
					}


				}



				for (int i = DiscardPile.Count; DiscardPile.Count > 0; i--)
				{

					Quaternion originalRotation = DiscardPile [i - 1].transform.rotation;

					DiscardPile [i - 1].SetActive (true);
					StartCoroutine(ScalePile (DiscardPilePos));
					DiscardPile [i - 1].transform.localScale = new Vector3(0.15f,0.15f, 1f);
					//DiscardPile [i - 1].transform.localScale.y = fifteen;
					while (Vector3.Distance (DiscardPile[i-1].transform.position, DrawPilePos.transform.position ) > 0.05f)
					{

						DiscardPile[i-1].transform.position = Vector3.MoveTowards (DiscardPile[i-1].transform.position, DrawPilePos.transform.position, 100f * Time.deltaTime);

						DiscardPile[i-1].transform.Rotate(Vector3.forward * 2000f *Time.deltaTime);

						yield return null;
					}
					StartCoroutine(ScalePile (DrawPilePos));
					DiscardPile [i - 1].SetActive (false);
					DiscardPile [i - 1].transform.localScale = new Vector3(0.30f,0.30f, 1f);
					DiscardPile [i - 1].transform.rotation = originalRotation;
			//	print ("add to drawpile : " + tempList [i - 1]);
					DrawPile.Add(tempListGO[i-1]);
					DiscardPile.Remove (DiscardPile[i-1]);
				}

			}
	//if hand is empty but there are cards at drawpile, reset templist and start drawcards
			else if(HandCards.Count == 0 & DrawPile.Count > 0)
			{
				DrawCards = true;

			}
	//drawing cards if drawpile has cards
			if(DrawCards == true & DrawPile.Count != 0)
			{

				int c = 0;
				int dpc = DrawPile.Count - 1 ;
				//draw cards in hand
				for (int b = dpc; HandCards.Count < CardDrawLimit; b--)
				{

					endloop = 0;
					if(DrawPile [b] != null)
					{
						Quaternion originalRotation = DrawPile [b].transform.rotation;
				
						if(HandCards.Count == 0)
						{
							DrawPile [b].transform.position = DrawPilePos.transform.position;

							StartCoroutine(ScalePile (DrawPilePos));

							DrawPile [b].SetActive (true);

						
							while (Vector3.Distance (DrawPile [b].transform.position, new Vector3 (-2, -4f, -3f)) > 0.05f & endloop < 500)
							{
								endloop++;
								if (endloop > 450)
								{
									print ("stopping infinite loop, look for error #1");
								}
								DrawPile [b].transform.position = Vector3.MoveTowards (DrawPile [b].transform.position, new Vector3 (-2, -4f, -3f), 25f * Time.deltaTime);
								DrawPile [b].transform.Rotate(Vector3.forward * -1000f *Time.deltaTime);
								yield return null;
							}

							DrawPile [b].transform.rotation = originalRotation;
						}

						else if (HandCards.Count != 0)
						{

							DrawPile [b].transform.position = DrawPilePos.transform.position;
							StartCoroutine(ScalePile (DrawPilePos));
							DrawPile [b].SetActive (true);
						
							while (Vector3.Distance (DrawPile [b].transform.position,new Vector3 (HandCards [c-1].transform.position.x + 1, -4f, HandCards [c-1].transform.position.z - 1)) > 0.05f & endloop < 500)
							{
								endloop++;
								if (endloop > 450)
								{
									print ("stopping infinite loop, look for error #2");
								}
								DrawPile [b].transform.position = Vector3.MoveTowards (DrawPile [b].transform.position, new Vector3 (HandCards [c-1].transform.position.x + 1, -4f, HandCards [c-1].transform.position.z - 1), 25f * Time.deltaTime);
								DrawPile [b].transform.Rotate(Vector3.forward * -1000f *Time.deltaTime);
								yield return null;
							}
							DrawPile [b].transform.rotation = originalRotation;

						}


						HandCards.Add (DrawPile [b]);
						DrawPile.Remove (DrawPile [b]);

						//print("hand: " + HandCards.Count + " / card draw limit: " + CardDrawLimit);
						c++;
					}
					if(HandCards.Count == CardDrawLimit)
					{
					//	print("drawcards = false");
						DrawCards = false;
						tempDrawList.Clear();
						GameManager.GetComponent<CardEffect> ().LastCardObject = null;
						//print (DrawCards);
					}
				}


			}


			


			}
	DrawingCards = false;
	yield return null;
	}


	IEnumerator ScalePile(GameObject pile)
	{
		Vector3 originalScale = pile.transform.localScale;
		Vector3 destinationScale = new Vector3(0.40f, 0.40f, 1f);
		 
		//print ("resizing");

		float currentTime = 0.0f;

		do
		{
			pile.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / 0.2f);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= 0.2f);

		do
		{
			pile.transform.localScale = Vector3.Lerp(destinationScale, originalScale,   0.2f / currentTime);
			currentTime -= Time.deltaTime;
			yield return null;
		} while (0.0f <= currentTime);

	
	}
}
