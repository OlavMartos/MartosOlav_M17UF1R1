using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform followTransform;
    
    void FixedUpdate()
    {
        // Hacemos que la camara siempre este siguiendo al followTransform
        if (followTransform != null)
        {
            transform.position = new Vector3(followTransform.position.x, followTransform.position.y, transform.position.z);
        }
    }
}
