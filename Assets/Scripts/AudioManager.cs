using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioSource _soundObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundEffectClip(AudioClip audioClip,Transform soundLocation, float volume)
    {
        AudioSource source = Instantiate(_soundObject, soundLocation.position, Quaternion.identity);

        _soundObject.clip = audioClip;
        Debug.Log(audioClip.length);
        source.volume = volume;
        source.Play();

        Destroy(source, source.clip.length);
    }
}
