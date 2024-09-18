using Exiled.API.Interfaces;

namespace SCP1853Buff
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float StaminaAdded { get; set; } = 0.060f;
        public float StaminaThreshold { get; set; } = 0.025f;
        public float HpRemoved { get; set; } = 0.5f;
    }
}