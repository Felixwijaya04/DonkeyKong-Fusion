using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] runSprite;
    public Sprite climbSprite;
    private int spriteIndex;

    private Rigidbody2D rb;
    private Vector2 direction;
    private new Collider2D collider;
    private Collider2D[] results;
    public Joystick joystick;

    [Header("Player Stats")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpStrength;

    private bool grounded;
    private bool climbing;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[4];
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimateSprite), 1f / 12f, 1f / 12f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        CheckCollision();

        if(climbing)
        {
            direction.y = joystick.Direction.y * moveSpeed;

        }
        else if(joystick.Direction.y > 0f && grounded)
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
        climbing = false;

        Vector2 size = collider.bounds.size;
        size.y += 0.1f;
        size.x /= 2f;

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for(int i=0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if (hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < transform.position.y - 0.5f;

                Physics2D.IgnoreCollision(collider, results[i], !grounded);
            }
            else if (hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                climbing = true;
            }
        }
    }

    private void AnimateSprite()
    {
        if(climbing)
        {
            spriteRenderer.sprite = climbSprite;
        }
        else if(direction.x != 0f)
        {
            spriteIndex++;

            if(spriteIndex >= results.Length)
            {
                spriteIndex = 0;
            }

            spriteRenderer.sprite = runSprite[spriteIndex];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            enabled = false;
            FindObjectOfType<GameManager>().LevelFailed();
        }
        else if (collision.gameObject.CompareTag("Objective"))
        {
            enabled = false;
            FindObjectOfType<GameManager>().LevelComplete();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Dead"))
        {
            enabled = false;
            FindObjectOfType<GameManager>().LevelFailed();
        }
    }
}
