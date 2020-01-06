using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System.Collections.Generic;

namespace Sanic
{
    public class ModEntry : Mod
    {
        public ModConfig Config { get; private set; }
        public SprintHelper Sprint { get; private set; }

        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();
            Sprint = new SprintHelper(Config, helper);

            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.Input.ButtonReleased += OnButtonReleased;
        }

        public void OnButtonPressed(object sender, ButtonPressedEventArgs args)
        {
            // if we have just pressed the sprint key, ignore the event and pass it on to the sprint helper.
            if (args.Button == Config.SprintKey)
            {
                Sprint.OnSprintKeyPressed(sender, args);
            }
        }

        public void OnButtonReleased(object sender, ButtonReleasedEventArgs args)
        {
            // if we have just released the sprint key, ignore the event and pass it on to the sprint helper.
            if (args.Button == Config.SprintKey)
            {
                Sprint.OnSprintKeyReleased(sender, args);
            }
        }
    }
}
