using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room camera
    [SerializeField] private float speed;
    private float currentPosX; // tells camera which position to go
    private Vector3 velocity = Vector3.zero;

    // Follow player camera
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance; // how far camera looks forward
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        // Room camera
        //   transform.position is the current position of the camera
        //   SmoothDamp(current pos, new pos, rate of change of camera pos, speed)
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        // Follow player camera
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed); // Lerp() gradually change value of lookAhead from initial value to final value
    }

    // change destination of camera
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x; // tells camera to move to current position x
    }
}