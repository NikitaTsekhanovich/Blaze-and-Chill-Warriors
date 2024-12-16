using System;
using System.Collections.Generic;

namespace GameControllers.ObjectsPool
{
    public class PoolBase<T>
    {
        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;
        private readonly int _preloadCound;
        private Queue<T> _pool = new();

        public PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCound)
        {
            _preloadFunc = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;
            _preloadCound = preloadCound;

            Spawn(preloadFunc, preloadCound);
        }

        private void Spawn(Func<T> preloadFunc, int preloadCound)
        {
            for (var i = 0; i < preloadCound; i++)
                Return(preloadFunc());
        }

        public T Get()
        {
            var item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
            _getAction(item);
            
            return item;
        }

        public void Return(T item)
        {
            _returnAction(item);
            _pool.Enqueue(item);
        }
    }
}

