using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField]
    private TMP_Dropdown _resolutionDropdown;
    private Resolution[] _resolutions;

    private void Start()
    {
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
        _audioMixer.SetFloat("volume", volume);
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
