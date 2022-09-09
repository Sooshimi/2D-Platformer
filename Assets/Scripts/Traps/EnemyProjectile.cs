using UnityEngine;

public class EnemyProjectile : EnemyDamage // will damage the player every time they touch
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime; // deactivates object after a certain time
    private float lifetime;
    private Animator anim;
    private bool hit;
    private BoxCollider2D coll;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }

    private void Update()
    {
        if (hit) return; // if projectile hits something, stop code execution
        float movementSpeed = speed * Time.deltaTime; // framerate independent
        transform.Translate(movementSpeed, 0, 0); // moves object on x axis based on rate of speed

        lifetime += Time.deltaTime; // increments lifetime of object
        if (lifetime > resetTime)
            gameObject.SetActive(false); // deactives object after certain amount of time
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;

        // 'base' lets you access the EnemyDamage parent class. Executes logic from it first.
        // Without this, because both these classes have OnTriggerOn2D, the TakeDamage function from parent class would not activate
        base.OnTriggerEnter2D(collision);
        coll.enabled = false; // disables the projectile collider

        if (anim != null)
            anim.SetTrigger("explode"); // arrow explodes when it hits something
        else
            gameObject.SetActive(false); // deactives arrow when it hits something
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }
}
