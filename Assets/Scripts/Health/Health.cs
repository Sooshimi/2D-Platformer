using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } // can get var from other scripts, but can only set within this script
    private Animator anim;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend; // grabs ref to SpriteRenderer component

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Player hurt
            anim.SetTrigger("hurt");

            // iframes
            StartCoroutine(Invulnerability());
        }
        else
        {
            // Player dead
            if (!dead)
            {
                anim.SetTrigger("die");

                // deactovate all attached component class
                foreach (Behaviour component in components)
                    component.enabled = false;

                // the above foreach loop does the same as the below code

                // Player 
                //if (GetComponent<player_movement>() != null)
                //    GetComponent<player_movement>().enabled = false; // cannot move when dead

                // Enemy
                //if (GetComponentInParent<EnemyPatrol>() != null)
                //    GetComponentInParent<EnemyPatrol>().enabled = false;

                //if (GetComponent<MeleeEnemy>() != null)
                //    GetComponent<MeleeEnemy>().enabled = false;

                dead = true;
            }

        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true); // 8 and 9 are the player and enemy layers, respectively
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
