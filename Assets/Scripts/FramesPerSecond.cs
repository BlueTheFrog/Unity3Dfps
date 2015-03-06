using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FramesPerSecond : MonoBehaviour
{

	private Text fpsText;
	private float fps;

	// Use this for initialization
	void Start ()
	{
		fpsText = GetComponent<Text> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		fps = 1 / Time.deltaTime;
		fpsText.text = "FPS: " + fps.ToString ();
	}
}
