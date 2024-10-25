using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private new Collider2D collider;
    private Collider2D[] results;
    public Joystick joystick;

    [Header("Player Stats")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpStrength;

    private bool grounded;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[4];
    }

    private void Update()
    {
        CheckCollision();

        if(joystick.Direction.y > 0f && grounded)
        {
            direction = Vector2.up * jumpStrength;
        }
        else
        {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = joystick.Direction.x * moveSpeed;
        
        if(grounded)
        {
            direction.y = Mathf.Max(direction.y, -1f);
        }
        
        if(direction.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        } else if(direction.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
    }

    private void CheckCollision()
    {
        grounded = false;

        Vector2 size = collider.bounds.size;
        size.y += 0.1f;
        size.x /= 2f;

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for(int i=0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if(hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < transform.position.y - 0.5f;
            }
        }
    }
}
