using System;
using UnityEngine;

namespace GameControllers.GameEntites.Balls.Properties
{
    public interface ICanInitBall<T>
    {
        public void Init(Vector3 startPosition, Action<T> returnAction);
    }
}

