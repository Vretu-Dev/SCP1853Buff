﻿using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SCP1853Buff
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("How much stamina does the player receive when it runs out?")]
        public float StaminaAdded { get; set; } = 0.060f;
        [Description("Stamina level at which it starts to regenerate.")]
        public float StaminaThreshold { get; set; } = 0.025f;
        [Description("How much health is deducted?")]
        public float HpRemoved { get; set; } = 0.5f;
        [Description("Effect intensity.")]
        public byte MovementBoostIntensity { get; set; } = 10;
        [Description("Kills the player when their HP reaches zero.")]
        public bool KillOnZeroHp { get; set; } = true;
        [Description("Apply Bleeding Effect when HP is depleting (Visual Only)")]
        public bool VisualEffect { get; set; } = true;
        [Description("Recommended to set to true; this ignores receiving effects from other plugins or items.")]
        public bool IgnoreAnotherMovementBoost { get; set; } = true;
    }
}