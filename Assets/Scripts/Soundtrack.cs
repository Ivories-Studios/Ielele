using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();
    [SerializeField] List<AudioClip> bossfight = new List<AudioClip>();
    [SerializeField] AudioSource proTV;

    List<AudioClip> availableList;

    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        availableList = clips;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_audioSource.isPlaying && !proTV.isPlaying)
        {
            if(Random.Range(0, 100) == 0)
            {
                proTV.Play();
            }
            _audioSource.clip = availableList[Random.Range(0, availableList.Count)];
            _audioSource.Play();
        }
    }

    public void SwitchSoundtracks()
    {
        if(availableList == clips)
        {
            availableList = bossfight;
        }
        else if(availableList == bossfight)
        {
            availableList = clips;
        }
        _audioSource.clip = availableList[Random.Range(0, availableList.Count)];
        _audioSource.Play();
    }
}
