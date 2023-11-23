using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hareket : MonoBehaviour {
    public float moveSpeed = 15f;
	
    public float speed;
    public float jumpForce;
    private float moveInput;
	
	private Rigidbody2D rb;
	private bool facingRight = true;
 
    private bool isGrounded;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;
 
 
    private int extraJumps;
	public int extraJumpsValue;
    
    void Start(){
		extraJumps = extraJumpsValue;
        rb = GetComponent <Rigidbody2D> ();
    }
 
    void FixedUpdate(){
        
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
		
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);
		
		if(facingRight == false && moveInput > 0){
			Flip();
		} else if(facingRight == true && moveInput < 0){
			Flip();
		}
    }
	
	void Update(){
		Jump();
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
		transform.position += movement * Time.deltaTime * moveSpeed;
	    }
	void Jump(){
		if (Input.GetButtonDown("Jump")){
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 25f), ForceMode2D.Impulse);
		}
		if(isGrounded == true){
			extraJumps = extraJumpsValue;
		}
		
		if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0){
			rb.velocity = Vector2.up * jumpForce;
			extraJumps--;
		} else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true){
			rb.velocity = Vector2.up * jumpForce;
		}
		if (isGrounded == false)
        {
            extraJumps = 0;
        }
	}
	
	void Flip(){
		
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}
}