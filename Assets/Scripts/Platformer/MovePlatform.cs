using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour
{
	private Vector3 NewLocation;
	private float DeltaHeight;
	private float RunningTime;
	public float Modifier = 20f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		NewLocation = transform.position;
		DeltaHeight = Mathf.Sin (RunningTime + Time.deltaTime) - Mathf.Sin (RunningTime);
		NewLocation.y = DeltaHeight * Modifier + NewLocation.y;
		RunningTime += Time.deltaTime;
		GetComponent<Rigidbody> ().MovePosition (NewLocation);
		//transform.position = NewLocation;
	}

	/*FVector NewLocation = GetActorLocation();
	float DeltaHeight = (FMath::Sin(RunningTime + DeltaTime) - FMath::Sin(RunningTime));
	NewLocation.Z += DeltaHeight * 20.0f;       //Scale our height by a factor of 20
	RunningTime += DeltaTime;
	SetActorLocation(NewLocation);*/
}
