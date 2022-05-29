using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerController p;

    private void Start()
    {
        p = PlayerController.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Modifier>(out var mod) && mod.ignoreGroundCheck)
            return;

        if (!p.onGround)
        {
            p.onGround = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Modifier>(out var mod) && mod.ignoreGroundCheck)
            return;

        if (!p.onGround)
        {
            p.onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Modifier>(out var mod) && mod.ignoreGroundCheck)
            return;

        if (p.onGround)
        {
            if(gameObject.activeInHierarchy)
                StartCoroutine(OnGroundFalse());
        }
    }

    IEnumerator OnGroundFalse()
    {
        yield return new WaitForSeconds(.3f);
        p.onGround = false;
    }
}
