using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private const string VolumePrefKey = "VolumePref";
    private const string MutePrefKey = "MutePref";

    public Sprite muteSprite;
    public Sprite unmuteSprite;
    public Image muteButtonImage;

    private float currentVolume;
    private bool isMuted;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        LoadSettings();
        audioMixer.SetFloat("Volume", currentVolume); 
        /*ApplySettings();*/
        GameObject.Find("VolumeSlider").GetComponent<UnityEngine.UI.Slider>().value = currentVolume;
        GameObject.Find("MuteToggle").GetComponent<UnityEngine.UI.Toggle>().isOn = isMuted;
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        Debug.Log(currentVolume);
        audioMixer.SetFloat("Volume", currentVolume);
        PlayerPrefs.SetFloat(VolumePrefKey, currentVolume);
    }

    public void SetMute(bool isMute)
    {
        isMuted = isMute;
        audioMixer.SetFloat("Volume", isMuted ? -80 : currentVolume);
        PlayerPrefs.SetInt(MutePrefKey, isMuted ? 1 : 0);
        muteButtonImage.sprite = isMuted ? muteSprite : unmuteSprite;
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(VolumePrefKey))
        {
            currentVolume = PlayerPrefs.GetFloat(VolumePrefKey);
        }
        else
        {
            currentVolume = 0;
        }

        if (PlayerPrefs.HasKey(MutePrefKey))
        {
            isMuted = PlayerPrefs.GetInt(MutePrefKey) == 1 ? true : false;
        }
        else
        {
            isMuted = false;
        }
    }
    public void backtomenu()
    {
       /* ApplySettings();*/
        Debug.Log(PlayerPrefs.GetFloat("Volume"));
        SceneManager.LoadScene("Main");
    }


}
