using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Sanic
{
    public class SprintHelper
    {
        public ModConfig Config { get; private set; }

        public bool IsSprinting { get; private set; } = false;

        public SprintHelper(ModConfig config, IModHelper helper)
        {
            Config = config;
            helper.Events.GameLoop.UpdateTicking += OnUpdateTicking;
        }

        public void OnUpdateTicking(object _, UpdateTickingEventArgs args)
        {
            // stop sprinting immediately if the run/walk button is held
            if (IsRunWalkButtonHeld && IsSprinting)
            {
                IsSprinting = false;
            }

            // bail out if we are not sprinting
            if (!IsSprinting)
                return;

            // set speed to sprint speed if we are sprinting and speed is less than sprint speed
            if (Game1.player.Speed < Config.SprintSpeed)
            {
                ForceSprintSpeed();
            }

            // remove some stamina if we have been sprinting for long enough
            if (args.Ticks % Config.StaminaLossTickCount == 0)
            {
                RemoveStamina();
            }
        }

        public void OnSprintKeyPressed(object _, ButtonPressedEventArgs args)
        {
            // sprint only if the player is not currently holding the run/walk button
            if (IsRunWalkButtonHeld)
                return;

            IsSprinting = true;
        }

        public void OnSprintKeyReleased(object _, ButtonReleasedEventArgs args)
        {
            // no matter what, when sprint key is released, stop sprinting.
            IsSprinting = false;
        }

        public bool IsRunWalkButtonHeld
        {
            get
            {
                // run button is NOT being held when all of the assigned run keys are up
                return !Game1.areAllOfTheseKeysUp(Game1.GetKeyboardState(), Game1.options.runButton);
            }
        }

        private void RemoveStamina()
        {
            // don't remove stamina if we aren't sprinting or if we can't move
            if (!IsSprinting || !Context.CanPlayerMove)
                return;

            Game1.player.Stamina -= Game1.player.MaxStamina * Config.StaminaLossModifier;
        }

        private void ForceSprintSpeed()
        {
            if (!IsSprinting)
                return;

            Game1.player.Speed = Config.SprintSpeed;
        }
    }
}
