using Terraria;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FaroRatinhoSFX
{

	public class FaroRatinhoSFXConfig : ModConfig
	{
		[DefaultValue(false)]
		public bool DisableCommands;

		[LabelArgs("Volume")]
		[Range(0.0f, 1.0f)]
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

		public List<string> ignoredSounds = new();

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
}