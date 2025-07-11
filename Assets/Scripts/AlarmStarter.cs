using System.Collections;
using System.Collections.Generic;
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
    }

    private void Update()
    {
        Work();
    }

    private void Work()
    {
        if (_volume <= 0 && _audioSource.isPlaying == true)
            _audioSource.Stop();
        if (_volume > 0 && _audioSource.isPlaying == false)
            _audioSource.Play();

        if (_insideChecker.IsPlayerInside)
            ChangeVolumeTowards(_maxVolume);
        else
            ChangeVolumeTowards(_minVolume);

        _audioSource.volume = _volume;
    }

    private void ChangeVolumeTowards(float targetVolume)
    {
        _volume = Mathf.MoveTowards(_volume, targetVolume, _volumeChangeSpeed * Time.deltaTime);
    }
}
