public class SettingViewModel : ViewModelBase
{
    public Observable<bool> IsMusicEnabled => GetObservable<bool>() as Observable<bool>;
    public Observable<bool> IsSoundEnabled => GetObservable<bool>() as Observable<bool>;


    public void ReturnMenu()
    {
        GameStateManager.Instance.AddCommand(new ReturnMainMenuCommand());
    }

    public void ToggleMusic(bool arg0)
    {
        SettingManager.Instance.SetMusicEnabled(arg0);
    }

    public void ToggleSound(bool arg0)
    {
        SettingManager.Instance.SetSoundEnabled(arg0);
    }

    protected override void InitializeProperties()
    {
        base.InitializeProperties();

        OnMusicToggleChanged(SettingManager.Instance.IsMusicEnabled);
        OnSoundToggleChanged(SettingManager.Instance.IsSoundEnabled);
        
        SettingManager.Instance.OnMusicToggleChanged += OnMusicToggleChanged;
        SettingManager.Instance.OnSoundToggleChanged += OnSoundToggleChanged;
    }

    protected override void OnDispose()
    {
        base.OnDispose();

        SettingManager.Instance.OnMusicToggleChanged -= OnMusicToggleChanged;
        SettingManager.Instance.OnSoundToggleChanged -= OnSoundToggleChanged;
    }

    private void OnMusicToggleChanged(bool obj)
    {
        IsMusicEnabled.SetValue(obj);
    }

    private void OnSoundToggleChanged(bool obj)
    {
        IsSoundEnabled.SetValue(obj);
    }
}