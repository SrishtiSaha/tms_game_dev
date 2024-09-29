using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIDeathManager : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void OnEnable()
    {
        Destructible.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        Destructible.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
