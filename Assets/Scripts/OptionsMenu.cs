using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] VolumeProfile volume;
    [SerializeField] AudioMixer mixer;
    [SerializeField] List<Slider> audioSliders = new List<Slider>();

    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        volume = volume == 0 ? 0.0001f : volume / 10;
        mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        volume = volume == 0 ? 0.0001f : volume / 10;
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat("EffectsVolume", volume);
        volume = volume == 0 ? 0.0001f : volume / 10;
        mixer.SetFloat("Effects", Mathf.Log10(volume) * 20);
    }

    public void SetVHSColorShift(bool active)
    {
        VHS_RLPRO vhsColor;
        if(volume.TryGet(out vhsColor))
        {
            vhsColor.active = active;
            PlayerPrefs.SetInt("vhsColor", active ? 1 : 0);
        }
    }

    public void SetNoise(bool active)
    {
        Noise_RLPRO noise;
        if (volume.TryGet(out noise))
        {
            noise.active = active;
            PlayerPrefs.SetInt("noise", active ? 1 : 0);
        }
    }

    public void SetScanlines(bool active)
    {
        VHSScanlines_RLPRO vhsScanlines;
        if(volume.TryGet(out vhsScanlines))
        {
            vhsScanlines.active = active;
            PlayerPrefs.SetInt("vhsScanlines", active ? 1 : 0);
        }
    }

    public void ReadPrefs()
    {
        SetMasterVolume(audioSliders[0].value = PlayerPrefs.GetFloat("MasterVolume", 10));
        SetMusicVolume(audioSliders[1].value = PlayerPrefs.GetFloat("MusicVolume", 10));
        SetEffectsVolume(audioSliders[2].value = PlayerPrefs.GetFloat("EffectsVolume", 10));
        VHS_RLPRO vhsColor;
        if (volume.TryGet(out vhsColor))
        {
            vhsColor.active = PlayerPrefs.GetInt("vhsColor", 1) != 0;
        }
        Noise_RLPRO noise;
        if (volume.TryGet(out noise))
        {
            noise.active = PlayerPrefs.GetInt("noise", 1) != 0;
        }
        VHSScanlines_RLPRO vhsScanlines;
        if (volume.TryGet(out vhsScanlines))
        {
            vhsScanlines.active = PlayerPrefs.GetInt("vhsScanlines", 1) != 0;
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        PlayerPrefs.Save();
    }
}
