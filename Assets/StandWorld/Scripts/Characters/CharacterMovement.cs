using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StandWorld.Characters.AI;
using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.World;
using UnityEngine;
using Task = StandWorld.Characters.AI.Task;

namespace StandWorld.Characters
{
    public class CharacterMovement
    {
        public Action<Direction> onChangeDirection;
        public Vector2Int position { get; protected set; }
        public Direction lookingAt { get; protected set; }
        public Vector2Int destination { get; protected set; }

        public Queue<Vector2Int> path => _path;

        public Vector3 visualPosition
        {
            get
            {
                return new Vector3(
                    Mathf.Lerp(position.x, _nextPosition.x, _movementPercent),
                    Mathf.Lerp(position.y, _nextPosition.y, _movementPercent),
                    LayerUtils.Height(Layer.Count)
                );
            }
        }

        private float _movementPercent;
        private Vector2Int _nextPosition;
        private bool _hasDestination;
        private Queue<Vector2Int> _path;
        private float _speed = 0.05f;
        private BaseCharacter _character;

        public CharacterMovement(Vector2Int position, BaseCharacter character)
        {
            this.position = position;
            _character = character;
            ToolBox.Instance.map[this.position].characters.Add(_character);
            ResetMovement();
        }

        private void UpdateLookingAt(Vector2Int nextPos)
        {
            Direction original = lookingAt;
            Vector2Int dir = nextPos - position;

            if (dir.x > 0)
            {
                lookingAt = Direction.E;
            }
            else if (dir.x < 0)
            {
                lookingAt = Direction.W;
            }
            else if (dir.y > 0)
            {
                lookingAt = Direction.N;
            }
            else
            {
                lookingAt = Direction.S;
            }
            
            if(lookingAt != original && onChangeDirection != null)
            {
                onChangeDirection(lookingAt);
            }
        }

        public void Move(Task task)
        {
            if (_hasDestination == false)
            {
                PathResult pathResult = Pathfinder.GetPath(position, task.targets.currentPosition);

                if (pathResult.success == false)
                {
                    task.state = TaskState.Failed;
                    ResetMovement();
                    return;
                }

                _hasDestination = true;
                _path = new Queue<Vector2Int>(pathResult.path);
                destination = task.targets.currentPosition;
            }

            if (destination == position)
            {
                ResetMovement();
                return;
            }

            if (position == _nextPosition)
            {
                _nextPosition = _path.Dequeue();
                UpdateLookingAt(_nextPosition);
            }

            float distance = GameUtils.Distance(position, _nextPosition);
            float distanceThisFrame = _speed * ToolBox.Instance.map[position].pathCost;
            _movementPercent += distanceThisFrame / distance;

            if (_movementPercent >= 1f)
            {
                ToolBox.Instance.map[position].characters.Remove(_character);
                ToolBox.Instance.map[_nextPosition].characters.Add(_character);
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