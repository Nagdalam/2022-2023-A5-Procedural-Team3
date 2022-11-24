using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Icon : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] Vector3 _offset;

    public Action _onActivation;
    public Action _onDeactivation;

    public RectTransform _rectTransform;
    public RawImage _rawImage;

    protected void Awake()
    {
        TryGetComponent(out _rawImage);
        TryGetComponent(out _rectTransform);

        if (!_rawImage)
            return;

        _rawImage.enabled = false;
    }

    void FixedUpdate()
    {
        if (!_rectTransform)
            return;

        if (!_target.transform)
            return;

        /*transform.position = Camera.main.transform.TransformPoint(_target.transform.position) + _offset;*/

        transform.position = _target.transform.position + _offset;
    }

    public virtual void Activation()
    {
        /*Debug.Log("activated");*/
        _rawImage.enabled = true;
    }

    public virtual void Deactivation()
    {
        /*Debug.Log("deactivated");*/
        _rawImage.enabled = false;
    }
}
