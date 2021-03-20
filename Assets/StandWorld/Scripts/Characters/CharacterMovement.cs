using System.Collections.Generic;
using StandWorld.Characters.AI;
using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Characters
{
    public class CharacterMovement 
    {
        public Vector2Int position { get; protected set; }
        public Vector2Int destination { get; protected set; }
        public Direction lookingAt { get; protected set; }
        public Queue<Vector2Int> path => _path;

        public Vector3 visualPosition
        {
            get
            {
                return new Vector3(
                    Mathf.Lerp(position.x,_nextPosition.x,_movementPercent),
                    Mathf.Lerp(position.y,_nextPosition.y,_movementPercent),
                    LayerUtils.Height(Layer.Count)
                );
            }
        }

        private Queue<Vector2Int> _path;
        private float _movementPercent;
        private Vector2Int _nextPosition;
        private bool _hasDestination;
        private float _speed = 0.02f;

        public CharacterMovement(Vector2Int position)
        {
            this.position = position;
            ResetMovement();
        }

        private void UpdateLookingAt()
        {
            
        }

        public void Move(Vector2Int dest)
        {
            if (_hasDestination == false)
            {
                PathResult pathResult = Pathfinder.GetPath(position, dest);

                if (pathResult.success == false)
                {
                    Debug.LogError("Move success == false");
                    return;
                }
                
                _hasDestination = true;
                _path = new Queue<Vector2Int>(pathResult.path);
                destination = dest;
            }

            if (destination == position)
            {
                ResetMovement();
                return;
            }

            if (position == _nextPosition)
            {
                _nextPosition = _path.Dequeue();
                UpdateLookingAt();
            }

            float distance = Utils.Distance(position, _nextPosition);

            float distanceThisFrame = _speed * ToolBox.map[position].pathCost;
            _movementPercent += distanceThisFrame / distance;

            if (_movementPercent >= 1f)
            {
                position = _nextPosition;
                _movementPercent = 0f;
            }
        }

        private void ResetMovement()
        {
            destination = position;
            _hasDestination = false;
            _nextPosition = position;
            _movementPercent = 0f;
            _path = new Queue<Vector2Int>();
        }
    }
}
