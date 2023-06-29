using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    private float screenHeightInWorldUnits;

    private void Start()
    {
        screenHeightInWorldUnits = Camera.main.orthographicSize;
    }
    private void Update()
    {
        if(transform.position.y < -screenHeightInWorldUnits)
        {
            Destroy(gameObject);
        }
    }

}