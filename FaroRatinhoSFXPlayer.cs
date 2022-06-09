using Terraria;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace FaroRatinhoSFX
{
    public class FaroRatinhoSFXPlayer : ModPlayer
    {

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            int count = 0;
            foreach (var sfxEntry in FaroRatinhoSFX.Instance.Sounds)
            {
                var sfx = sfxEntry.Value;
                if (!string.IsNullOrEmpty(sfx.defaultKeyBind) && sfx.keybind.JustPressed)
                {
                    var config = ModContent.GetInstance<FaroRatinhoSFXConfig>();

                    var color = new Color(config.MessageColor.R, config.MessageColor.G, config.MessageColor.B);
                    if (Player.team > 0 && config.MessageColorAsTeam)
                    {
                        var team = Player.team;
                        color = new Color(Main.teamColor[team].R, Main.teamColor[team].G, Main.teamColor[team].B);
                    }
                    ChatHelper.BroadcastChatMessageAs((byte)Player.whoAmI, NetworkText.FromLiteral("/" + sfx.name), color);
                    count++;

                    if (count >= ModContent.GetInstance<FaroRatinhoSFXServerConfig>().MaxSounds) break;

                }
            }
        }

    }
}