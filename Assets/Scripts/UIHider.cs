using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIHider : MonoBehaviour
{
    [SerializeField] private InsideChecker _insideChecker;
    [SerializeField] private float _opacityChangeSpeed = 0.5f;

    private Image _image;

    private float _opacity;
    private float _maxOpacity = 1;
    private float _minOpacity = 0;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        Work();
    }

    private void Work()
    {
        if (_insideChecker.IsPlayerInside)
            ChangeOpacityTowards(_maxOpacity);
        else
            ChangeOpacityTowards(_minOpacity);

        Color imageColor = _image.color;
        imageColor.a = _opacity;

        _image.color = imageColor;
    }

    private void ChangeOpacityTowards(float targetVolume)
    {
        _opacity = Mathf.MoveTowards(_opacity, targetVolume, _opacityChangeSpeed * Time.deltaTime);
    }
}
