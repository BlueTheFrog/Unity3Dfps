﻿using UnityEngine;
using System.Collections;

public class FollowSky : MonoBehaviour
{

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
	
	}
}
