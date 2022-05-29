using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    Transform sprite;
    SpriteRenderer sr;

    Animator anim;

    Collider2D col;

    bool dead;

    public static UnityEvent playerDeathEvent;

    public static PlayerController instance;

    [HideInInspector] public Vector2 lastMovingInput = Vector2.right;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0);
        sr = sprite.GetComponent<SpriteRenderer>();

        col = GetComponent<Collider2D>();

        anim = sprite.GetComponent<Animator>();
    }

    public bool onGround;

    [HideInInspector]
    public int scl = 1;
    float spriteTurnSpeed = 9f;

    public float walkSpeed = 3;

    public float jumpHeight = 6;

    void Update()
    {
        sprite.localScale = new Vector2(Mathf.Lerp(sprite.localScale.x, scl, spriteTurnSpeed * Time.deltaTime), 1);

        if (dead)
            return;

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.velocity = new Vector2(movement.x * walkSpeed, rb.velocity.y);
        if (movement != Vector2.zero)
            lastMovingInput = movement;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        else if (rb.velocity.y > jumpHeight / 2 && !Input.GetButton("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .99f);

        if (movement.x > 0)
            scl = 1;
        else if (movement.x < 0)
            scl = -1;

        //anim.SetBool("OnGround", onGround);
        //anim.SetFloat("Y", Mathf.Abs(rb.velocity.y));
        anim.SetFloat("Blend", Mathf.Abs(movement.x));
    }

    public void Jump()
    {
        if (!onGround)
            return;

        ForceJump();
    }

    public void ForceJump()
    {
        onGround = false;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }

    void Die()
    {
        dead = true;

        gameObject.SetActive(false);

        DeadPlayerController.instance.Die(transform.position);
    }

    public void UnDie(Vector3 pos)
    {
        dead = false;

        transform.position = pos;
        gameObject.SetActive(true);
    }

    //public float goldenspot = .29f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Modifier>(out var mod) && mod.kill)
        {
            Die();
        }
    }

    Coroutine fls;

    void Flash()
    {
        if (fls != null)
            StopCoroutine(fls);
        fls = StartCoroutine(_flash());

        IEnumerator _flash()
        {
            Color _1 = Color.red;
            Color _2 = Color.white;

            while (sr.color != _1)
            {
                sr.color = Vector4.MoveTowards(sr.color, _1, .5f);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(.2f);

            while (sr.color != _2)
            {
                sr.color = Vector4.MoveTowards(sr.color, _2, .2f);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
