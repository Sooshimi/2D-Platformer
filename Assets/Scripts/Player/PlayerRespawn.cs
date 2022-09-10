using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint; // store latest checkpoint here
    private Health playerHealth; //reset player health upon respawn

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.position; //move player to checkpoint position
        playerHealth.Respawn(); //restore player health and reset animation

        //move camera to checkpoint room
        //for this to work, the checkpoint objects must be placed as a child of the room object
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    //Activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //save checkpoint we activated as the current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear"); //trigger checkpoint animation
        }
    }
}
