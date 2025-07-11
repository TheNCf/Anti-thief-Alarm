using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmStarter : MonoBehaviour
{
    [SerializeField] private InsideChecker _insideChecker;
    [SerializeField] private float _volumeChangeSpeed = 0.25f;

    private AudioSource _audioSource;

    private float _volume;
    private float _maxVolume = 1;
    private float _minVolume = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _insideChecker.PlayerEntered += StartRaiseVolume;
        _insideChecker.PlayerLeft += StartLowerVolume;
    }

    private void ChangeVolumeTowards(float targetVolume)
    {
        _volume = Mathf.MoveTowards(_volume, targetVolume, _volumeChangeSpeed * Time.deltaTime);
        _audioSource.volume = _volume;
    }

    private void StartRaiseVolume()
    {
        StopAllCoroutines();
        StartCoroutine(RaiseVolume());
    }

    private void StartLowerVolume() 
    {
        StopAllCoroutines();
        StartCoroutine(LowerVolume()); 
    }

    private IEnumerator RaiseVolume()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        while (_volume < _maxVolume)
        {
            ChangeVolumeTowards(_maxVolume);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator LowerVolume()
    {
        while (_volume > 0)
        {
            ChangeVolumeTowards(_minVolume);
            yield return new WaitForEndOfFrame();
        }

        if (_audioSource.isPlaying == true)
            _audioSource.Stop();
    }
}
