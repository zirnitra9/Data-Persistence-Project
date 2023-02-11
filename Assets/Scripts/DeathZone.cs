using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();

        if (Memorius.Instance != null)
        {
            Memorius.Instance.UpdateHighScores(Memorius.Instance.PlayerName, Memorius.Instance.StealScoreFromMainManager());

            Memorius.Instance.SaveHighScores();
        }
    }
}
