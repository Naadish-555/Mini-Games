using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Guard : MonoBehaviour
{
    [SerializeField] private Transform pathHolder;
    [SerializeField] private Transform guardRange;
    [SerializeField] private float turnSpeed = 90f; 
    [SerializeField] private float patrolSpeed = 5f;
    [SerializeField] private float waitTime = .3f;


    [SerializeField] private Light spotLight;
    [SerializeField] private float viewDistance;
    private float viewAngle;


    private void Start()
    {
        viewAngle = spotLight.spotAngle;
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y,waypoints[i].z);
         
      
           
        }
        StartCoroutine(GuardMovement(waypoints));
    }


    IEnumerator GuardMovement(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetwaypointindex = 1;
        Vector3 targetwaypoint = waypoints[targetwaypointindex];
        transform.LookAt(targetwaypoint);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetwaypoint, patrolSpeed * Time.deltaTime);  
            if(transform.position == targetwaypoint )
            {
                targetwaypointindex = (targetwaypointindex + 1) % waypoints.Length;
                targetwaypoint = waypoints[targetwaypointindex];
                yield return new WaitForSeconds(waitTime);
                StartCoroutine(GuardRotateTowardsNextPoint(targetwaypoint));


            }
            yield return null;
        }
    }

    private void EnemyDetection()
    {
        if()
    }

    IEnumerator GuardRotateTowardsNextPoint(Vector3 lookTarget)
    {
        Vector3 dirToLook = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLook.z , dirToLook.x) * Mathf.Rad2Deg;

        while(Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y,targetAngle))> 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y , targetAngle , turnSpeed* Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }

    }
    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform points in pathHolder)
        {
            Gizmos.DrawSphere(points.position, 0.3f);
            Gizmos.DrawLine(previousPosition, points.position);
            previousPosition = points.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward * viewDistance);
    }
}
