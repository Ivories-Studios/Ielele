using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] VolumeProfile volume;
    [SerializeField] AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        ReadPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume(float volume)
    {
        volume = volume == 0 ? 0.0001f : volume / 10;
        mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        volume = volume == 0 ? 0.0001f : volume / 10;
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        volume = volume == 0 ? 0.0001f : volume / 10;
        mixer.SetFloat("Effects", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", volume);
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

    void ReadPrefs()
    {
        mixer.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
        mixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));
        mixer.SetFloat("Effects", PlayerPrefs.GetFloat("EffectsVolume"));
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
