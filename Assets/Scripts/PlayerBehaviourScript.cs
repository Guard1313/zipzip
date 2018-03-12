using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviourScript : MonoBehaviour {
	private int lives=3;
	public SpriteRenderer sprite;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float groundCheckRadius;
	private bool grounded;
	private bool doubleJumped;
	public CanvasGroup canvasYouWin;


	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);

	}
	// Update is called once per frame
	void Update () {
		var rigidBody = GetComponent<Rigidbody2D> ();
		var transform = GetComponent<Transform> ();

		if (Input.GetKey ("right")) {
			sprite.flipX = false; {
				if (Input.GetKey ("left shift")) {
					rigidBody.velocity = new Vector2 (10, rigidBody.velocity.y);
				} else {
					rigidBody.velocity = new Vector2 (5, rigidBody.velocity.y);
				}
			}
		}

		if (Input.GetKey ("left")) {
			sprite.flipX = true; {
				if (Input.GetKey ("left shift")) {
					rigidBody.velocity = new Vector2 (-10, rigidBody.velocity.y);
				} else {
					rigidBody.velocity = new Vector2 (-5, rigidBody.velocity.y);
				}
			}

		}

		if (grounded)
			doubleJumped = false;
		if (Input.GetKeyDown ("up") && grounded) {
					rigidBody.velocity = new Vector2 (rigidBody.velocity.x, 12);
				
			}

		if (Input.GetKeyDown ("up") && !grounded && !doubleJumped) {
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x, 12);
			doubleJumped = true;

		}

		if (transform.position.y < -6) {
			lives -= 1;
			if (transform.position.x < 37) {
				transform.position = new Vector2 (-8, -3);

			
			} else {
				transform.position = new Vector2 (36, 1);
			}
		}
		if (lives == 0) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}

	}
	public void OnTriggerEnter2D (Collider2D other) { 
		if (other.name == "EnemyDamage") {
			lives -= 1;
			transform.position = new Vector2 (-8, -3);
		} else if (other.name == "Exit") {
			canvasYouWin.alpha = 1;
		}

	}
}
