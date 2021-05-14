using System;
using StandWorld.Game;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public float zoomDesired { get; set; }
        public float zoomMin { get; set; }
        public float zoomMax { get; set; }
        public float zoom => (zoomDesired * (ToolBox.map.size.x / 4f));
        
        public float sensitivity { get; set; }
        public Vector3 mousePosition { get; set; }
        public Vector2Int tileMapMousePosition => new Vector2Int((int) mousePosition.x, (int) mousePosition.y);
        public RectI viewRect;

        private Camera _camera;
        private Vector3 _lastMousePosition;

        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;

        private void Start()
        {
            _camera = Camera.main;
            zoomMin = 0.1f;
            zoomMax = 1f;
            sensitivity = 1f;
            zoomDesired = 0.3f;

            var mapSize = Settings.mapSize.x;
            var mapSizeIndent = Settings.mapSize.x / 20f;

            _minX = -mapSizeIndent;
            _maxX = mapSize + mapSizeIndent;
            _minY = -mapSizeIndent;
            _maxY = mapSize + mapSizeIndent;
        }

        void ClamperPosition(Vector2 position)
        {
            var vertExtent = _camera.orthographicSize;    
            var horzExtent = vertExtent * Screen.width / Screen.height;
            var cameraTransform = _camera.transform;
            var xValue = position.x;
            var yValue = position.y;

            xValue = Mathf.Clamp(xValue, _minX + horzExtent, _maxY - horzExtent);
            yValue = Mathf.Clamp(yValue, _minY + vertExtent, _maxY - vertExtent);
            cameraTransform.position = new Vector3(xValue, yValue, cameraTransform.position.z);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector2(_minX,_maxY), new Vector2(_maxX,_maxY));
            Gizmos.DrawLine(new Vector2(_minX,_minY), new Vector2(_maxX,_minY));
            Gizmos.DrawLine(new Vector2(_minX,_maxY), new Vector2(_minX,_minY));
            Gizmos.DrawLine(new Vector2(_maxX,_maxY), new Vector2(_maxX,_minY));
        }

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
            
            //При зміні зума перераховуємо видимість камери
            if (zoom != _camera.orthographicSize)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoom, 10 * Time.deltaTime);
                UpdateViewRect();
            }
        }

        // Задає розміри видимості камери
        private void UpdateViewRect()
        {
            var position = _camera.transform.position;
            
            ClamperPosition(position);
            
            viewRect = new RectI(
                new Vector2Int(
                    Mathf.FloorToInt(position.x - _camera.orthographicSize * _camera.aspect + 1f), 
                    Mathf.FloorToInt(position.y - _camera.orthographicSize + 1f)
                ),
                new Vector2Int(
                    Mathf.FloorToInt(position.x + _camera.orthographicSize * _camera.aspect), 
                    Mathf.FloorToInt(position.y + _camera.orthographicSize)
                )
            );
            
            //Оновлює дані видимості у всіх регіонах
            ToolBox.map.UpdateVisibles();
        }


        private void Update()
        {
            mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            UpdateCamera();
            _lastMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
