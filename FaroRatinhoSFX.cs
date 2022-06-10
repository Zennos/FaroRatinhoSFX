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

        public override void Load()
        {

            Instance = this;

            AddSound("ahrur", "AHRUR AH BHRUAH BHRUU AH BHRUUUU AH", new string[]{ "ah" }, defaultKeyBind: "NumPad7");
            AddSound("ai", "Ai.", defaultKeyBind: "NumPad8");
            AddSound("aiaiai", "AAAAAAAAI AI AAAAAAAAI.", new string[] { "aiai" }, defaultKeyBind: "NumPad9");
            AddSound("atumalaca", "ATUMALACA KKKKKKKKKKKK.", new string[] { "haha", "risada", "kkk" }, defaultKeyBind: "NumPad4");
            AddSound("brasil", "BRASIL SIL SIL!.", defaultKeyBind: "NumPad5");
            AddSound("brincadeira", "Eh brincadeira hein.", defaultKeyBind: "NumPad6");
            AddSound("calma", "Que isso meu filho calma.", new string[] { "filho" }, defaultKeyBind: "NumPad1");
            AddSound("cavalo", "Cavalo.", defaultKeyBind: "NumPad2");
            AddSound("chega", "CHEEEEGA!", new string[] { "xega" }, defaultKeyBind: "NumPad3");
            AddSound("danca", "Dansa gatinho dansa.", new string[] { "dansa", "gatinho", "dança" }, defaultKeyBind: "NumPad7");
            AddSound("demais", "Demais.", new string[] { "d+", "dmais" }, defaultKeyBind: "NumPad8");
            AddSound("elegosta", "Ele gosta.", new string[] { "gosta", "ele" }, defaultKeyBind: "NumPad9");
            AddSound("galinha", "*[i:4016] Som de Galinha*", new string[] { "ga" }, defaultKeyBind: "NumPad4");
            AddSound("irra", "IIIIIIIIRRRRRRRRAAAAAAAAAAAA!!!", new string[] { "iha" }, defaultKeyBind: "NumPad5");
            AddSound("nao", "Nao.", new string[] { "n" }, defaultKeyBind: "NumPad6");
            AddSound("nsei", "*Nao sei que nome dar pra isso, me sugere no workshop kk*", defaultKeyBind: "NumPad1");
            AddSound("okok", "Okay Okay.", new string[] { "ok", "okay" }, defaultKeyBind: "NumPad2");
            AddSound("papelao", "Que papelao hein.", new string[] { "pa", "papel", "papelão", "que" }, defaultKeyBind: "NumPad3");
            AddSound("pare", "PARE!", defaultKeyBind: "NumPad7");
            AddSound("patrao", "Esse eh meu patrao kkkk.", new string[] { "patrão", "esse" }, defaultKeyBind: "NumPad8");
            AddSound("potencia", "AOOOOOOOO POTENCIA!", new string[] { "ao" }, defaultKeyBind: "NumPad9");
            AddSound("rapaz", "[i:4375] Rapaaaaz.", new string[] { "rapa" }, defaultKeyBind: "NumPad4");
            AddSound("ratinho", "[i:4375] RATINHO NHO NHO!", new string[] { "ra" }, defaultKeyBind: "NumPad5");
            AddSound("semgraca", "Maque cara mais sem grasa.", new string[] { "sem", "graça", "semgraça" }, defaultKeyBind: "NumPad6");
            AddSound("tambor", "*Barulho de tambor*", new string[] { "tum" }, defaultKeyBind: "NumPad4");
            AddSound("tapa", "*Tapa*", new string[] { "pshh", "psh" }, defaultKeyBind: "NumPad5");
            AddSound("tomi", "Tomi!", new string[] { "tome", "toma" }, defaultKeyBind: "NumPad6");
            AddSound("tuim", "*Tuim*", defaultKeyBind: "NumPad1");
            AddSound("uepa", "Uepa!", new string[] { "epa", "eita" }, defaultKeyBind: "NumPad2");
            AddSound("uh", "Uh!", defaultKeyBind: "NumPad3");
            AddSound("ui", "UUUUUUIII!", defaultKeyBind: "NumPad7");
            AddSound("vaodanca", "Vao dansa?", new string[] { "vamo", "vao", "vão" }, defaultKeyBind: "NumPad8");
            AddSound("wah", "*Wah wah wah*", new string[] { "ua", "wa" }, defaultKeyBind: "NumPad9");
            AddSound("xi", "Xiiiiiiii!", new string[] { "chi" }, defaultKeyBind: "NumPad4");

            On.Terraria.Chat.ChatHelper.DisplayMessage += ChatHelper_DisplayMessage;
        }

        private void ChatHelper_DisplayMessage(On.Terraria.Chat.ChatHelper.orig_DisplayMessage orig, NetworkText text, Color color, byte messageAuthor)
        {
            orig.Invoke(text, color, messageAuthor);

            var lowerText = text.ToString().ToLower();

            MatchCollection matches = rx.Matches(lowerText);

            int count = 0;

            if (matches.Count >= 0)
            {
                List<string> preventRepeat = new();

                foreach (Match match in matches)
                {
                    if (match.Index != 0 && !ModContent.GetInstance<FaroRatinhoSFXConfig>().CommandAnywhere) return;

                    GroupCollection groups = match.Groups;
                    var g = groups[1].Value;
                    if (SoundsNamesAndAliases.ContainsKey(g))
                    {
                        var soundName = SoundsNamesAndAliases[g];
                        var sfx = Sounds[soundName];

                        if (preventRepeat.Contains(soundName)) continue;

                        preventRepeat.Add(soundName);

                        var player = Main.player[messageAuthor];
                        var message = $"/{g} - {sfx.description}";

                        if (matches.Count == 1 && match.Index == 0)
                        {
                            message = "";
                        }

                        PlaySound(sfx.sound, messageAuthor, message, player.team);
                        count++;

                        if (count >= ModContent.GetInstance<FaroRatinhoSFXServerConfig>().MaxSounds) break;
                    }

                }
            }

        }

        public void AddSound(string name, string description, string[] aliases = null, string defaultKeyBind = "" )
        {
            aliases ??= new string[] { };
            Sounds.Add(name, new FaroRatinhoSound(mod: this, name: name, description: description, aliases: aliases, defaultKeyBind: defaultKeyBind));
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {

			string soundName = reader.ReadString();
            string message = reader.ReadString();
            int team = reader.ReadInt32();
            int who = reader.ReadInt32();

            var sfx = Sounds[soundName];

			PlaySound(sfx.sound, who, message, team);
			
        }

        public void SendMessage(string msg)
        {
            var packet = GetPacket();
            packet.Write(msg);
            packet.Send();
        }

        public void SendSoundMessage(Player player, FaroRatinhoSound sfx)
        {

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                PlaySound(sfx.sound, player.whoAmI, sfx.description);
                return;
            }

            ModPacket packet = GetPacket();
            packet.Write(sfx.name);
            packet.Write($"{player.name}: {sfx.description}");
            packet.Write(player.team);
            packet.Write(player.whoAmI);
            packet.Send();
        }

        public void PlaySound(SoundStyle sound, int fromWho, string message = "", int team = 0)
        {

            var config = ModContent.GetInstance<FaroRatinhoSFXConfig>();
            var serverConfig = ModContent.GetInstance<FaroRatinhoSFXServerConfig>();


            if (serverConfig.DisableCommands || config.DisableCommands)
            {
                if (Main.LocalPlayer.whoAmI == fromWho)
                {
                    Main.NewText($"Nao foi possivel tocar o audio ja que foi desabilitado nas opcoes{ (serverConfig.DisableCommands ? " do servidor" : "") }.");
                }
                return;
            }

            var messageColor = new Color(config.MessageColor.R, config.MessageColor.G, config.MessageColor.B);
            var volume = config.Volume;
            var showMessage = !(config.DisableChatMessage || serverConfig.DisableChatMessage);

            if (config.MessageColorAsTeam && team > 0)
            {
                messageColor = new Color(Main.teamColor[team].R, Main.teamColor[team].G, Main.teamColor[team].B);
            }

            if (!Main.dedServ)
            {
                SoundEngine.PlaySound(sound with { Volume = volume });
            }

            if (showMessage && !string.IsNullOrEmpty(message))
            {
                Main.NewText(message, messageColor);

                Console.WriteLine(message);
            }

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