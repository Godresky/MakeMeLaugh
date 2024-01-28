using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField]
    private TMP_Dropdown _resolutionDropdown;
    private Resolution[] _resolutions;
    [SerializeField]
    private Slider _sensitiveSliderX;
    [SerializeField]
    private Slider _sensitiveSliderY;
    [SerializeField]
    private Slider _volumeSlider;

    private void Start()
    {
        _sensitiveSliderX.value = PlayerPrefs.GetFloat("SensitiveX", 35);
        _sensitiveSliderY.value = PlayerPrefs.GetFloat("SensitiveY", 0.5f);
        _volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);

        _resolutions = Screen.resolutions;

        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);
            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = 1;
            }
        }
         _resolutionDropdown.AddOptions(options);
    }

    public void SetVolume(float volume)
    {
        //_audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("Volum", volume);
        AudioListener.volume = volume;
    }

    public void SetSensitivityX(float sensitivity)
    {
        PlayerPrefs.SetFloat("SensitiveX", sensitivity);
    }

    public void SetSensitivityY(float sensitivity)
    {
        PlayerPrefs.SetFloat("SensitiveY", sensitivity);
    }

    public void SetResolution(int resoltionIndex)
    {
        Resolution resolution = _resolutions[resoltionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }    

    public void FullScreenToggle(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
}
