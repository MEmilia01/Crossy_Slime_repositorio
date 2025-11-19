using UnityEngine;

public class SlimeAudios : MonoBehaviour
{
    [SerializeField]AudioSource slimeWalk;
    [SerializeField]AudioSource slimeDieDragon;
    [SerializeField] AudioSource slimeDieVacio; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    internal void StartSoundWalking()
    {
        slimeWalk.Play();
    }
    internal void StartSoundDie()
    {
        slimeDieDragon.Play();
    }
    internal void StartSoundVacio()
    {
        slimeDieVacio.Play();
    }
}
