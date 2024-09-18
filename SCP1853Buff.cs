using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SCP1853Buff
{
    public class SCP1853Buff : Plugin<Config>
    {
        public override string Name => "SCP1853Buff";
        public override string Author => "Vretu";
        public override string Prefix { get; } = "SCP1853Buff";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(8, 9, 8);

        private Timer staminaCheckTimer;
        private HashSet<Player> playersWithEffect;
        public override void OnEnabled()
        {
            staminaCheckTimer = new Timer(100);
            staminaCheckTimer.Elapsed += OnStaminaCheck;
            staminaCheckTimer.Start();

            playersWithEffect = new HashSet<Player>();
            Exiled.Events.Handlers.Player.ReceivingEffect += OnReceivingEffect;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            if (staminaCheckTimer != null)
            {
                staminaCheckTimer.Stop();
                staminaCheckTimer.Dispose();
            }
            Exiled.Events.Handlers.Player.ReceivingEffect -= OnReceivingEffect;

            base.OnDisabled();
        }
        private void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            if (ev.Effect.GetEffectType() == EffectType.Scp1853)
            {
                if (ev.Intensity > 0) // Add player, if effect is active
                {
                    playersWithEffect.Add(ev.Player);
                }
                else // Remove Player, if effect is removed
                {
                    playersWithEffect.Remove(ev.Player);
                }
            }
        }
        private void OnStaminaCheck(object sender, ElapsedEventArgs e)
        {
            // Iteration by players with SCP-1853 effect
            foreach (var player in playersWithEffect.ToList())
            {
                if (player.Stamina <= Config.StaminaThreshold)
                {
                    // Set Stamina to a new value, but only if it makes sense to do so
                    player.Stamina = Math.Max(player.Stamina, Config.StaminaAdded);

                    // Add health only when stamina is updated
                    if (player.Stamina == Config.StaminaAdded)
                    {
                        player.Health -= Config.HpRemoved;
                    }
                }
            }
        }
    }
}
