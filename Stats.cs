using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stats : MonoBehaviour {
	

	public int DiffX1;
	public int DiffX3;
	public int DiffX5;
	public int DiffX10;

	public GameObject Hero;
	public float HeroHP;
	public float HeroMP;
	public int HeroAP;
	public float HeroArmor;
	public int HeroStrength;
	public int HeroAgility;
	public int HeroEndurance;
	public int HeroIntelect;

	public float WeaklingHP;
	public float MonsterHP;
	public float BossHP;
	public float WeaklingMP;
	public float MonsterMP;
	public float BossMP;
	public int WeaklingAP;
	public int MonsterAP;
	public int BossAP;

	// Use this for initialization
	void Start () {
		HeroHP = 100;
		HeroMP = 0;
		HeroAP = 3;
		HeroArmor = 0;
		HeroStrength = 0;
		HeroAgility = 0;
		HeroEndurance = 0;
		HeroIntelect = 0;

	DiffX1 = 0;
	DiffX3 = DiffX1 * 3;
	DiffX5 = DiffX1 * 5;
	DiffX10 = DiffX1 * 10;

	//WeaklingHP = Random.Range (5 + DiffX3, 30 + DiffX3);
	//MonsterHP = Random.Range (20 + DiffX5, 130 + DiffX5);
	}
	
	// Update is called once per frame
	void Update () {
		DiffX3 = DiffX1 * 3;
		DiffX5 = DiffX1 * 5;
		DiffX10 = DiffX1 * 10;
	}


}
