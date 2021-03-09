using System;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public const float PIXEL_PER_UNIT = 4f;
        public float zoomDesired { get; protected set; }
        public float zoomMin { get; protected set; }
        public float zoomMax { get; protected set; }

        public float zoom
        {
            get
            {
                return (zoomDesired * (ToolBox.map.size.x / PIXEL_PER_UNIT));
            }
        }
        public float sensitivity { get; protected set; }
        public Vector3 mousePosition { get; protected set; }
        private Vector3 _lastMousePosition;
        
        public RectI viewRect;

        private Camera _camera;

        private void UpdateCamera()
        {
            if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
            {
                Vector3 diff = _lastMousePosition - mousePosition;

                if (diff != Vector3.zero)
                {
                    _camera.transform.Translate(diff);
                    UpdateViewRect();
                }
            }

            zoomDesired -= zoomDesired * Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            zoomDesired = Mathf.Clamp(zoomDesired, zoomMin, zoomMax);

            if (zoom != _camera.orthographicSize)
            {
                _camera.orthographicSize = zoom;
                UpdateViewRect();
            }
        }

        private void UpdateViewRect()
        {
            viewRect = new RectI(
                new Vector2Int(
                    Mathf.FloorToInt(_camera.transform.position.x - _camera.orthographicSize * _camera.aspect - 1f), 
                    Mathf.FloorToInt(_camera.transform.position.y - _camera.orthographicSize - 1f)
                ),
                new Vector2Int(
                    Mathf.FloorToInt(_camera.transform.position.x + _camera.orthographicSize * _camera.aspect), 
                    Mathf.FloorToInt(_camera.transform.position.y + _camera.orthographicSize)
                )
            );
        }

        private void Start()
        {
            _camera = Camera.main;
            zoomMin = 0.1f;
            zoomMax = 1f;
            sensitivity = 1f;
            zoomDesired = 0.2f;
        }

        private void Update()
        {
            mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            UpdateCamera();
            _lastMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
