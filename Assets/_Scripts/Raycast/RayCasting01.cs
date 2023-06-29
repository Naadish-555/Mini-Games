using UnityEngine;

public class RayCasting01 : MonoBehaviour
{
    [SerializeField] private LayerMask mask;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo,100f,mask,QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(ray.origin, hitinfo.point , Color.red);
        }
        else
        {
            Debug.DrawLine(ray.origin , ray.origin + ray.direction * 100, Color.green);
        }
    }
}
