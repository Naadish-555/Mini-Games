using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform sphereTransform;

    private void Start()
    {
        sphereTransform.parent = transform;
    }

    private void Update()
    {
        //transform.eulerAngles += Vector3.up * 180 * Time.deltaTime;

        //transform.Rotate(Vector3.up * 180 * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.up * 180 *Time.deltaTime, Space.Self);
        transform.Translate(Vector3.fwd  * Time.deltaTime * 7, Space.World);
    }
}
