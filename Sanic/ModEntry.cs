using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Sanic
{
    public class ModEntry : Mod
    {
        private readonly int sprintSpeed = 10;
        private readonly float staminaLossModifier = 0.01f;
        private readonly int staminaLossModulo = 90;

        private int sprintTickCounter = 0;
        private bool isSprinting = false;

        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.Input.ButtonReleased += OnButtonReleased;
            helper.Events.GameLoop.UpdateTicking += OnUpdateTicking;
        }

        public void OnUpdateTicking(object sender, UpdateTickingEventArgs args)
        {
            if (isSprinting)
            {
                // make player sprint if player _should_ be sprinting but isn't
                if (Game1.player.Speed < sprintSpeed)
                    Game1.player.Speed = sprintSpeed;

                // make player lose some energy if player is moving while sprinting
                if (Game1.player.isMoving())
                {
                    bool shouldLoseStamina = (++sprintTickCounter % staminaLossModulo) == 0;

                    if (shouldLoseStamina)
                    {
                        Game1.player.Stamina -= Game1.player.MaxStamina * staminaLossModifier;
                    }
                }
            }
        }

        public void OnButtonPressed(object sender, ButtonPressedEventArgs args)
        {
            // if we have just pressed space, and we are not holding left shift, we are now sprinting.
            if (args.Button == SButton.Space && !args.IsDown(SButton.LeftShift))
            {
                isSprinting = true;
            }

            // cannot sprint with left-shift down
            if (args.Button == SButton.LeftShift)
            {
                isSprinting = false;
            }
        }

        public void OnButtonReleased(object sender, ButtonReleasedEventArgs args)
        {
            // cannot sprint without spacebar down
            if (args.Button == SButton.Space)
            {
                isSprinting = false;
            }

            // if we have just released left shift, but we are still holding spacebar, we are now sprinting.
            else if (args.Button == SButton.LeftShift && args.IsDown(SButton.Space))
            {
                isSprinting = true;
            }
        }
    }
}
