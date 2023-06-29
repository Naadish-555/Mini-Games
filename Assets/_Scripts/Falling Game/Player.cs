using System.Data;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    private Vector3 velocity;
    private Rigidbody rb;
    private float screenWidthInWorldUnits;
    private float playerhalfwidth;

    private void Start()
    {
        playerhalfwidth = transform.localScale.x/2f;
        rb = GetComponent<Rigidbody>();
        screenWidthInWorldUnits = Camera.main.orthographicSize * Camera.main.aspect + playerhalfwidth;

    }

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        Vector3 moveDir = input.normalized;
        velocity = moveDir * movementSpeed;

        if(transform.localPosition.x < -screenWidthInWorldUnits)
        {
            transform.position = new Vector3(screenWidthInWorldUnits - playerhalfwidth , transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.x > screenWidthInWorldUnits)
        {
            transform.position = new Vector3(-screenWidthInWorldUnits + playerhalfwidth, transform.localPosition.y, transform.localPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Obstacle")
        {
            Destroy(gameObject);
            Debug.Log("GameEnd");
        }
    }

    private void FixedUpdate()
    {
        rb.position += velocity * Time.deltaTime;
    }


}
