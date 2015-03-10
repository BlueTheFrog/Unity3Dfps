using UnityEngine;
using System.Collections;

public class Dead : MonoBehaviour
{

	public GameObject player;

	private Vector3 playerSpawn;

	// Use this for initialization
	void Start ()
	{
		playerSpawn = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter ()
	{
		player.transform.position = playerSpawn;
	}

}
