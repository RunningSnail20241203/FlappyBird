using System;
using UnityEngine;

public class SettingManager : MonoSingleton<SettingManager>
{
    public bool IsMusicEnabled { get; private set; }
    public bool IsSoundEnabled { get; private set; }
    
    public event Action<bool> OnMusicToggleChanged;
    public event Action<bool> OnSoundToggleChanged;

    private const string MusicEnabledKey = "MusicEnabled";
    private const string SoundEnabledKey = "SoundEnabled";
    
    protected override void OnInitialize()
    {
        base.OnInitialize();
        IsMusicEnabled = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1;
        IsSoundEnabled = PlayerPrefs.GetInt(SoundEnabledKey, 1) == 1;
    }

    public void SetMusicEnabled(bool arg0)
    {
        IsMusicEnabled = arg0;
        PlayerPrefs.SetInt(MusicEnabledKey, arg0 ? 1 : 0);
        OnMusicToggleChanged?.Invoke(arg0);
    }

    public void SetSoundEnabled(bool arg0)
    {
        IsSoundEnabled = arg0;
        PlayerPrefs.SetInt(SoundEnabledKey, arg0 ? 1 : 0);
        OnSoundToggleChanged?.Invoke(arg0);
    }
}