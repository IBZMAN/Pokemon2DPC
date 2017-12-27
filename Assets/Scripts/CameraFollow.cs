using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private bool restrictCamera = false;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float yMin;

    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMax;

    [SerializeField]
    private Transform target;

    [SerializeField]
    public Vector3 offset;

    void LateUpdate()
    {   
        if (restrictCamera)
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x + offset.x, xMin, xMax), Mathf.Clamp(target.position.y + offset.y, yMin, yMax), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, transform.position.z);
        }
    }
}
