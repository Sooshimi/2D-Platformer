using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound; //sound upon changing option (selection arrow moving)
    [SerializeField] private AudioClip interactSound; //sound upon selection button click
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // change position of selection arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        // interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0) //checks if selection arrow has changed position
            SoundManager.instance.PlaySound(changeSound);

        //prevent selection to be outside of available options
        if (currentPosition < 0)
            currentPosition = options.Length - 1; //if selection arrow moves up from top option, it goes to last option
        else if (currentPosition > options.Length - 1)
            currentPosition = 0; //if selection arrow moves down from bottom option, it goes to top option

        //assign Y position of the current option to the arrow (up and down)
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        // access button component on each option and call its function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
