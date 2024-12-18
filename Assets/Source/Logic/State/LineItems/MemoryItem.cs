using System;
using Source.Logic.Events;
using UnityEngine;

namespace Source.Logic.State.LineItems
{
    [Serializable]
    public class MemoryItem : LineItem
    {
        public int OwnerId;
        public string Definition;
        public int CurrentRunProgress;
        public int MaxRunProgress;
        public int CurrentRunCount;
        public int MaxRunCount;
        public float DataSize;

        public virtual MemoryItem CreateInstance()
        {
            return new MemoryItem()
            {
                Definition = Definition,
                OwnerId = OwnerId,
                CurrentRunProgress = CurrentRunProgress,
                MaxRunProgress = MaxRunProgress,
                DataSize = DataSize,
            };
        }

        public virtual void Tick(EventTracker eventTracker, GameState gameState)
        {
            Debug.Log($"{Definition} tick. Progress: {CurrentRunProgress} / {MaxRunProgress}");
            
            CurrentRunProgress++;

            if (CurrentRunProgress <= MaxRunProgress) return;
            
            Run(eventTracker, gameState);
            CurrentRunProgress = 0;
        }

        protected virtual void Run(EventTracker eventTracker, GameState gameState)
        {
            CurrentRunCount++;
        }

        public override string ToString()
        {
            return $"{Definition}:(OId: {OwnerId}, Prog: {CurrentRunProgress}, Max Prog: {MaxRunProgress})";
        }
    }
}