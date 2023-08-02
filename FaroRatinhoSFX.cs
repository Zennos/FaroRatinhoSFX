using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FaroRatinhoSFX
{
	public class FaroRatinhoSFX : Mod
	{
        public static FaroRatinhoSFX Instance;

        public Dictionary<string, string> SoundsNamesAndAliases = new();
        public Dictionary<string, FaroRatinhoSound> Sounds = new();
        public Regex rx = new Regex(@"/(\w+)", RegexOptions.Compiled);

        public ModKeybind RandomSoundKeybind;

        public enum SoundTypes
        {
            Normal = 0,
            Death = 1,
            Horse = 2
        }

        public override void Load()
        {

            Instance = this;

            AddSound("ahrur", "AHRUR AH BHRUAH BHRUU AH BHRUUUU AH", new string[]{ "ah" }, defaultKeyBind: "NumPad7");
            AddSound("ai", "Ai.", defaultKeyBind: "NumPad8");
            AddSound("aiaiai", "AAAAAAAAI AI AAAAAAAAI.", new string[] { "aiai" }, defaultKeyBind: "NumPad9");
            AddSound("atumalaca", "ATUMALACA KKKKKKKKKKKK.", new string[] { "haha", "risada", "kkk" }, defaultKeyBind: "NumPad4");
            AddSound("brasil", "BRASIL SIL SIL!.", defaultKeyBind: "NumPad5");
            AddSound("brincadeira", "Eh brincadeira hein.", defaultKeyBind: "NumPad6");
            AddSound("boing", "*Boing*", defaultKeyBind: "NumPad6");
            AddSound("boom", "*Boom*", new string[] { "bam", "bum", "thud", "vine" }, defaultKeyBind: "NumPad6");
            AddSound("calma", "Que isso meu filho calma.", new string[] { "filho" }, defaultKeyBind: "NumPad1");
            AddSound("cavalo", "Cavalo.", defaultKeyBind: "NumPad2");
            AddSound("chega", "CHEEEEGA!", new string[] { "xega" }, defaultKeyBind: "NumPad3");
            AddSound("corre", "*[i:54] corre! kkkkkk*", new string[] { "correr", "corra", "fujir", "fuga", "fugir" }, defaultKeyBind: "NumPad3");
            AddSound("danca", "Dansa gatinho dansa.", new string[] { "dansa", "gatinho", "dança" }, defaultKeyBind: "NumPad7");
            AddSound("demais", "Demais.", new string[] { "d+", "dmais" }, defaultKeyBind: "NumPad8");
            AddSound("dutudutuim", "Du tu du tuim", new string[] { "dutu", "du" }, defaultKeyBind: "NumPad8");
            AddSound("elegosta", "Ele gosta.", new string[] { "gosta", "ele" }, defaultKeyBind: "NumPad9");
            AddSound("galinha", "*[i:4016] Som de Galinha*", new string[] { "ga" }, defaultKeyBind: "NumPad4");
            AddSound("irra", "IIIIIIIIRRRRRRRRAAAAAAAAAAAA!!!", new string[] { "iha" }, defaultKeyBind: "NumPad5");
            AddSound("menino", "Eh o menino de papai eh?", new string[] { "pai", "papai", "me", "eho" }, defaultKeyBind: "NumPad1");
            AddSound("mola", "*Barulho de Mola*", defaultKeyBind: "NumPad1");
            AddSound("nao", "Nao.", new string[] { "n" }, defaultKeyBind: "NumPad6");
            AddSound("okok", "Okay Okay.", new string[] { "ok", "okay" }, defaultKeyBind: "NumPad2");
            AddSound("papelao", "Que papelao hein.", new string[] { "pa", "papel", "papelão", "que" }, defaultKeyBind: "NumPad3");
            AddSound("pare", "PARE!", defaultKeyBind: "NumPad7");
            AddSound("patrao", "Esse eh meu patrao kkkk.", new string[] { "patrão", "esse" }, defaultKeyBind: "NumPad8");
            AddSound("peido", "*Peido*", new string[] { "peidar", "fart", "pe" }, defaultKeyBind: "NumPad8");
            AddSound("potencia", "AOOOOOOOO POTENCIA!", new string[] { "ao" }, defaultKeyBind: "NumPad9");
            AddSound("rapaz", "[i:4375] Rapaaaaz.", new string[] { "rapa" }, defaultKeyBind: "NumPad4");
            AddSound("ratinho", "[i:4375] RATINHO NHO NHO!", new string[] { "ra" }, defaultKeyBind: "NumPad5");

            RandomSoundKeybind = KeybindLoader.RegisterKeybind(this, "Tocar \"som\"", "NumPad0");

            AddSound("semgraca", "Maque cara mais sem grasa.", new string[] { "sem", "graça", "semgraça" }, defaultKeyBind: "NumPad6");
            AddSound("tambor", "*Barulho de tambor*", new string[] { "tum" }, defaultKeyBind: "NumPad4");
            AddSound("tapa", "*Tapa*", new string[] { "pshh", "psh" }, defaultKeyBind: "NumPad5");
            AddSound("tomi", "Tomi!", new string[] { "tome", "toma" }, defaultKeyBind: "NumPad6");
            AddSound("tuim", "*Tuim*", defaultKeyBind: "NumPad1");
            AddSound("uepa", "Uepa!", new string[] { "epa", "eita" }, defaultKeyBind: "NumPad2");
            AddSound("uh", "Uh!", defaultKeyBind: "NumPad3");
            AddSound("ui", "UUUUUUIII!", defaultKeyBind: "NumPad7");
            AddSound("vixi", "Vixi!", new string[] { "vish", "vi" }, defaultKeyBind: "NumPad7");
            AddSound("vaodanca", "Vao dansa?", new string[] { "vamo", "vao", "vão", "vaodança", "vaodansa" }, defaultKeyBind: "NumPad8");
            AddSound("wah", "*Wah wah wah*", new string[] { "ua", "wa" }, defaultKeyBind: "NumPad9");
            AddSound("xi", "Xiiiiiiii!", new string[] { "chi" }, defaultKeyBind: "NumPad4");

            Terraria.Chat.On_ChatHelper.DisplayMessage += ChatHelper_DisplayMessage;

            Terraria.On_Player.QuickMount += On_Player_QuickMount;
        }

        private void On_Player_QuickMount(On_Player.orig_QuickMount orig, Player self)
        {
            orig.Invoke(self);
            // The code below will only run in the clients.

            if (self.whoAmI != Main.LocalPlayer.whoAmI) return;
            if (!IsHorseMount(self.mount.Type)) return;
            
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                PlaySound(Sounds["cavalo"], self.whoAmI, soundType: SoundTypes.Horse, playAtPlayerLocation: true);
                return;
            }

            // Send it to the server.
            SendSoundMessage(self, Sounds["cavalo"], false, SoundTypes.Horse, true);

        }

        public bool IsHorseMount(int mountId)
        {
            var validMountIds = new[]
            {
                MountID.PaintedHorse,
                MountID.MajesticHorse,
                MountID.DarkHorse,
                MountID.Unicorn,
                MountID.WallOfFleshGoat,
                MountID.Rudolph
            };

            return validMountIds.Contains(mountId);
        }

        public bool IsBossMessage(string text)
        {
            return text == Language.GetTextValue("LegacyMisc.28") ||
                   text == Language.GetTextValue("LegacyMisc.29") ||
                   text == Language.GetTextValue("LegacyMisc.30");
        }

        private void ChatHelper_DisplayMessage(Terraria.Chat.On_ChatHelper.orig_DisplayMessage orig, NetworkText text, Color color, byte messageAuthor)
        {
            orig.Invoke(text, color, messageAuthor);

            var clientConfig = ModContent.GetInstance<FaroRatinhoSFXConfig>();
            var serverConfig = ModContent.GetInstance<FaroRatinhoSFXServerConfig>();

            if (
                Main.LocalPlayer.whoAmI == 0 &&
                messageAuthor == 255 &&
                IsBossMessage(text.ToString()) &&
                serverConfig.playSoundOnBossMessage.enabled
                )
            {
                var soundList = serverConfig.playSoundOnBossMessage.useAnySound ? Sounds.Keys.ToList() : serverConfig.playSoundOnBossMessage.sounds;
                soundList.RemoveAll(x => !SoundsNamesAndAliases.ContainsKey(x));

                if (soundList.Count > 0 )
                {
                    var sfx = GetRandomSFXFromList(soundList);
                    SendSoundMessage(Main.LocalPlayer, sfx, false);
                }

            }

            var lowerText = text.ToString().ToLower();

            MatchCollection matches = rx.Matches(lowerText);

            int count = 0;

            if (matches.Count >= 0)
            {
                List<string> preventRepeat = new();

                foreach (Match match in matches)
                {
                    if (match.Index != 0 && !clientConfig.CommandAnywhere) return;

                    GroupCollection groups = match.Groups;
                    var g = groups[1].Value;
                    bool isRandomSound = (g == "som" || g == "sound" || g == "aleatorio" || g == "r");
                    if (SoundsNamesAndAliases.ContainsKey(g) || isRandomSound)
                    {
                        var soundName = "";
                        FaroRatinhoSound sfx = null;
                        if (isRandomSound)
                        {
                            sfx = GetRandomSFX();
                            soundName = sfx.name;
                        } else
                        {
                            soundName = SoundsNamesAndAliases[g];
                            sfx = Sounds[soundName];
                        }

                        if (preventRepeat.Contains(soundName)) continue;

                        preventRepeat.Add(soundName);

                        var player = Main.player[messageAuthor];
                        var message = $"/{g} - {sfx.description}";

                        if (matches.Count == 1 && match.Index == 0)
                        {
                            message = "";
                        }

                        PlaySound(sfx, messageAuthor, message, player.team);
                        count++;

                        if (count >= serverConfig.MaxSounds) break;
                    }

                }
            }
        }

        public void AddSound(string name, string description, string[] aliases = null, string defaultKeyBind = "" )
        {
            aliases ??= new string[] { };
            Sounds.Add(name, new FaroRatinhoSound(mod: this, name: name, description: description, aliases: aliases, defaultKeyBind: defaultKeyBind));
        }

        public FaroRatinhoSound GetRandomSFX(bool withIgnoreClientList = true, bool withIgnoreServerList = true)
        {
            return GetRandomSFXFromList(Sounds.Keys.ToList(), withIgnoreClientList, withIgnoreServerList);
        }

        public FaroRatinhoSound GetRandomSFXFromList(List<string> soundList, bool withIgnoreClientList = true, bool withIgnoreServerList = true)
        {
            var clientConfig = ModContent.GetInstance<FaroRatinhoSFXConfig>();
            var serverConfig = ModContent.GetInstance<FaroRatinhoSFXServerConfig>();

            List<string> ignoredSounds = new();

            if (withIgnoreClientList)
            {
                foreach (var item in clientConfig.ignoredSounds)
                {
                    var search = item.ToLower();
                    if (!SoundsNamesAndAliases.ContainsKey(search)) continue;
                    ignoredSounds.Add(SoundsNamesAndAliases[search]);
                }
            }

            if (withIgnoreServerList)
            {
                foreach (var item in serverConfig.ignoredSounds)
                {
                    var search = item.ToLower();
                    if (!SoundsNamesAndAliases.ContainsKey(search)) continue;
                    ignoredSounds.Add(SoundsNamesAndAliases[search]);
                }
            }


            List<string> keyList = new(soundList);
            // Removing the ones that are in the ignoredList.
            keyList.RemoveAll(ignoredSounds.Contains);

            if (keyList.Count == 0) return Sounds[soundList.First()];

            // Pick one random of the list.
            Random rand = new Random();
            string randomKey = keyList[rand.Next(keyList.Count)];

            // Return the sound.
            return Sounds[SoundsNamesAndAliases[randomKey]];
        }

        public void SendSoundMessage(Player player, FaroRatinhoSound sfx, bool withMessage = true, SoundTypes soundType = SoundTypes.Normal, bool playAtPlayerLocation = false)
        {

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                PlaySound(sfx, player.whoAmI, withMessage ? sfx.description : "", 0, soundType);
                return;
            }

            ModPacket packet = GetPacket();
            packet.Write(sfx.name);
            packet.Write(withMessage ? $"{player.name}: {sfx.description}" : "");
            packet.Write(player.team);
            packet.Write(player.whoAmI);
            packet.Write((int)soundType);
            packet.Write(playAtPlayerLocation);

            packet.Send();
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {

            string soundName = reader.ReadString();
            string message = reader.ReadString();
            int team = reader.ReadInt32();
            int who = reader.ReadInt32();
            SoundTypes soundType = (SoundTypes)reader.ReadInt32();
            bool playAtLocation = reader.ReadBoolean();

            if (Main.dedServ)
            {
                // Repass message to the clients.
                ModPacket packet = GetPacket();
                packet.Write(soundName);
                packet.Write(message);
                packet.Write(team);
                packet.Write(who);
                packet.Write((int)soundType);
                packet.Write(playAtLocation);

                packet.Send();
                return;
            }

            var sfx = Sounds[soundName];
            PlaySound(sfx, who, message, team, soundType, playAtLocation);
        }

        public void PlaySound(FaroRatinhoSound sfx, int fromWho, string message = "", int team = 0, SoundTypes soundType = SoundTypes.Normal, bool playAtPlayerLocation = false)
        {
            var sound = sfx.sound;
            var config = ModContent.GetInstance<FaroRatinhoSFXConfig>();
            var serverConfig = ModContent.GetInstance<FaroRatinhoSFXServerConfig>();

            if (!config.playSoundOnHorseMount && soundType == SoundTypes.Horse) return; 

            if (!config.deathSounds.enabled && soundType == SoundTypes.Death) return;

            if (serverConfig.DisableCommands || config.DisableCommands)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient && Main.LocalPlayer.whoAmI == fromWho)
                {
                    Main.NewText($"Sons de efeito foi desabilitado nas opcoes{ (serverConfig.DisableCommands ? " do servidor" : "") }.", new Color(255f, 35f, 0f));
                }
                return;
            }

            var ignored = CheckIfSoundIsOnList(sfx, config.ignoredSounds);
            var ignoredServer = CheckIfSoundIsOnList(sfx, serverConfig.ignoredSounds);

            if (ignored || ignoredServer)
            {
                if (ignoredServer && Main.netMode == NetmodeID.MultiplayerClient && Main.LocalPlayer.whoAmI == fromWho)
                {
                    Main.NewText($"O dono do servidor desabilitou esse som.", new Color(255f, 35f, 0f));
                }
                return;
            }

            var messageColor = new Color(config.MessageColor.R, config.MessageColor.G, config.MessageColor.B);
            var volume = config.Volume / 2f;
            var showMessage = !(config.DisableChatMessage || serverConfig.DisableChatMessage);

            if (config.MessageColorAsTeam && team > 0)
            {
                messageColor = new Color(Main.teamColor[team].R, Main.teamColor[team].G, Main.teamColor[team].B);
            }

            if (!Main.dedServ)
            {
                Vector2? pos = null;
                if (playAtPlayerLocation)
                {
                    var plr = Main.player[fromWho];
                    pos = plr.position;
                }
                SoundEngine.PlaySound(sound with { Volume = volume }, pos);
            }

            if (showMessage && !string.IsNullOrEmpty(message))
            {
                Main.NewText(message, messageColor);

                Console.WriteLine(message);
            }

        }

        public bool CheckIfSoundIsOnList(FaroRatinhoSound sfx, List<string> list)
        {
            foreach (var ignoredName in list)
            {
                var searchName = ignoredName.ToLower();
                if (!SoundsNamesAndAliases.ContainsKey(searchName)) continue;
                var name = SoundsNamesAndAliases[searchName];

                if (sfx.name == name) return true;
            }

            return false;
        }

        public static bool IsPlayerLocalServerOwner(Player player)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return Netplay.Connection.Socket.GetRemoteAddress().IsLocalHost();
            }

            for (int plr = 0; plr < Main.maxPlayers; plr++)
                if (Netplay.Clients[plr].State == 10 && Main.player[plr] == player && Netplay.Clients[plr].Socket.GetRemoteAddress().IsLocalHost())
                    return true;
            return false;
        }

    }

    public class FaroRatinhoSound
    {
        public FaroRatinhoSFX mod;
        public string name;
        public string description;
        public string soundPath;
        public List<string> aliases;
        public SoundStyle sound;
        public string defaultKeyBind;
        public ModKeybind keybind;

        public FaroRatinhoSound(FaroRatinhoSFX mod, string name, string description, string soundPath = "", string defaultKeyBind = "", string[] aliases = null)
        {
            this.mod = mod;
            this.name = name;
            this.description = description;
            this.soundPath = string.IsNullOrEmpty(soundPath) ? $"{nameof(FaroRatinhoSFX)}/Assets/Sounds/{name}" : soundPath;
            this.sound = new SoundStyle(this.soundPath);
            this.defaultKeyBind = defaultKeyBind;

            aliases ??= new string[] { };
            this.aliases = aliases.ToList();

            if (mod.SoundsNamesAndAliases.ContainsKey(this.name))
            {
                Console.WriteLine($"WARNING: Duplicated '{this.name}' sound name.");
            } else
            {
                mod.SoundsNamesAndAliases.Add(this.name, this.name);
            }

            foreach (var aliase in this.aliases)
            {
                if (mod.SoundsNamesAndAliases.ContainsKey(aliase))
                {
                    Console.WriteLine($"WARNING: Duplicated '{aliase}' sound aliase.");
                }
                else
                {
                    mod.SoundsNamesAndAliases.Add(aliase, this.name);
                }
            }

            if (defaultKeyBind != "")
            {
                keybind = KeybindLoader.RegisterKeybind(mod, $"Tocar \"{this.name}\"", defaultKeyBind);
            }

        }
    }
}