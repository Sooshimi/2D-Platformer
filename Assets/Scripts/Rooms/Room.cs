using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject[] enemies; // array of all enemies in the room
    private Vector3[] initialPosition; // array of initial positions of all enemies

    private void Awake()
    {
        initialPosition = new Vector3[enemies.Length];

        
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position; // saves initial position of all enemies in the room
        }
    }

    public void ActivateRoom(bool _status)
    {
        // activate/deactivate enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i]; // sets the enemies position back to their saved initial positions
        }
    }
}
