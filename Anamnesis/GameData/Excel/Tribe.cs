﻿// © Anamnesis.
// Licensed under the MIT license.

namespace Anamnesis.GameData.Excel
{
	using Anamnesis.Memory;
	using Lumina.Data;
	using Lumina.Excel;
	using Lumina.Text;

	using ExcelRow = Anamnesis.GameData.Sheets.ExcelRow;

	[Sheet("Tribe", 0xe74759fb)]
	public class Tribe : ExcelRow
	{
		public string Name => this.CustomizeTribe.ToString();
		public ActorCustomizeMemory.Tribes CustomizeTribe => (ActorCustomizeMemory.Tribes)this.RowId;

		public string Female { get; private set; } = string.Empty;
		public string Male { get; private set; } = string.Empty;

		public string DisplayName
		{
			get
			{
				// big old hack to keep miqo tribe names short for the UI
				if (this.Female.StartsWith("Seeker"))
					return "Seeker";

				if (this.Female.StartsWith("Keeper"))
					return "Keeper";

				return this.Female;
			}
		}

		public bool Equals(Tribe? other)
		{
			if (other is null)
				return false;

			return this.CustomizeTribe == other.CustomizeTribe;
		}

		public override void PopulateData(RowParser parser, Lumina.GameData gameData, Language language)
		{
			base.PopulateData(parser, gameData, language);

			this.Male = parser.ReadColumn<SeString>(0) ?? string.Empty;
			this.Female = parser.ReadColumn<SeString>(1) ?? string.Empty;
		}
	}
}
