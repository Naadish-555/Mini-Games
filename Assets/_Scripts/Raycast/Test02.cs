using UnityEngine;

public class Test02 : MonoBehaviour
{
    [SerializeField] private Transform cube;
    [SerializeField] Camera cam;
    [SerializeField] private LayerMask mask;

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100f,mask))
        {
            cube.position = hit.point;
            cube.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        
    }
}
