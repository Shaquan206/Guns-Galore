using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;

    public float moveSpeed;

    Vector2 movement;
    Vector2 mousePos;

    Vector3 offset;

    private void Start()
    {
        offset = cam.transform.position - transform.position;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void LateUpdate()
    {
        cam.transform.position = transform.position + offset;
    }
}
