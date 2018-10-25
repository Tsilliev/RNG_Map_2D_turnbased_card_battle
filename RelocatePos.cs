using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocatePos : MonoBehaviour {
	public bool relocating = false;
	GameObject Map;
	public GameObject TheMap;
	// Use this for initialization
	void Start () {
		TheMap = GameObject.Find ("TheMap");
	
	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnCollisionStay2D(Collision2D other)
	{
		


		Map = GameObject.Find ("GameManager");
		Map.GetComponent<MapGen> ().relocbool = false;

			float RNGposY = Random.Range (-0.4f, 0.4f);
			float RNGposX = Random.Range (-0.4f, 0.4f);
			transform.position = new Vector3 (transform.position.x + RNGposX, transform.position.y + RNGposY, transform.position.z);
		transform.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, .5f);

		
		}

	void OnCollisionExit2D(Collision2D other)
	{
		if(Map !=null)
		{
			Map.GetComponent<MapGen> ().relocbool = true;
		}
			


	}


	/*
	void OnTriggerStay2D(Collider2D other)
	{
	if (relocating == true)
	{

		float RNGposY = Random.Range (-0.4f, 0.4f);
		float RNGposX = Random.Range (-0.4f, 0.4f);
		transform.position = new Vector3 (transform.position.x + RNGposX, transform.position.y + RNGposY, transform.position.z);
	}
	}
*/
	//void OnTriggerExit2D(Collider2D other)
	//{
	//	relocating = false;
	//}
}
