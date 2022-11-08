using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float distToGround;
    [SerializeField] private float gravity;
    private RaycastHit hit;
    private Vector3 velocity;
    private bool hitGround = false;

    void Start()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out hit)) {
            controller.enabled = false;
            transform.position = hit.point + hit.normal*2;
            controller.enabled = true;
        }  
    }

    void FixedUpdate()
    {
        playerMovement();
        playergravity();
    }

    void playerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        if (hitGround && Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Input.GetKey(KeyCode.LeftShift)){
            controller.Move(move * speed/3 * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        hitGround = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    void playergravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (hitGround)
        {
            velocity.y = 0;
        }
    }
}
