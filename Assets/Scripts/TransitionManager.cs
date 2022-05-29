using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    Image img;
    Canvas canvas;

    static TransitionManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        img = GetComponentInChildren<Image>();
        img.color = Color.black;

        canvas = GetComponent<Canvas>();

        StartCoroutine(crossout());
    }

    public static void LoadScene(string scene)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.crossin(scene));
    }

    public static void LoadScene(int index)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.crossin(SceneManager.GetSceneByBuildIndex(index).name));
    }

    IEnumerator crossin(string scene)
    {
        SpeedrunManager.Pause();
        canvas.sortingOrder = int.MaxValue;

        while (img.color != Color.black)
        {
            img.color = Vector4.MoveTowards(img.color, Color.black, 0.03f);
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadSceneAsync(scene);

        StartCoroutine(crossout());
    }

    IEnumerator crossout()
    {
        Color c = Color.black;
        c.a = 0;

        while (img.color != c)
        {
            img.color = Vector4.MoveTowards(img.color, c, 0.03f);
            yield return new WaitForEndOfFrame();
        }

        canvas.sortingOrder = int.MinValue;
        SpeedrunManager.Play();
    }
}
