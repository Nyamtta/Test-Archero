using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PausBar : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Text = default;
    [SerializeField] private GameObject PausBarObj = default;

    private void Start() {

        PausBarObj.SetActive(false);

    }

    public void GameOver(string text) {

        PausBarObj.SetActive(true);
        
        Text.text = text;
        Time.timeScale = 0;

    }

    public void MenuButton() {

        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel() {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
