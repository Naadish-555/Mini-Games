using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] Transform targetPositon;
    [SerializeField] float moveSpeed = 7f;
    // Update is called once per frame
    void Update()
    {
        Vector3 displacementFromObject = targetPositon.position - transform.position;
        Vector3 directionToTarget = displacementFromObject.normalized;
        Vector3 velocity = directionToTarget * moveSpeed;
         
        float distanceToTarget = displacementFromObject.magnitude;
        if(displacementFromObject.magnitude > 1.5f)
        {
            transform.Translate(velocity*Time.deltaTime);
        }
        

    }
}
