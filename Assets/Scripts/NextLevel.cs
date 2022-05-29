using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextCOck;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerController>(out var kyrpä))
        {
            Transiteaiputea();
        }
    }

    public void Transiteaiputea()
    {

        TransitionManager.LoadScene(nextCOck);
    }
}
