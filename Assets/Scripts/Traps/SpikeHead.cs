using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("Spikehead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer; // only detect the player in the Player layer
    private Vector3 destination;
    private Vector3[] directions = new Vector3[4]; // spikehead checks 4 directions
    private float checkTimer;
    private bool attacking;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    // OnEnable function gets called whenever an object gets activated
    private void OnEnable()
    {
        Stop(); // objects starts in an idle position
    }

    private void Update()
    {
        // move spikehead to destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        // checks in all 4 directions if spikehead sees player
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red); // draw ray lines in Play mode, for debugging purposes
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer); // detects player

            if (hit.collider != null && !attacking) // checks if spikehead detects a player and isn't attacking, then attack
            {
                attacking = true;
                destination = directions[i]; // destination is in the direction of the detected player
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range; // right direction, to a certain range
        directions[1] = -transform.right * range; // left direction, to a certain range
        directions[2] = transform.up * range; // up direction, to a certain range
        directions[3] = -transform.up * range; // down direction, to a certain range
    }

    private void Stop()
    {
        destination = transform.position; // set destination as current position so it doesn't move
        attacking = false;
    }

    // handle collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop(); // stop spikehead once it hits something
    }
}