using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Chat;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FaroRatinhoSFX
{
	public class SomCommand : ModCommand
	{
		public override CommandType Type => CommandType.World;

		public override string Command => "som";

		public override string Description => "Toca um som aleatorio.";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			List<string> keyList = new(FaroRatinhoSFX.Instance.Sounds.Keys);
			Random rand = new Random();
			string randomKey = keyList[rand.Next(keyList.Count)];

			var sfx = FaroRatinhoSFX.Instance.Sounds[randomKey];
            FaroRatinhoSFX.Instance.SendSoundMessage(caller.Player, sfx);

		}

	}

	public class TomiCommand : ModCommand
	{
		public override CommandType Type => CommandType.World;

		public override string Command => "tomi";

        public override string Description => "\"Tomi!\"";

        public override void Action(CommandCaller caller, string input, string[] args)
		{
			var sfx = FaroRatinhoSFX.Instance.Sounds[Command];
			FaroRatinhoSFX.Instance.SendSoundMessage(caller.Player, sfx);

		}

	}

	public class AhrurCommand : TomiCommand
	{
		public override string Command => "ahrur";

		public override string Description => "\"AHRUR AH BHRUAH BHRUU AH BHRUUUU AH\"";
	}

	public class AiCommand : TomiCommand
	{
		public override string Command => "ai";

		public override string Description => "\"Ai.\"";
	}

	public class AiAiAiCommand : TomiCommand
	{
		public override string Command => "aiaiai";

		public override string Description => "\"AAAAAAAAI AI AAAAAAAAI.\"";
	}

	public class AtumalacaCommand : TomiCommand
	{
		public override string Command => "atumalaca";

		public override string Description => "\"ATUMALACA KKKKKKKKKKKK.\"";
	}

	public class BrasilCommand : TomiCommand
	{
		public override string Command => "brasil";

		public override string Description => "\"BRASIL SIL SIL!.\"";
	}

	public class BrincadeiraCommand : TomiCommand
	{
		public override string Command => "brincadeira";

		public override string Description => "\"Eh brincadeira hein.\"";
	}

	public class CalmaCommand : TomiCommand
	{
		public override string Command => "calma";
		public override string Description => "\"Que isso meu filho calma.\"";
	}

	public class CavaloCommand : TomiCommand
	{
		public override string Command => "cavalo";
		public override string Description => "\"Cavalo.\"";
	}

	public class ChegaCommand : TomiCommand
	{
		public override string Command => "chega";
		public override string Description => "\"CHEEEEGA!\"";
	}

	public class DancaCommand : TomiCommand
	{
		public override string Command => "danca";
		public override string Description => "\"Dansa gatinho dansa.\"";
	}

	public class DemaisCommand : TomiCommand
	{
		public override string Command => "demais";
		public override string Description => "\"Demais.\"";
	}

	public class EleGostaCommand : TomiCommand
	{
		public override string Command => "elegosta";
		public override string Description => "\"Ele gosta.\"";
	}

	public class GalinhaCommand : TomiCommand
	{
		public override string Command => "galinha";
		public override string Description => "*Som de Galinha*";
	}

	public class IrraCommand : TomiCommand
	{
		public override string Command => "irra";
		public override string Description => "\"IIIIIIIIRRRRRRRRAAAAAAAAAAAA!!!\"";
	}

	public class NaoCommand : TomiCommand
	{
		public override string Command => "nao";

		public override string Description => "\"Nao.\"";
	}

	public class NSeiCommand : TomiCommand
	{
		public override string Command => "nsei";

		public override string Description => "*Nao sei que nome dar pra isso, me sugere no workshop kk*";
	}

	public class OkOkCommand : TomiCommand
	{
		public override string Command => "okok";

		public override string Description => "\"Okay Okay.\"";
	}

	public class PapelaoCommand : TomiCommand
	{
		public override string Command => "papelao";

		public override string Description => "\"Que papelao hein.\"";
	}

	public class PareCommand : TomiCommand
	{
		public override string Command => "pare";

		public override string Description => "\"PARE!\"";
	}

	public class PatraoCommand : TomiCommand
	{
		public override string Command => "patrao";

		public override string Description => "\"Esse eh meu patrao kkkk.\"";
	}

	public class PotenciaCommand : TomiCommand
	{
		public override string Command => "potencia";

		public override string Description => "\"AOOOOOOOO POTENCIA!\"";
	}

	public class RapazCommand : TomiCommand
	{
		public override string Command => "rapaz";

		public override string Description => "\"Rapaaaaz.\"";
	}

	public class RatinhoCommand : TomiCommand
	{
		public override string Command => "ratinho";

		public override string Description => "\"RATINHO NHO NHO!\"";
	}

	public class SemGracaCommand : TomiCommand
	{
		public override string Command => "semgraca";

		public override string Description => "\"Maque cara mais sem grasa.\"";
	}

	public class TamborCommand : TomiCommand
	{
		public override string Command => "tambor";

		public override string Description => "*Barulho de tambor*";
	}

	public class TapaCommand : TomiCommand
	{
		public override string Command => "tapa";

		public override string Description => "*Tapa*";
	}

	public class TuimCommand : TomiCommand
	{
		public override string Command => "tuim";

		public override string Description => "*Tuim*";
	}

	public class UepaCommand : TomiCommand
	{
		public override string Command => "uepa";

		public override string Description => "\"Uepa!\"";
	}

	public class UhCommand : TomiCommand
	{
		public override string Command => "uh";

		public override string Description => "\"Uh!\"";
	}

	public class UiCommand : TomiCommand
	{
		public override string Command => "ui";

		public override string Description => "\"UUUUUUIII!\"";
	}

	public class VaodancaCommand : TomiCommand
	{
		public override string Command => "vaodanca";

		public override string Description => "\"Vao dansa?\"";
	}

	public class WahCommand : TomiCommand
	{
		public override string Command => "wah";

		public override string Description => "*Wah wah wah*";
	}

	public class XiCommand : TomiCommand
	{
		public override string Command => "xi";

		public override string Description => "\"Xiiiiiiii!\"";
	}


}