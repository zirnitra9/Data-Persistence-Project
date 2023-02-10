using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;

    public void StartNewGame()
    {
        ResolvePlayerName();
        SceneManager.LoadScene(1);
    }

    void ResolvePlayerName()
    {
        string str = nameInput.text;

        if (str != "")
        {
            Memorius.Instance.PlayerName = str;
        }
        else
        {
            Memorius.Instance.PlayerName = "DEFAULT";
        }
    }
}
