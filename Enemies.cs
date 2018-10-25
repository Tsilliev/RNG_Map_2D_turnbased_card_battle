using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

	public GameObject Hero;
	public GameObject Monster;
	public GameObject CardEffectInt;
	public GameObject GameManager;
	public int WeaklingHP;
	public int MonsterHP;
	public int WeaklingSTR;
	public int WeaklingATK;
	public int Weaklings;
	public int Diff;
	public int DiffX3;
	public List<GameObject> WeaklingsObjList = new List<GameObject>();
//	public List<string> SkelBehav = new List<string>();*/
	// Use this for initialization
	void Start () {
		//GameManager = GameObject.Find ("GameManager");
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
