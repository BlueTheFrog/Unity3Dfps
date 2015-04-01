using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dead : MonoBehaviour
{

	public GameObject player;
	public GameObject livesText;
	public Vector3 playerSpawn;

	private int lives = 3;

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
			lives--;
			if (lives <= 0)
			{
				Application.LoadLevel("FPS");
			}
			livesText.GetComponent<Text>().text = "Lives: " + lives.ToString();
		    player.transform.position = playerSpawn;
		}
	}

}
