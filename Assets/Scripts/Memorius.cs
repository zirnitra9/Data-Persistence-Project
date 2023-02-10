using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memorius : MonoBehaviour
{
    //private variables
    static Memorius instance;
    static string playerName;

    //get and set
    public static Memorius Instance => instance;
    public string PlayerName { get => playerName; set => playerName = value; }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
