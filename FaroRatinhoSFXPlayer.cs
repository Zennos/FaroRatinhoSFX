using Terraria;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace FaroRatinhoSFX
{
    public class FaroRatinhoSFXPlayer : ModPlayer
    {

        public override void ProcessTriggers(TriggersSet triggersSet)
        {

            if (FaroRatinhoSFX.Instance.RandomSoundKeybind.JustPressed)
            {
                var randSfx = FaroRatinhoSFX.Instance.GetRandomSFX();
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    FaroRatinhoSFX.Instance.PlaySound(randSfx, Player.whoAmI, randSfx.description);
                }
                else
                {
                    ChatHelper.SendChatMessageFromClient(new ChatMessage("/som"));
                }

                return;
            }

            foreach (var sfx in FaroRatinhoSFX.Instance.Sounds.Values)
            {
                if (!string.IsNullOrEmpty(sfx.defaultKeyBind) && sfx.keybind.JustPressed)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        FaroRatinhoSFX.Instance.PlaySound(sfx, Player.whoAmI, sfx.description);
                        break;
                    }
                    else
                    {
                        SendSoundCommandToChat(sfx);
                        break;
                    }

                }
            }
        }

        public void SendSoundCommandToChat(FaroRatinhoSound sfx, bool byRandom = false)
        {
            var config = ModContent.GetInstance<FaroRatinhoSFXConfig>();

            var color = new Color(config.MessageColor.R, config.MessageColor.G, config.MessageColor.B);
            if (Player.team > 0 && config.MessageColorAsTeam)
            {
                var team = Player.team;
                color = new Color(Main.teamColor[team].R, Main.teamColor[team].G, Main.teamColor[team].B);
            }
            var message = "/" + sfx.name;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                FaroRatinhoSFX.Instance.PlaySound(sfx, Player.whoAmI, sfx.description);
            }
            else
            {
                ChatHelper.SendChatMessageFromClient(new ChatMessage(message));
            }
        }

    }
}