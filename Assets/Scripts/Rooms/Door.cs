using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true); // activates the entered room
                previousRoom.GetComponent<Room>().ActivateRoom(false); // deactivates the previous room
            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
                previousRoom.GetComponent<Room>().ActivateRoom(true); // activates the entered room
                nextRoom.GetComponent<Room>().ActivateRoom(false); // deactivates the previous room
            }
                
        }
    }
}
