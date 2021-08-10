using System;
using UnityEngine;

public class WaitingToStart : MonoBehaviour
{
    private void Start()
    {
        Bird.GetInstance().OnStartedPlaying += WaitingToStartWindow_OnStartedPlaying;
        Show();
    }

    private void WaitingToStartWindow_OnStartedPlaying(object sender, EventArgs e)
    {
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}