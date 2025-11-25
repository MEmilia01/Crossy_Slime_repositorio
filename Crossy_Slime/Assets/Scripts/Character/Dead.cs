using UnityEngine;

public class Dead : MonoBehaviour
{
    public GameObject player;
    public GameObject dragon;
    public Cameramove camara;
    public GameObject casillaMuerte;
    public GameObject menuPuntuacion;
    public GameObject menuMuerte;
    public static Dead dead;

    private void Awake()
    {
        dead = GetComponent<Dead>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == dragon)
        {
            IsDead();
        }
    }

    public void IsDead()
    {
        camara.Stop();
        player.GetComponent<Movement>().enabled = false;
        menuPuntuacion.SetActive(false);
        menuMuerte.SetActive(true);
    }
}
