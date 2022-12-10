using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    [SerializeField] OptionsMenu optionsMenu;
    public static AudioMixerGroup effectMixer;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] private bool isTraining;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (!isTraining)
            optionsMenu.ReadPrefs();
        effectMixer = audioMixer.FindMatchingGroups("Master/Effects")[0];
        yield return new WaitForSeconds(0.4f);
        if (!isTraining)
        {
            DialogueManager.Instance.Dialogue1();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1.0f, AudioMixerGroup group = null)
    {
        if (clip == null) return;
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.spatialBlend = 0;
        if (group != null)
            audioSource.outputAudioMixerGroup = group;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
        Destroy(gameObject, clip.length * (Time.timeScale < 0.009999999776482582 ? 0.01f : Time.timeScale));
    }
}
