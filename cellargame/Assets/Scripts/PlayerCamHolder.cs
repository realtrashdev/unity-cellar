using UnityEngine;

public class PlayerCamHolder : MonoBehaviour
{
    Transform playerTransform;
    
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = playerTransform.position;
    }
}
