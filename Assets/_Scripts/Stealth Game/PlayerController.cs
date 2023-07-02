using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float smoothTime = .1f;

    private Rigidbody rigidbody;
    private float angle;
    private Vector3 movement;
    private bool disabled = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Guard.OnGuardHasSpottedPlayer += Disabled;
    }
    private void Update()
    {
        Vector3 input = Vector3.zero;
        if (!disabled)
        {
            input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        Vector3 inputDxn = input.normalized;
        float inputMagnitude = Mathf.Lerp(0, inputDxn.magnitude, smoothTime);
        float targetAngle = Mathf.Atan2(inputDxn.x, inputDxn.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(transform.eulerAngles.y , targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);
        movement = transform.forward * movementSpeed * inputMagnitude ;
     /*   transform.eulerAngles = Vector3.up * angle;
        transform.Translate(inputDxn * movementSpeed * inputMagnitude * Time.deltaTime,Space.World);*/

    }

    private void Disabled()
    {
        disabled = true;
    }

    private void FixedUpdate()
    {
        rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigidbody.MovePosition(rigidbody.position + movement * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Guard.OnGuardHasSpottedPlayer -= Disabled;
    }
}
