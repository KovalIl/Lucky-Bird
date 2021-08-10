using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System;

public class GameOverWindow : MonoBehaviour
{
    private Text scoreText;
    private Text highescoreText;

    private void Awake()
    {
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        highescoreText = transform.Find("highscoreText").GetComponent<Text>();

        transform.Find("retryButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.GameScene);
        };
        transform.Find("retryButton").GetComponent<Button_UI>().AddButtonSounds();

        transform.Find("mainMenuButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        };
        transform.Find("mainMenuButton").GetComponent<Button_UI>().AddButtonSounds();

        Hide();
    }

    private void Start()
    {
        Bird.GetInstance().OnDied += Bird_OnDied;
    }

    private void Bird_OnDied(object sender, EventArgs e)
    {      
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        scoreText.text = Level.GetInstance().GetPassedCount().ToString();
        if(Level.GetInstance().GetPassedCount() > Score.GetHighscore())
        {
            highescoreText.text = "NEW HIGHSCORE";
        }
        else
        {
            highescoreText.text = $"HIGHSCORE: {Score.GetHighscore()}";
        }
        gameObject.SetActive(true);
    }
}