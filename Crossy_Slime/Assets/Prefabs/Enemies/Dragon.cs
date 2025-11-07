using System.Collections;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject endPoint;
    float speedDragon = 20;
    bool isMoving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            this.transform.position += -transform.right * speedDragon * Time.deltaTime;
        }
    }
        
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == endPoint)
        {
            isMoving = true;
            StartCoroutine(DelayForSpawn());
            this.transform.position = spawnPoint.transform.position;
        }
    }
    IEnumerator DelayForSpawn()
    {
        yield return new WaitForSeconds(7);
        isMoving = false;
    }
}
