using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource audioController;
    [SerializeField] AudioSource audioControllerWalkingSlime;
    [SerializeField] AudioClip slimeWalk;
    [SerializeField] AudioClip slimeDieDragon;
    [SerializeField] AudioClip slimeDieVacio;
    [SerializeField] AudioClip grass;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

    }
    internal void Walking()
    {
        audioControllerWalkingSlime.Play();
    }
    internal void DieForDragon()
    {
        audioController.clip = slimeDieDragon;
        audioController.priority = 128;
        audioController.volume = 1;
        audioController.Play();
    }
    internal void DieForVacio()
    {
        audioController.clip = slimeDieVacio;
        audioController.priority = 128;
        audioController.volume = 1;
        audioController.Play();
    }
    internal void SoundGrass()
    {
        audioController.clip = grass;
        audioController.priority = 50;
        audioController.volume = 1f;
        audioController.Play();
    }
}
