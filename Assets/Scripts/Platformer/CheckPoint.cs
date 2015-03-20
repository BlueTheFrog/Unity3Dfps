using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{

    public GameObject player;
	public GameObject deadZone;

	// Use this for initialization
	void Start () 
	{
	        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other)
	{
	    deadZone.GetComponent<Dead>().playerSpawn = player.transform.position;
		gameObject.SetActive(false);
	}
}
