using StardewModdingAPI;

namespace Sanic
{
    public class ModConfig
    {
        /// <summary>The button used to sprint.</summary>
        public SButton SprintKey { get; set; } = SButton.Space;

        /// <summary>The speed value for sprinting.</summary>
        public int SprintSpeed { get; set; } = 5;

        /// <summary>The percentage of max stamina that should be lost when sprinting.</summary>
        public float StaminaLossModifier { get; set; } = 0.01f;

        /// <summary>The number of ticks that should pass between stamina losses.</summary>
        public int StaminaLossTickCount { get; set; } = 90;
    }
}
