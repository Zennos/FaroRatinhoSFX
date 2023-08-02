using Terraria;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;
using System.Diagnostics;
using System;

namespace FaroRatinhoSFX
{

	public class FaroRatinhoSFXConfig : ModConfig
	{
		[DefaultValue(false)]
		public bool DisableCommands;

		[LabelArgs("Volume")]
		[Range(0.0f, 2.0f)]
		[DefaultValue(1.0f)]
		public float Volume;

		[DefaultValue(typeof(Color), "255, 255, 0, 255")]
		public Color MessageColor;

		[DefaultValue(true)]
		public bool MessageColorAsTeam;

		[DefaultValue(false)]
		public bool DisableChatMessage;

		[DefaultValue(true)]
		public bool CommandAnywhere;

		public bool playSoundOnHorseMount = false;

		public List<string> ignoredSounds = new();

        public DeathSoundsClientData deathSounds = new DeathSoundsClientData();

        public override ConfigScope Mode => ConfigScope.ClientSide;

    }

	class FaroRatinhoSFXServerConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Note")]

		[DefaultValue(false)]
		public bool DisableCommands;

		[DefaultValue(false)]
		public bool DisableChatMessage;

		[Range(1, 6)]
		[DefaultValue(3)]
		public int MaxSounds;

		public List<string> ignoredSounds = new();

        public DeathSoundsServerData deathSounds = new DeathSoundsServerData();

        public BossMessageData playSoundOnBossMessage = new BossMessageData();

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
            if (!FaroRatinhoSFX.IsPlayerLocalServerOwner(Main.player[whoAmI]))
			{
				message = "Apenas o dono do servidor pode mudar isso, lamento :v";
				return false;
			}
			return base.AcceptClientChanges(pendingConfig, whoAmI, ref message);
		}
	}

	public class SoundListData
	{
        public bool useAnySound = false;

        public List<string> sounds = new();

        public SoundListData()
        {
            sounds = new List<string>() { "peido", "boom", "ai", "uh", "atumalaca", "wah" };
        }
    }

    public class BossMessageData : SoundListData
    {
        public bool enabled = false;

        public BossMessageData()
        {
            sounds = new List<string>() { "xi", "boom", "corre", "galinha", "vixi", "ui", "rapaz", "vaodanca"};
        }
    }

    public class DeathSoundsClientData : SoundListData
    {
        public bool enabled = false;

        public override string ToString()
        {
            return enabled ?
                Language.GetTextValue("GameUI.Enabled") :
                Language.GetTextValue("GameUI.Disabled");
        }

        public override bool Equals(object obj)
        {
            if (obj is DeathSoundsClientData other)
                return enabled == other.enabled && useAnySound == other.useAnySound && sounds.SequenceEqual(other.sounds);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return new { enabled, sounds, useAnySound }.GetHashCode();
        }
    }

    public class DeathSoundsServerData : SoundListData
    {
		[Header("DeathSoundsServerNotice")]

        public bool enabled = false;

        public bool playAtLocationOfDeath = false;

        public override bool Equals(object obj)
        {
            if (obj is DeathSoundsServerData other)
                return enabled == other.enabled && useAnySound == other.useAnySound && playAtLocationOfDeath == other.playAtLocationOfDeath && sounds.SequenceEqual(other.sounds);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return new { enabled, sounds, useAnySound, playAtLocationOfDeath }.GetHashCode();
        }
    }
}