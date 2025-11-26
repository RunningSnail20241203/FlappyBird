using System;
using UnityEngine;

public class SettingManager : MonoSingleton<SettingManager>
{
    public bool IsMusicEnabled { get; private set; }
    public bool IsSoundEnabled { get; private set; }
    
    public event Action<bool> OnMusicToggleChanged;
    public event Action<bool> OnSoundToggleChanged;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        IsMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        IsSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
    }

    public void SetMusicEnabled(bool arg0)
    {
        IsMusicEnabled = arg0;
        PlayerPrefs.SetInt("MusicEnabled", arg0 ? 1 : 0);
        OnMusicToggleChanged?.Invoke(arg0);
    }

    public void SetSoundEnabled(bool arg0)
    {
        IsSoundEnabled = arg0;
        PlayerPrefs.SetInt("SoundEnabled", arg0 ? 1 : 0);
        OnSoundToggleChanged?.Invoke(arg0);
    }
}