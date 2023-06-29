using UnityEngine;

public class GenerateTime : MonoBehaviour
{
    private float randTime;

    public float GenerateRandomTime()
    {
        randTime = Random.Range(8f, 20f);   
        return Mathf.Round(randTime);
    }
}
