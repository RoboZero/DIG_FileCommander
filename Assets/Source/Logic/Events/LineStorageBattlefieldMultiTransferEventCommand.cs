﻿using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Logic.State.Battlefield;
using Source.Logic.State.LineItems;
using Source.Utility;

namespace Source.Logic.Events
{
    public class LineStorageBattlefieldMultiTransferEventCommand : EventCommand
    {
        private LineStorage<MemoryItem> memoryStorage;
        private List<int> memorySlots;
        private List<LineStorage<BattlefieldItem>> battlefieldStorages;
        private List<int> battlefieldSlots;
        private LineStorageBattlefieldTransferEventCommand.TransferredItem transferredItem;
        private TransferEventOverrides transferEventOverrides;
        
        public LineStorageBattlefieldMultiTransferEventCommand(
            EventTracker eventTracker,
            List<LineStorage<BattlefieldItem>> battlefieldStorages,
            List<int> battlefieldSlots,
            LineStorage<MemoryItem> memoryStorage,
            List<int> memorySlots,
            LineStorageBattlefieldTransferEventCommand.TransferredItem transferredItem,
            TransferEventOverrides transferEventOverrides
        ) : base(eventTracker)
        {
            this.memoryStorage = memoryStorage;
            this.memorySlots = memorySlots;
            this.battlefieldStorages = battlefieldStorages;
            this.battlefieldSlots = battlefieldSlots;
            this.transferredItem = transferredItem;
            this.transferEventOverrides = transferEventOverrides;
        }
        
        public override async UniTask<bool> Apply(CancellationToken cancellationToken)
        {
            
            AddLog($"{GetType().Name} Starting multiple line storage transfers from storages {memoryStorage}: slots {memorySlots.ToItemString()} to slot {battlefieldStorages.ToItemString()}:{battlefieldSlots.ToItemString()}");
            var failurePrefix = $"Failed to multi transfer: ";

            var success = true;

            for (var index = 0; index < memorySlots.Count; index++)
            {
                if (index >= battlefieldSlots.Count)
                {
                    AddLog(failurePrefix + $"Unable to transfer at transfer {index}, from slots index {index} is greater than to slots count {battlefieldSlots.Count}");
                    success = false;
                    continue;
                }
                
                var result = await ApplyChildEventWithLog(new LineStorageBattlefieldTransferEventCommand(
                    eventTracker,
                    memoryStorage,
                    memorySlots[index],
                    battlefieldStorages[index],
                    battlefieldSlots[index],
                    transferredItem,
                    transferEventOverrides
                ));

                if (!result)
                    success = false;
            }

            if(success)
                AddLog($"Successfully multi transferred");
            else
                AddLog($"Failed to fully multi transfer");
            
            return success;
        }
    }
}