using Terraria;
using System.ComponentModel;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;

namespace FaroRatinhoSFX
{
	public class FaroRatinhoSFXConfig : ModConfig
	{

		[Label("Desabilitar o mod.")]
		[Tooltip("Nada tocara pra voce e nada vai aparecer no chat.")]
		[DefaultValue(false)]
		public bool DisableCommands;

		[Label("Volume")]
		[Range(0.0f, 1.0f)]
		[Tooltip("0 = nada.\n1 = normal.")]
		[DefaultValue(1.0f)]
		public float Volume;

		[Label("Cor em que aparece no chat.")]
		[DefaultValue(typeof(Color), "255, 255, 0, 255")]
		public Color MessageColor;

		[Label("Usar cor do time no chat.")]
		[DefaultValue(true)]
		public bool MessageColorAsTeam;

		[Label("Desabilitar texto no chat.")]
		[DefaultValue(false)]
		public bool DisableChatMessage;

		[Label("Tocar som se o comando estiver em qualquer lugar da mensagem.")]
		[DefaultValue(true)]
		public bool CommandAnywhere;

		public override ConfigScope Mode => ConfigScope.ClientSide;


	}

	class FaroRatinhoSFXServerConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Header("[c/00FF00:Opsoes de Servidor]")]

		[Label("Desabilitar o mod. (Vai desabilitar pra todo mundo)")]
		[DefaultValue(false)]
		public bool DisableCommands;

		[Label("Desabilitar texto no chat. (Vai desabilitar pra todo mundo)")]
		[DefaultValue(false)]
		public bool DisableChatMessage;

		[Label("Maximo de som ao mesmo tempo na mesma mensagem")]
		[Range(1, 6)]
		[DefaultValue(3)]
		public int MaxSounds;

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