using Terraria;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Linq;
using static FaroRatinhoSFX.FaroRatinhoSFX;
using static Humanizer.In;

namespace FaroRatinhoSFX
{
    public class FaroRatinhoSFXPlayer : ModPlayer
    {
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            base.Kill(damage, hitDirection, pvp, damageSource);

            var clientConfig = ModContent.GetInstance<FaroRatinhoSFXConfig>();
            var serverConfig = ModContent.GetInstance<FaroRatinhoSFXServerConfig>();

            List<string> soundList = null;
            FaroRatinhoSound sfx = null;

            if (Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient && serverConfig.deathSounds.enabled) return;
                if (!clientConfig.deathSounds.enabled) return;

                soundList = clientConfig.deathSounds.useAnySound ? FaroRatinhoSFX.Instance.Sounds.Keys.ToList() : clientConfig.deathSounds.sounds;
                soundList.RemoveAll(x => !FaroRatinhoSFX.Instance.SoundsNamesAndAliases.ContainsKey(x));

                if (soundList.Count == 0) return;

                sfx = FaroRatinhoSFX.Instance.GetRandomSFXFromList(soundList);
                FaroRatinhoSFX.Instance.PlaySound(sfx, Player.whoAmI);
                return;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            // Only the server will execute the code below.

            if (!serverConfig.deathSounds.enabled) return;

            soundList = serverConfig.deathSounds.useAnySound ? FaroRatinhoSFX.Instance.Sounds.Keys.ToList() : serverConfig.deathSounds.sounds;
            soundList.RemoveAll(x => !FaroRatinhoSFX.Instance.SoundsNamesAndAliases.ContainsKey(x));

            if (soundList.Count == 0) return;

            sfx = FaroRatinhoSFX.Instance.GetRandomSFXFromList(soundList);
            FaroRatinhoSFX.Instance.SendSoundMessage(Player, sfx, false, SoundTypes.Death, serverConfig.deathSounds.playAtLocationOfDeath);
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