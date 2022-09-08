using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator anim;
    private float lifetime;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(hit) return; // if hits something, return nothing
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        // Check duration of a fireball being active
        lifetime += Time.deltaTime;
        if (lifetime > 2) gameObject.SetActive(false);
    }

    // Check to see if projectile hits another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    // Use method everytime player shoots, goes left and right directions
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction; //creates local variable
        gameObject.SetActive(true); //creates the object
        hit = false;
        boxCollider.enabled = true; // resets state of fireball

        // flips fireball if going other direction
        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactive()
    {
        gameObject.SetActive(false); // destroys object
    }
}
