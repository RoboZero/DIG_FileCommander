using System.Linq;
using Source.Logic.State;
using Source.Utility;
using UnityEngine;

namespace Source.Visuals.MemoryStorage
{
    public class ProcessorStorageBehavior : LineStorageBehavior
    {
        public Processor Processor => processor;

        [Header("Dependencies")]
        [SerializeField] private int processorIndex = 0;

        private Processor processor;
        
        protected override void UpdateStorageFromState(GameState gameState)
        {
            var player = gameState.Players.FirstOrDefault(player => player.Id == playerId);
            if (player == null)
            {
                Debug.LogWarning($"Failed to read from processor: playerId {playerId} is invalid, gamestate players count {gameState.Players.Count}");
                return;
            }

            if (!player.Processors.InBounds(processorIndex))
            {
                Debug.LogWarning($"Failed to read from processor: processor index {processorIndex} is invalid, gamestate players count {gameState.Players.Count}");
                return;
            }

            processor = player.Processors[processorIndex];
            itemStorageSize = processor.ProcessorStorage.Length;
            state = processor.ProcessorStorage;
            level = gameState.Level;
        }
    }
}
