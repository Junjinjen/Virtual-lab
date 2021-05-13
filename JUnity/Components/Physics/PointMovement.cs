using SharpDX;
using System;
using System.Collections.Generic;

namespace JUnity.Components.Physics
{
    public class PointMovement
    {
        public event EventHandler<EventArgs> OnAnimationEnd;

        private bool _isAnimationEnd;
        private bool _move;
        private bool Last => Points.Count - 2 <= PointIndex;

        public PointMovement(GameObject gameObject, Vector3 startPos)
        {
            Points = new List<Vector3>();
            Points.Add(startPos);
            Speeds = new List<float>();
            GameObject = gameObject;
            _move = false;
            DefaultSpeed = 1;
        }

        public GameObject GameObject { get; set; }

        public List<Vector3> Points { get; }

        public List<float> Speeds { get; }

        public float DefaultSpeed { get; set; }

        public int PointIndex { get; set; }

        public void Move(float deltaT)
        {
            float _steap;
            if (_move)
            {
                if (Speeds.Count > PointIndex)
                {
                    _steap = (deltaT * Speeds[PointIndex]) / (Points[PointIndex + 1] - GameObject.Position).Length();
                    GameObject.Position = Vector3.Lerp(GameObject.Position, Points[PointIndex + 1], _steap > 1 ? 1 : _steap);
                }
                else
                {
                    _steap = (deltaT * DefaultSpeed) / (Points[PointIndex + 1] - GameObject.Position).Length();
                    GameObject.Position = Vector3.Lerp(GameObject.Position, Points[PointIndex + 1], _steap > 1 ? 1 : _steap);
                }
            }

            if(GameObject.Position == Points[PointIndex + 1])
            {
                if (!Last)
                {
                    _move = true;
                    PointIndex++;
                }
                else
                {
                    _move = false;
                    if (!_isAnimationEnd)
                    {
                        OnAnimationEnd?.Invoke(this, new EventArgs());
                        _isAnimationEnd = true;
                    }
                }
            }
        }

        public void Start()
        {
            _move = Points.Count - 2 >= PointIndex;
        }

        public void Reset()
        {
            _move = false;
            PointIndex = 0;
            _isAnimationEnd = false;
        }

        public void Stop()
        {
            _move = false;
        }
    }
}
