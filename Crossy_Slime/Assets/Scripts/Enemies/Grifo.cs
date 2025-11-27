using System.Collections;
using UnityEngine;

public class Grifo : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] GameObject grifo;
    [SerializeField] GameObject player;
    float speedDragon = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            this.transform.position += -transform.right * speedDragon * Time.deltaTime;
    }
        
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == endPoint)
        {
            grifo.SetActive(false);
        }
        if (collision.gameObject == player)
        {
            player.SetActive(false);
        }
    }
}
