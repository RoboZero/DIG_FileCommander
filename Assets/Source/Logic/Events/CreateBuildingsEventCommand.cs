﻿using System;
using System.Collections.Generic;
using System.Text;
using Source.Logic.Data;
using Source.Utility;
using UnityEngine;

namespace Source.Logic.Events
{
    public class CreateBuildingsEventCommand : EventCommand
    {
        private BattlefieldStorage battlefieldStorage;
        private List<int> slots;
        private Building building;
        private bool forceIfOccupied;

        public CreateBuildingsEventCommand(
            BattlefieldStorage battlefieldStorage,
            List<int> slots,
            Building building,
            bool forceIfOccupied
        )
        {
            this.battlefieldStorage = battlefieldStorage;
            this.slots = slots;
            this.building = building;
            this.forceIfOccupied = forceIfOccupied;
        }

        public override bool Perform()
        {
            var logBuilder = new StringBuilder();
            logBuilder.AppendLine($"{ID} Creating buildings of type {building.Definition} in slots {slots.ToItemString()} of {battlefieldStorage}");

            var success = true;
            foreach (var slot in slots)
            {
                if (slot < 0 || slot >= battlefieldStorage.Items.Count)
                {
                    logBuilder.AppendLine($"Failed to create unit of type {building.Definition} in slot {slot} of {battlefieldStorage}: slot {slot} out of battlefield index bounds {battlefieldStorage.Items.Count}");
                    success = false;
                    continue;
                }
                
                battlefieldStorage.Items[slot] ??= new BattlefieldItem();
                if (!forceIfOccupied && battlefieldStorage.Items[slot].Unit != null)
                {
                    logBuilder.AppendLine($"Failed to create unit of type {building.Definition} in slot {slot} of {battlefieldStorage}: slot is occupied by {battlefieldStorage.Items[slot].Building.Definition}");
                    success = false;
                    continue;
                }
                
                battlefieldStorage.Items[slot].Building = building;
                logBuilder.AppendLine($"Successfully created building of type {building.Definition} in slot {slot} of {battlefieldStorage}");
            }
            
            Debug.Log(logBuilder);
            return success;
        }
    }
}