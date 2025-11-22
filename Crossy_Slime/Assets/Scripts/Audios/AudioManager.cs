using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource audioController;
    [SerializeField] AudioSource audioControllerWalkingSlime;
    [SerializeField] AudioSource audioControllerDieVacio;
    [SerializeField] AudioClip dragon;
    [SerializeField] AudioClip slimeDieDragon;
    [SerializeField] AudioClip slimeDieGrifo;
    [SerializeField] AudioClip slimeDieVacio;
    [SerializeField] AudioClip grass;
    [SerializeField] AudioClip ice;
    [SerializeField] AudioClip teleport;
    [SerializeField] AudioClip longJump;
    [SerializeField] AudioClip breakable;
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
    internal void Dragon()
    {
        audioController.clip = dragon;
        audioController.priority = 128;
        audioController.volume = 1;
        audioController.Play();
    }
    internal void DieForVacio()
    {
        audioControllerDieVacio.Play();
    }
    internal void DieForDragon()
    {
        audioController.clip = slimeDieDragon;
        audioController.priority = 128;
        audioController.volume = 1;
        audioController.Play();
    }
    internal void DieForGrifo()
    {
        audioController.clip = slimeDieGrifo;
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
    internal void SoundIce()
    {
        audioController.clip = ice;
        audioController.priority = 50;
        audioController.volume = 1f;
        audioController.Play();
    }
    internal void SoundTeleport()
    {
        audioController.clip = teleport;
        audioController.priority = 50;
        audioController.volume = 1f;
        audioController.Play();
    }

    internal void SoundLongJump()
    { 
        audioController.clip = longJump;
        audioController.priority = 50;
        audioController.volume = 1f;
        audioController.Play();
    }

    internal void SoundBreakable()
    {
        audioController.clip = breakable;
        audioController.priority = 50;
        audioController.volume = 1f;
        audioController.Play();
    }
}
