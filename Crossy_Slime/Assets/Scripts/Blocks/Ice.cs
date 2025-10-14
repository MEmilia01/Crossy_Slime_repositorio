using Unity.VisualScripting;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public bool isPlayerOnIce = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerOnIce = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Estoy encima del hielo");
            isPlayerOnIce = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnIce = false;
        }
    }
}
