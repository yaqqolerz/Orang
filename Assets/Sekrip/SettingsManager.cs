using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    private bool isMuted = false;
    private float volume = 1.0f;
    private const string MUTE_KEY = "isMuted";
    private const string VOLUME_KEY = "volume";
    private const float MIN_VOLUME = 0f;
    private const float MAX_VOLUME = 1f;

    public void SetPreferences()
    {
        PlayerPrefs.SetInt(MUTE_KEY, isMuted ? 1 : 0);
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
    }

    public void LoadPreferences()
    {
        if (PlayerPrefs.HasKey(MUTE_KEY))
        {
            isMuted = PlayerPrefs.GetInt(MUTE_KEY) == 1;
        }

        if (PlayerPrefs.HasKey(VOLUME_KEY))
        {
            volume = PlayerPrefs.GetFloat(VOLUME_KEY);
        }
    }

    public void ToggleAudioMute(bool isOn)
    {
        isMuted = isOn;
        audioMixer.SetFloat("Volume", isMuted ? MIN_VOLUME : volume);
    }

    public void SetVolume(float value)
    {
        volume = value;
        audioMixer.SetFloat("Volume", isMuted ? MIN_VOLUME : volume);
    }

    public void SaveSettings()
    {
        SetPreferences();
    }

    private void Start()
    {
        LoadPreferences();
        if (!isMuted)
        {
            audioMixer.SetFloat("Volume", volume);
        }
    }
}
