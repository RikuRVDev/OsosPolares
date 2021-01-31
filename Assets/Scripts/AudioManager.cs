using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{

    private AudioSource _audioSource;

    // Music
    public AudioClip _music;

    // Footsteps
    public AudioClip[] _footsteps;

    // Companion
    public AudioClip _companion;

    // Fail
    public AudioClip _fail;

    // Victory
    public AudioClip _victory;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        _audioSource.volume = PlayerPrefs.GetFloat(Constants.AUDIO_VOLUME);
        _audioSource.clip = _music;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void PlayFootstep() {
        AudioClip randomClip = _footsteps[Random.Range(0, _footsteps.Length)];
        _audioSource.PlayOneShot(randomClip);
    }

    public void PlayCompanion() {
        _audioSource.PlayOneShot(_companion);
    }

    public void PlayFail() {
        _audioSource.loop = false;
        _audioSource.Stop();
        _audioSource.clip = _fail;
        _audioSource.Play();
    }

    public void PlayVictory() {
        _audioSource.loop = false;
        _audioSource.Stop();
        _audioSource.clip = _victory;
        _audioSource.Play();
    }

    public bool GetIsPlaying() {
        return _audioSource.isPlaying;
    }

}
