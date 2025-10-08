using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] DetectorOfGround detectorOfGroundForward;
    [SerializeField] DetectorOfGround detectorOfGroundBackward;
    [SerializeField] DetectorOfGround detectorOfGroundRight;
    [SerializeField] DetectorOfGround detectorOfGroundLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && detectorOfGroundForward.PossibleMove())
        {
            transform.position += this.transform.forward * 1 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) && detectorOfGroundBackward.PossibleMove())
        {
            transform.position += -this.transform.forward * 1 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) && detectorOfGroundLeft.PossibleMove())
        {
            transform.position += -this.transform.right * 1 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A) && detectorOfGroundRight.PossibleMove())
        {
            transform.position += this.transform.right * 1 * Time.deltaTime;
        }
    }
    

}
