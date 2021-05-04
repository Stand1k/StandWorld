using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BackgroundInteract : MonoBehaviour
{
    private Transform _transform;
    private Vector2 _mousePosition;
    private Vector3 _lastMousePosition;
    private bool _isMove;
    public float zoom;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        _transform.DOMoveZ(zoom, 3).OnComplete(() =>
        {
            Vector2 objectPosition = _camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            _lastMousePosition = new Vector3(objectPosition.x, objectPosition.y, zoom);
            _transform.DOMove(_lastMousePosition, 0.5f).OnComplete(() => _isMove = true);
        });
    }

    private void Update()
    {
        if (_isMove)
        {
            _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objectPosition = _camera.ScreenToWorldPoint(_mousePosition);
            _transform.position = Vector3.Lerp(_lastMousePosition,new Vector3(objectPosition.x / 3, objectPosition.y / 3, zoom), Time.deltaTime);
            _lastMousePosition = _transform.position;
        }
    }
}