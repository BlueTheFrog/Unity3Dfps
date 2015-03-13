using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour
{

	public float speed;

	private Vector3 newRotation;
	private Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey("a"))
		{
			anim.SetBool("yes", true);
			//newRotation = new Vector3 (0f, 0f, transform.rotation.eulerAngles.z + speed * Time.deltaTime);
			//transform.rotation = Quaternion.Euler (newRotation);
		}
		if (Input.GetKey("d"))
		{
			newRotation = new Vector3 (0f, 0f, transform.rotation.eulerAngles.z - speed * Time.deltaTime);
			transform.rotation = Quaternion.Euler (newRotation);
		}
	
	}
}
