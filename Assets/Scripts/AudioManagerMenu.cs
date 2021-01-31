using System.Collections;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{

    private AudioSource _audioSource;
    private static float _volume = 0.2f;

    // Menu music loop
    public AudioClip _menuLoop;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        _audioSource.volume = _volume;
        _audioSource.loop = false;
        _audioSource.Play();
        StartCoroutine("PlayAudioLoop");
    }

    private IEnumerator PlayAudioLoop() {
        yield return new WaitWhile(() => _audioSource.isPlaying);
        _audioSource.Stop();
        _audioSource.loop = true;
        _audioSource.clip = _menuLoop;
        _audioSource.Play();
    }

}
