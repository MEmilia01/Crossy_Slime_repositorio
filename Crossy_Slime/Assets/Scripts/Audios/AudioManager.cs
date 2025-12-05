using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource audioController;
    [SerializeField] AudioSource audioControllerWalkingSlime;
    [SerializeField] AudioSource audioControllerDieVacio;
    [SerializeField] AudioSource audioControllerIce;
    [SerializeField] AudioSource audioControllernuve;
    [SerializeField] AudioSource audioControllerDragon;
    [SerializeField] AudioSource audioControllerGrifo;
    [SerializeField] AudioClip slimeDieDragon;
    [SerializeField] AudioClip slimeDieGrifo;
    [SerializeField] AudioClip slimeDieVacio;
    [SerializeField] AudioClip grass;
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
        audioControllerDragon.Play();
    }internal void Grifo()
    {
        audioControllerGrifo.Play();
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
        audioControllerIce.Play();
    }
    internal void SoundNuve()
    {
        audioControllernuve.Play();
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
    internal void MuteAll()
    {
        AudioListener.volume = 0f;
    }
    internal void UnMute()
    {
        AudioListener.volume = 1f;
    }
}
