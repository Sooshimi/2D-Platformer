using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField] private float speed; // SerializeField allows manual input of speed value in Unity
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    
    private void Awake()
    {
        // Grab references for rigid and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); // left & right movement

        // Flip player sprite when moving left/right
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        if(Input.GetKey(KeyCode.Space) && grounded)
            Jump();
        
        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") // check for an object that contains the "Ground" tag
            grounded = true;
    }
}
