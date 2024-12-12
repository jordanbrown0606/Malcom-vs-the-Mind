using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _startingTrack;
    [SerializeField] private AudioClip _keyTrack;

    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        PlayAudio( _startingTrack );
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.angerKey == true)
        {
            PlayAudio(_keyTrack);
            this.enabled = false;
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        _source.Stop();
        _source.loop = true;
        _source.PlayOneShot(clip);

    }
}
