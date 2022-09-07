using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint; // position of where arrows shoot from
    [SerializeField] private GameObject[] arrows; // array of arrow
    private float cooldownTimer;
    private void Attack()
    {
        cooldownTimer = 0;

        arrows[FindArrow()].transform.position = firePoint.position; // sets position of arrow to firePoint position
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActiveProjectile();
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy) // checks if arrow in current array index is inactive. If it is, use it
                return i;
        }
        return 0; // if doesn't find any inactive arrow, use first arrow
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}