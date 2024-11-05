using Source.Logic;
using Source.Logic.State;
using Source.Serialization;
using UnityEngine;

namespace Source.Visuals.LineStorage
{
    public abstract class LineStorageBehavior : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected int playerID = 0;
        [SerializeField] protected int itemStorageSize;
    
        [SerializeField] protected GameStateLoader gameStateLoader;
    
        public Logic.State.LineItems.LineStorage State => state;
        protected Logic.State.LineItems.LineStorage state;
    
        public void Tick()
        {
            if(gameStateLoader.GameState != null)
                UpdateStorageFromState(gameStateLoader.GameState);
        }

        protected abstract void UpdateStorageFromState(GameState gameState);
    }
}
