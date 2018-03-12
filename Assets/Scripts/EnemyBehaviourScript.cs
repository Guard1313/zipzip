using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour {

	public bool moveLeft;
	public LayerMask groundLayer;
	public Transform wallCheck;
	public float wallCheckRadius;
	public bool wallCollision;
	public bool notAtEdge;
	public Transform edgeCheck;
	// Use this for initialization
	void Start () {
		
		moveLeft = true;
	
	}

	void FixedUpdate () {
		wallCollision = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, groundLayer);
		notAtEdge = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, groundLayer);
	}
	
	// Update is called once per frame
	void Update () {
		var rB = GetComponent<Rigidbody2D> ();

		if (wallCollision || !notAtEdge) {
			moveLeft = !moveLeft;
		}

		if (moveLeft) {
			transform.localScale = new Vector2 (1, 1);
			rB.velocity = new Vector2 (-3, rB.velocity.y);
		} 

		else {
			transform.localScale = new Vector2 (-1, 1);
			rB.velocity = new Vector2 (3, rB.velocity.y);

		}
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "HeadStomper") {
			Destroy (gameObject);
		}
	}
}
