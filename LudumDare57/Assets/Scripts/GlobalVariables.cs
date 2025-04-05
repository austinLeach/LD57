using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;

public static class GlobalVariables
{
    public static bool Timer(ref bool isChanging, ref float timer)
    {
        if (isChanging)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                isChanging = false;
            }
        }
        return isChanging;
    }
    public static float timeInAudio = 0f;   
    public static void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
