using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using System.Timers;

public class SpeedrunManager : MonoBehaviour
{
    float time;

    public TextMeshProUGUI text;

    static SpeedrunManager instance;

    public static bool speedrun = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
            Destroy(gameObject);

        //Play();
    }

    List<string> nospeedrun = new List<string>(2)
    {
        "MainMenu",
        "Ending"
    };

    public static void Play()
    {
        if (instance.adding != null)
            return;

        if (speedrun && !instance.nospeedrun.Contains(SceneManager.GetActiveScene().name))
            instance.adding = instance.StartCoroutine(instance.AddToTimer());
    }

    public static void Pause()
    {
        if (instance.adding != null)
        {
            instance.StopCoroutine(instance.adding);
            instance.adding = null;
        }
    }

    public static void Stop()
    {
        if (instance.adding != null)
        {
            instance.StopCoroutine(instance.adding);
            instance.adding = null;
        }
        instance.time = 0;
    }

    Coroutine adding;
    IEnumerator AddToTimer()
    {
        while(true)
        {
            time += Time.deltaTime;

            text.text = $"{Mathf.Round(time * 100) / 100}s";
            
            yield return new WaitForEndOfFrame();
        }
    }
}
