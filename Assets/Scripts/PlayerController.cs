using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;

	public float speed;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		float horizontalMovement = Input.GetAxis ("Horizontal");
		float verticalMovement = Input.GetAxis ("Vertical");

		Vector3 newPosition = transform.position + new Vector3 (horizontalMovement - verticalMovement, 0.0f, verticalMovement + horizontalMovement) * speed;
		rigidBody.MovePosition (newPosition);

	}
}
