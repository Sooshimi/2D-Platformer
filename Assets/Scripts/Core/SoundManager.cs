using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // create public class of the same instance as this one
    // make static so it's stored in memory as the only copy, so no other SoundManager instance
    // access instance from other scripts, but only modified in this script
    public static SoundManager instance { get; private set; } 

    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();

        // keep this object even when going to a new scene
        if (instance == null)
        {
            instance = this; // prevents multiple bg music playing if another scene is loaded with another bg music
            DontDestroyOnLoad(gameObject);
        }
        // destroy duplicate gameObjects
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
