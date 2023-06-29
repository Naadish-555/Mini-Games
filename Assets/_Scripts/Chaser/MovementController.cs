using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    // Update is called once per frame

    private void Start()
    {
        transform.position = Vector3.zero;
    }
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * moveSpeed ;
        Vector3 moveAmount = velocity * Time.deltaTime;
        transform.Translate(moveAmount);
    }
}
