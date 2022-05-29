using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedrunToggle : MonoBehaviour
{
    Toggle t;

    private void Start()
    {
        t = GetComponent<Toggle>();
    }

    public void Toggle() =>
        SpeedrunManager.speedrun = t.isOn;
}
