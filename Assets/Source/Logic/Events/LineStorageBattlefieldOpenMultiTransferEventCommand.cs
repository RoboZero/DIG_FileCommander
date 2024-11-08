﻿using System.Collections.Generic;
using Source.Logic.State.Battlefield;
using Source.Logic.State.LineItems;
using Source.Utility;

namespace Source.Logic.Events
{
    // TODO: Rename huge name!
    public class LineStorageBattlefieldOpenMultiTransferEventCommand : EventCommand
    {
        private List<LineStorage<BattlefieldItem>> fromStorages;
        private List<int> fromSlots;
        private LineStorage<MemoryItem> toStorage;
        private LineStorageBattlefieldTransferEventCommand.TransferredItem transferredItem;
        private TransferEventOverrides transferEventOverrides;

        public LineStorageBattlefieldOpenMultiTransferEventCommand (
            List<LineStorage<BattlefieldItem>> fromStorages,
            List<int> fromSlots,
            LineStorage<MemoryItem> toStorage,
            LineStorageBattlefieldTransferEventCommand.TransferredItem transferredItem,
            TransferEventOverrides transferEventOverrides
        )
        {
            this.fromStorages = fromStorages;
            this.fromSlots = fromSlots;
            this.toStorage = toStorage;
            this.transferredItem = transferredItem;
            this.transferEventOverrides = transferEventOverrides;
        }

        
        public override bool Perform()
        {
            AddLog($"{GetType().Name} Starting multiple line storage transfers from slots {fromStorages.ToItemString()}:{fromSlots.ToItemString()} to all {toStorage} open slots");
            var failurePrefix = "Failed to start multiple line storage transfers to open slots: ";

            var openSlots = new List<int>();
            for (var index = 0; index < toStorage.Items.Count; index++)
            {
                var item = toStorage.Items[index];
                if(item == null || transferEventOverrides.CanSwitch){
                    openSlots.Add(index);
                }
            }

            if (openSlots.Count == 0)
            {
                AddLog(failurePrefix + $"No open slots");
                return false;
            }

            var result = PerformChildEventWithLog(new LineStorageBattlefieldMultiTransferEventCommand(
                fromStorages,
                fromSlots,
                toStorage,
                openSlots,
                transferredItem,
                transferEventOverrides
            ));

            return result;
        }
    }
}