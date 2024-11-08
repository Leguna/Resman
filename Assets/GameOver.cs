using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject bg;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TMP_Text textFinish;

    public Action onRestart = delegate { };
    public Action onOpenMenu = delegate { };

    private void Awake()
    {
        Close();
        quitButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(Quit);
        restartButton.onClick.AddListener(() => onRestart?.Invoke());
        mainMenuButton.onClick.AddListener(() => onOpenMenu?.Invoke());
    }


    public void SetTextFinish(string text)
    {
        textFinish.text = text;
    }


    public void Open()
    {
        GameManager.gameSpeed = 0;
        gameObject.SetActive(true);
        gameOverPanel.SetActive(true);
        bg.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        bg.SetActive(false);
        GameManager.gameSpeed = 0;
    }

    public void Quit()
    {
        GameManager.gameSpeed = 1;
        Application.Quit();
    }
}