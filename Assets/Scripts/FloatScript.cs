using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatScript : MonoBehaviour
{
    public Vector3 movementInDirection;
    int mu = 1;

    public float speed;

    public bool smooth = false;

    Vector2 center;

    private void Start()
    {
        center = transform.position;
    }

    private void Update()
    {
        if (smooth)
            transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector3(center.x, center.y) + (movementInDirection * mu), speed * Time.deltaTime);
        else
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector3(center.x, center.y) + (movementInDirection * mu), speed * Time.deltaTime);

        if (transform.position == new Vector3(center.x, center.y) + movementInDirection * mu)
        {
            mu *= -1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + movementInDirection, .2f);
        Gizmos.DrawWireSphere(transform.position + -movementInDirection, .2f);
    }
}
