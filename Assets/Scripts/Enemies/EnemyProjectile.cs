using UnityEngine;

public class EnemyProjectile : EnemyDamage // will damage the player every time they touch
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime; // deactivates object after a certain time
    private float lifetime;

    public void ActiveProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime; // framerate independent
        transform.Translate(movementSpeed, 0, 0); // moves object on x axis based on rate of speed

        lifetime += Time.deltaTime; // increments lifetime of object
        if (lifetime > resetTime)
            gameObject.SetActive(false); // deactives object after certain amount of time
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 'base' lets you access the EnemyDamage parent class. Executes logic from it first.
        // Without this, because both these classes have OnTriggerOn2D, the TakeDamage function from parent class would not activate
        base.OnTriggerEnter2D(collision); 

        gameObject.SetActive(false); // deactivates arrow when it hits an object with collision
    }
}
