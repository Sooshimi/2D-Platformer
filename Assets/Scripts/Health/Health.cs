using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]private float startingHealth;
    public float currentHealth { get; private set; } // can get var from other scripts, but can only set within this script
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Player hurt
            anim.SetTrigger("hurt");
            // iframes
        }
        else
        {
            // Player dead
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<player_movement>().enabled = false; // cannot move when dead
                dead = true;
            }

        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
