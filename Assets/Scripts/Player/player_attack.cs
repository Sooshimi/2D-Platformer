using UnityEngine;

public class player_attack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint; // the position of where the projectiles are fired from
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    private Animator anim;
    private player_movement playerMovement;
    private float cooldownTimer = Mathf.Infinity; // so player can attack when game starts

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<player_movement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        
        fireballs[FindFireball()].transform.position = firePoint.position; // sets position of first fireball to position of firePoint
        fireballs[FindFireball()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x)); // sets direction of fireball to direction of player
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if(!fireballs[i].activeInHierarchy) // if fireball is not active, then use it
                return i;
        }

        return 0;
    }
}
