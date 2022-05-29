using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    ParticleSystem par;

    private void Start()
    {
        par = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<DeadPlayerController>(out var player))
        {
            par.Play();
        }
    }
}
