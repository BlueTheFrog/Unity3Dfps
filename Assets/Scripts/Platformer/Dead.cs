using UnityEngine;
using System.Collections;

public class Dead : MonoBehaviour
{

	public GameObject player;

	public Vector3 playerSpawn;

	// Use this for initialization
	void Start ()
	{
		playerSpawn = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.transform.tag == "Player")
		{
		    player.transform.position = playerSpawn;
		}
	}

}
