using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    private float screenWidthInWorldUnits;
    private float screenHeightInWorldUnits;
    [SerializeField] private float startTime = 2f;
    [SerializeField] private float repeatTime = 1.5f;

    float nextSpawnTime;
    public Vector2 secondsBetweenSpawnsMinMax;  


    private void Start()
    {
        string spawnObstacle = "SpawnObstacleAtRandomLocation";
        screenWidthInWorldUnits = Camera.main.orthographicSize * Camera.main.aspect;
        screenHeightInWorldUnits = Camera.main.orthographicSize;
        //InvokeRepeating(spawnObstacle, startTime, repeatTime);
        
    }

    private void Update()
    {
        if(Time.time > nextSpawnTime)
        {
            float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());
            nextSpawnTime = Time.time + secondsBetweenSpawns;
            float randompos = Random.Range(-screenWidthInWorldUnits, screenWidthInWorldUnits);
            float randomwidth = Random.Range(1, 4f);
            float randomheight = Random.Range(1, 4f);
            int randomAngle = Random.Range(0, 361);
            bool spawned = false;
            transform.position = new Vector3(randompos, transform.localPosition.y, transform.localPosition.z);
            transform.rotation = Quaternion.Euler(randomAngle, randomAngle, 0);
            obstaclePrefab.transform.localScale = new Vector3(randomwidth, randomheight, transform.localScale.z);
            GameObject obstacle = (GameObject)Instantiate(obstaclePrefab, transform.position, transform.rotation); 
       }
   
    }
   /* private void GenerateRandomPoints()
    {
        float randompos = Random.Range(-screenWidthInWorldUnits, screenWidthInWorldUnits);
        float randomwidth = Random.Range(1, 2.5f);
        float randomheight = Random.Range(1, 2.5f);
        int randomAngle = Random.Range(0, 361);
        transform.position = new Vector3(randompos, transform.localPosition.y,transform.localPosition.z);
        transform.rotation = Quaternion.Euler(randomAngle, randomAngle, 0);
        obstaclePrefab.transform.localScale = new Vector3(randomwidth , randomheight, transform.localScale.z);
        GameObject obstacle = (GameObject)Instantiate(obstaclePrefab, transform.position, transform.rotation);
      
    }*/
}


