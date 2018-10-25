using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour {

	public bool GenMap;
	public bool MapGenerated;
	bool FoundFirst;
	bool FoundSecond;
	public bool relocbool;
	public GameObject StartPos;
	public GameObject SectorParent;
	public GameObject LineRenderGroup;
	public GameObject Weakling;
	public GameObject Monster;
	public GameObject Inn;
	public GameObject Rest;
	public GameObject BossLoc;
	public GameObject Treasure;
	public GameObject Backgrounds;
	GameObject BossLocation;
	GameObject Location;
	GameObject Location2;
	GameObject currentLoc;
	GameObject upperLoc;
	GameObject lastRowLoc;
	GameObject upperLastRowLoc;
	GameObject RNGupperLoc;
	GameObject ChosenLocation;
	public List<GameObject> locations = new List<GameObject>();
	public List<GameObject> LRG = new List<GameObject>();
	public List<int> listRN = new List<int>();
	public List<int> listRows = new List<int>();
	public List<bool> RelocatedBools = new List<bool>();
	public List<GameObject> LocationChance = new List<GameObject>();
	public int Randomsectors;
	public int a;
	int weaklingChance;
	int monsterChance;
	int innChance;
	int restChance;
	int treasureChance;
    int Vertical = 8; 
	int sectorCount;
	int d1;
	int Rand;
	int Horizontal = 8;
	int upperLocValue;
	int rowDif2;
	int uppRow;
	int currentRowValue;
	int currentRowValueDif;
	int upperLocValueDif;
	float prevRNGY;
	float prevRNGX;
	// Use this for initialization
	void Start () {
		
		}
	
	// Update is called once per frame
	void Update () {
		if (GenMap == true & MapGenerated == false) 
		{
		GenerateLocations();

		
		Invoke("GeneratePathways",1);
			Resize (Backgrounds);
		}
		
		if(GenMap == false )
		{
		DeleteMapF();
		}
	}

		void GenerateLocations()
	{
	int previousGeneratedLocations = 0;
	int GeneratedLocations = 0;

		//percentage to occur
		 weaklingChance = 60;
		 monsterChance = 15;
		 innChance = 10;
		 restChance = 15;
		 treasureChance = 10;
	

		for (int c = 0; c <= weaklingChance; c++)
		{
			LocationChance.Add (Weakling);
		}
		for (int g = 0; g <= monsterChance; g++)
		{
			LocationChance.Add (Monster);
		}
		for (int h = 0; h <= innChance; h++)
		{
			LocationChance.Add (Inn);
		}
		for (int j = 0; j <= restChance; j++)
		{
			LocationChance.Add (Rest);
		}
		for (int k = 0; k <= treasureChance; k++)
		{
			LocationChance.Add (Treasure);
		}

	for (int e = 0; e <= Vertical; e++)
	{

	if (e < Vertical)
	{


		
		//	print ("e: " + e);
		Horizontal = Random.Range (2, 5);
		GeneratedLocations = Horizontal;
		listRows.Add (Horizontal);	
		for (int d = 0; d < Horizontal; d++)
		{
			float RNGposY = Random.Range (-1.5f + prevRNGY, 0f);
			float RNGposX = Random.Range (-2f + prevRNGX, 0f);
			
			int chance = Random.Range (0, LocationChance.Count);
					if(e == 0)
			{
			Location = (GameObject)Instantiate (Weakling, new Vector3 (StartPos.transform.position.x + d + RNGposX, StartPos.transform.position.y + e + RNGposY, StartPos.transform.position.z), StartPos.transform.rotation);
				

			}
			else if(e == Vertical - 1)
			{
			Location = (GameObject)Instantiate (Rest, new Vector3 (StartPos.transform.position.x + d + RNGposX, StartPos.transform.position.y + e + RNGposY, StartPos.transform.position.z), StartPos.transform.rotation);
								
			}
			else
			{
			Location = (GameObject)Instantiate (LocationChance[chance], new Vector3 (StartPos.transform.position.x + d + RNGposX, StartPos.transform.position.y + e + RNGposY, StartPos.transform.position.z), StartPos.transform.rotation);	
			}
			Location.transform.parent = SectorParent.transform;
			d1++;
			Location.name = "Loc" + e + d;	
			locations.Add (Location);
			sectorCount++;
				prevRNGX = RNGposX * -1;
				prevRNGY = RNGposY * -1;
		}
		previousGeneratedLocations = previousGeneratedLocations + GeneratedLocations;
	}
	
		else if(e == Vertical)
		{

				BossLocation = (GameObject)Instantiate (BossLoc, new Vector3 (StartPos.transform.position.x + 1, StartPos.transform.position.y + e, StartPos.transform.position.z), StartPos.transform.rotation);
				BossLocation.transform.parent = SectorParent.transform;
				BossLocation.name = "BossLocation";
		}
	}
	MapGenerated = true;		
	}




	void GeneratePathways()
	{

	for (int v = 0; v <= Vertical; v++)
	{
		if (v < Vertical)
		{
				

			if (listRows.Count != v + 1)
			{
				upperLocValue = listRows [v + 1] - 1;

				uppRow = v + 1;
			}
			currentRowValue = listRows [v] - 1;

			for (int r = 0; r < listRows.Count; r++)
			{

				currentLoc = GameObject.Find ("Loc" + v + r);
				upperLoc = GameObject.Find ("Loc" + uppRow + r);
				RNGupperLoc = GameObject.Find ("Loc" + uppRow + Random.Range (r - 1, r + 2));

				if (currentLoc != null)
				{
						

					lastRowLoc = GameObject.Find ("Loc" + v + currentRowValue);
					upperLastRowLoc = GameObject.Find ("Loc" + uppRow + upperLocValue);


					int rowDif = (currentRowValue) - (upperLocValue);
					if (listRows.Count != v + 1)
					{
						rowDif2 = listRows [v] - listRows [v + 1];
						if (rowDif2 < 0)
						{
							rowDif2 = rowDif2 * -1;
						}
					}

					if (currentLoc != null & upperLoc != null)
					{

						GameObject nlr = new GameObject ();
						nlr.transform.parent = LineRenderGroup.transform;
						//nlr.transform.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, .5f);
						nlr.name = "LR" + v + r;
						LRG.Add (nlr);
						LineRenderer lr = nlr.AddComponent<LineRenderer> ();
						lr.startWidth = (0.05f);
						lr.endWidth = (0.05f);
						lr.material.color = Color.black;
						lr.material.color = new Color (1f, 1f, 1f, .5f);
						lr.SetPosition (0, currentLoc.transform.position);
						lr.SetPosition (1, upperLoc.transform.position);

					}
					int chanceOnetoSix = Random.Range (1, 6);

					if (currentLoc != null & RNGupperLoc != null & chanceOnetoSix == 5)
					{

						GameObject nlr = new GameObject ();
						nlr.transform.parent = LineRenderGroup.transform;	
						//nlr.transform.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, .5f);
						nlr.name = "LR" + v + r;
						LRG.Add (nlr);
						LineRenderer lr = nlr.AddComponent<LineRenderer> ();
						lr.startWidth = (0.05f);
						lr.endWidth = (0.05f);
						lr.material.color = Color.black;
						lr.material.color = new Color (1f, 1f, 1f, .5f);
						lr.SetPosition (0, currentLoc.transform.position);
						lr.SetPosition (1, RNGupperLoc.transform.position);

					}
				
				


					for (int d = 0; d < rowDif2; d++)
					{
					
						if (rowDif < 0 & upperLocValue >= 0 & currentRowValue >= 0 & GameObject.Find ("Loc" + uppRow + upperLocValueDif) != null & lastRowLoc != null)
						{
							

							GameObject nlr = new GameObject ();
							nlr.transform.parent = LineRenderGroup.transform;	
							//nlr.transform.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, .5f);
							nlr.name = "LR" + v + r;
							LRG.Add (nlr);
							LineRenderer lr = nlr.AddComponent<LineRenderer> ();
							lr.startWidth = (0.05f);
							lr.endWidth = (0.05f);
							lr.material.color = Color.black;
							lr.material.color = new Color (1f, 1f, 1f, .5f);

							lr.SetPosition (0, lastRowLoc.transform.position);
							lr.SetPosition (1, GameObject.Find ("Loc" + uppRow + upperLocValue).transform.position);
							upperLocValue--;
						} else if (rowDif > 0 & upperLocValue >= 0 & currentRowValue >= 0 & GameObject.Find ("Loc" + v + currentRowValueDif) != null)
						{
							GameObject nlr = new GameObject ();
							nlr.transform.parent = LineRenderGroup.transform;
							//nlr.transform.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, .5f);
							nlr.name = "LR";
							LRG.Add (nlr);
							LineRenderer lr = nlr.AddComponent<LineRenderer> ();
							lr.startWidth = (0.05f);
							lr.endWidth = (0.05f);
							lr.material.color = Color.black;
							lr.material.color = new Color (1f, 1f, 1f, .5f);

							lr.SetPosition (0, GameObject.Find ("Loc" + v + currentRowValue).transform.position);
							lr.SetPosition (1, upperLastRowLoc.transform.position);
							currentRowValue--;
						}
					}

				}
			}
		} else if (v == Vertical)
		{
			for (int r = 0; r < listRows.Count; r++)
			{
				int Vminus = v - 1;
				currentLoc = GameObject.Find ("Loc" + Vminus + r);
				


				if (currentLoc != null)
				{


					GameObject nlr = new GameObject ();
					nlr.transform.parent = LineRenderGroup.transform;	
					nlr.name = "LR" + v + r;
					LRG.Add (nlr);
					LineRenderer lr = nlr.AddComponent<LineRenderer> ();
					lr.startWidth = (0.05f);
					lr.endWidth = (0.05f);
					lr.material.color = Color.black;
					lr.SetPosition (0, currentLoc.transform.position);
					lr.SetPosition (1, BossLocation.transform.position);
				}
			}
		}
		
		
	}
	}

	void Resize(GameObject go) {
		SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
		if(sr == null) return;

		go.transform.localScale = new Vector3(1,1,1);

		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;


		float worldScreenHeight=Camera.main.orthographicSize*2.2f;
		float worldScreenWidth=worldScreenHeight/Screen.height*Screen.width;

		Vector3 xWidth = go.transform.localScale;
		xWidth.x=worldScreenWidth / width;
		go.transform.localScale=xWidth;
		//transform.localScale.x = worldScreenWidth / width;
		Vector3 yHeight = go.transform.localScale;
		yHeight.y=worldScreenHeight / height;
		go.transform.localScale=yHeight;
		//transform.localScale.y = worldScreenHeight / height;
	}


	void DeleteMapF()
	{
		MapGenerated = false;

		locations.ForEach(Location => Destroy(Location));
		LRG.ForEach(nlr => Destroy(nlr));
		Destroy (GameObject.Find ("BossLocation"));
		d1 = 0;
		sectorCount = 0;
		listRN.Clear();
		locations.Clear();
		listRows.Clear ();
	}
}	
	
	
