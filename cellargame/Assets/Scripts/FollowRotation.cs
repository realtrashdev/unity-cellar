using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    [SerializeField] private Transform rotation;
    [SerializeField] private float smoothing;

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation.rotation, smoothing * Time.deltaTime);
    }
}
