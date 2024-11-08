﻿using System;
using Source.Logic.Events;
using Source.Logic.State.Battlefield;
using UnityEngine;

namespace Source.Logic.State.LineItems.Units
{
    public class UnitMemory : MemoryItem
    {
        public int Health;
        public int Power;
        public bool CanSwitchPlaces = true;
        public bool CanEngageCombat = true;
        
        protected override void Run(EventTracker eventTracker, GameState gameState)
        {
            Debug.Log("Unit ran!");
        }
        
        public override string ToString()
        {
            return $"{Definition}: (OId: {OwnerId} H {Health}, P {Power})";
        }
    }
}