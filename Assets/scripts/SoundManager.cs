using UnityEngine;
using CodeMonkey.Utils;

public static class SoundManager
{
    public enum Sound
    {
        BirdJump,
        Score,
        Lose,
        ButtonClick,
    }

    public static void PlaySound(Sound sound)
    {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.GetInstance().sonudAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    public static void AddButtonSounds(this Button_UI buttonUI)
    {
        buttonUI.ClickFunc += () => PlaySound(Sound.ButtonClick);
    }
}