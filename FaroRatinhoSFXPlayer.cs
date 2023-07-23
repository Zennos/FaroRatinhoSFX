using Terraria;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace FaroRatinhoSFX
{
    public class FaroRatinhoSFXPlayer : ModPlayer
    {
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            base.Kill(damage, hitDirection, pvp, damageSource);

            var clientConfig = ModContent.GetInstance<FaroRatinhoSFXConfig>();
            if (!clientConfig.deathSounds.enabled) return;

            var serverConfig = ModContent.GetInstance<FaroRatinhoSFXServerConfig>();
            if (Main.netMode != NetmodeID.SinglePlayer && !serverConfig.deathSounds.enabled) return;

            List<string> soundList = null;
            if (Main.netMode != NetmodeID.SinglePlayer && serverConfig.deathSounds.sounds.Count > 0)
            {
                soundList = serverConfig.deathSounds.useAnySound ? FaroRatinhoSFX.Instance.Sounds.Keys.ToList() : serverConfig.deathSounds.sounds;
            } else if (clientConfig.deathSounds.sounds.Count > 0)
            {
                soundList = clientConfig.deathSounds.useAnySound ? FaroRatinhoSFX.Instance.Sounds.Keys.ToList() :  clientConfig.deathSounds.sounds;
            }

            if (soundList == null) return;

            soundList.RemoveAll(x => !FaroRatinhoSFX.Instance.SoundsNamesAndAliases.ContainsKey(x));

            if (soundList.Count == 0) return;

            var sfx = FaroRatinhoSFX.Instance.GetRandomSFXFromList(soundList);

            FaroRatinhoSFX.Instance.PlaySound(sfx, Player.whoAmI);
            if (Main.netMode == NetmodeID.SinglePlayer) return;

            FaroRatinhoSFX.Instance.SendSoundMessage(Player, sfx, false, true);
        }

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