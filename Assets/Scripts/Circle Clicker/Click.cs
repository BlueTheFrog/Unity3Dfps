using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class Click : MonoBehaviour
{

	public GameObject circlesClickedText;
	public GameObject CPStext;

	//private float[] CPSlist;
	private int circlesClicked = 1;
	private float CPS = 0f;
	private float timer;

	// Use this for initialization
	void Start ()
	{
	
	}

	void FixedUpdate ()
	{
		timer += Time.fixedDeltaTime;
		if (timer > 1)
		{
			CPStext.GetComponent<Text> ().text = "CPS: 0";
		}
	}

	void OnMouseDown ()
	{
		CPS = 1f / timer;
		timer = 0f;
		circlesClickedText.GetComponent<Text> ().text = "Circles Clicked: " + circlesClicked++.ToString();
		CPStext.GetComponent<Text> ().text = "Circles Per Second: " + CPS.ToString();
	}
}
