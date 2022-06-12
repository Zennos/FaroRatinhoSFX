using Terraria;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
		[Tooltip("0 = 0%.\n1 = 100%.")]
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

		[Label("Nao tocar esses sons:")]
		[Tooltip("Se um player mandar um desses nao vai tocar, nem com /som.\nDigite na lista o nome do comando de som.")]
		public List<string> ignoredSounds = new();

		public override ConfigScope Mode => ConfigScope.ClientSide;

    }

	class FaroRatinhoSFXServerConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Header("[c/00FF00:Opsoes de Servidor]: As opcoes abaixo sera aplicada pra todo mundo.")]

		[Label("Desabilitar o mod.")]
		[DefaultValue(false)]
		public bool DisableCommands;

		[Label("Desabilitar texto no chat.")]
		[DefaultValue(false)]
		public bool DisableChatMessage;

		[Label("Maximo de som ao mesmo tempo na mesma mensagem.")]
		[Range(1, 6)]
		[DefaultValue(3)]
		public int MaxSounds;

		[Label("Nao tocar esses sons:")]
		[Tooltip("Se um player mandar um desses nao vai tocar, nem com /som.\nDigite na lista o nome do comando de som.")]
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