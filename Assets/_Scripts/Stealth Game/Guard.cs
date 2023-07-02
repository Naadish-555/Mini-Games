using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Guard : MonoBehaviour
{
    public static event System.Action OnGuardHasSpottedPlayer;

    [SerializeField] private Transform player;
    [SerializeField] private LayerMask ObstacleLayer;
    [SerializeField] private Transform pathHolder;
    [SerializeField] private Transform guardRange;
    [SerializeField] private float turnSpeed = 90f; 
    [SerializeField] private float patrolSpeed = 5f;
    [SerializeField] private float waitTime = .3f;


    [SerializeField] private Light spotLight;
    [SerializeField] private float viewDistance;
    [SerializeField] private float timeToSpotPlayer = 1.0f;
    private float viewAngle ;
    private float spotTime;
    private Color originalSpotLightColor;


    private void Start()
    {
        viewAngle = spotLight.spotAngle;
        originalSpotLightColor = spotLight.color;
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y,waypoints[i].z);
         
      
           
        }
        StartCoroutine(GuardMovement(waypoints));
    }

    private void Update()
    {
        
        if (IsEnemySpotted())
        {
            spotTime += Time.deltaTime;  
        }
        else
        {
            spotTime -= Time.deltaTime;
        }
        spotTime = Mathf.Clamp (spotTime , 0.0f, timeToSpotPlayer);
        spotLight.color = Color.Lerp(originalSpotLightColor, Color.red, spotTime / timeToSpotPlayer);
        if(spotTime >=  timeToSpotPlayer)
        {
            if(OnGuardHasSpottedPlayer != null)
            {
                OnGuardHasSpottedPlayer();
            }
        }
    }

    private bool IsEnemySpotted()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Debug.Log("within distance");
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenPlayerAndGuard = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenPlayerAndGuard < viewAngle / 2f)
            {
                Debug.Log("within viewangle");
                RaycastHit hitInfo;
                if(!Physics.Raycast(transform.position, player.position ,out hitInfo,viewDistance,ObstacleLayer))
                {
                    Debug.Log("Enemy Spotted"); 
                    Debug.DrawLine(transform.position, player.position,color:Color.green);
                    
                    return true;
                }
            }

        }
        return false;
    }

    IEnumerator GuardMovement(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetwaypointindex = 1;
        Vector3 targetwaypoint = waypoints[targetwaypointindex];
        transform.LookAt(targetwaypoint);
        while (true)
        {
            //EnemyDetection();
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
/*
    private void EnemyDetection()
    {
        
        float distance = (player.position - transform.position).magnitude;
        int rayCount = 10;
        viewAngle = spotLight.spotAngle;
        float rayDistance = viewDistance;

        float angleStep = viewAngle / (rayCount-1);
        float startAngle = transform.eulerAngles.z - viewAngle / 0.5f;
        for(int i = 0;i < rayCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 direction = Quaternion.Euler(0,angle,0) * Vector3.forward;
            RaycastHit hit;
            Debug.DrawLine(transform.position, direction * rayDistance, color: Color.green);
            if(Physics.Raycast(transform.position, direction, out hit, rayDistance, EnemyLayer))
            {
                Debug.Log("Enemy Spotted!");
            }
        }

        
       
    }*/


    
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
