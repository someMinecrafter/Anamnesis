// © Anamnesis.
// Licensed under the MIT license.

namespace Anamnesis.Memory
{
	using System;

	public class ActorModelMemory : MemoryBase
	{
		/// <summary>
		/// Known data paths.
		/// </summary>
		public enum DataPaths : short
		{
			MidlanderMale = 101,
			MidlanderMaleChild = 104,
			MidlanderFemale = 201,
			MidlanderFemaleChild = 204,
			HighlanderMale = 301,
			HighlanderFemale = 401,
			ElezenMale = 501,
			ElezenMaleChild = 504,
			ElezenFemale = 601,
			ElezenFemaleChild = 604,
			MiqoteMale = 701,
			MiqoteMaleChild = 704,
			MiqoteFemale = 801,
			MiqoteFemaleChild = 804,
			RoegadynMale = 901,
			RoegadynFemale = 1001,
			LalafellMale = 1101,
			LalafellFemale = 1201,
			AuRaMale = 1301,
			AuRaFemale = 1401,
			HrothgarMale = 1501,
			////HrothgarFemale = 1601,
			VieraMale = 1701,
			VieraFemale = 1801,
			PadjalMale = 9104,
			PadjalFemale = 9204,
		}

		[Bind(0x030, BindFlags.Pointer)] public ExtendedWeaponMemory? Weapons { get; set; }
		[Bind(0x050)] public TransformMemory? Transform { get; set; }
		[Bind(0x0A0, BindFlags.Pointer | BindFlags.OnlyInGPose)] public SkeletonMemory? Skeleton { get; set; }
		[Bind(0x148, BindFlags.Pointer)] public BustMemory? Bust { get; set; }
		[Bind(0x240, 0x040, 0x020, BindFlags.Pointer)] public ExtendedAppearanceMemory? ExtendedAppearance { get; set; }
		[Bind(0x26C)] public float Height { get; set; }
		[Bind(0x2B0)] public float Wetness { get; set; }
		[Bind(0x2BC)] public float Drenched { get; set; }
		[Bind(0x938)] public short DataPath { get; set; }
		[Bind(0x93C)] public byte DataHead { get; set; }

		public bool LockWetness
		{
			get => this.IsFrozen(nameof(ActorModelMemory.Wetness));
			set => this.SetFrozen(nameof(ActorModelMemory.Wetness), value);
		}

		public bool ForceDrenched
		{
			get => this.IsFrozen(nameof(ActorModelMemory.Drenched));
			set => this.SetFrozen(nameof(ActorModelMemory.Drenched), value, value ? 5 : 0);
		}

		public bool IsPlayer
		{
			get
			{
				if (!Enum.IsDefined(typeof(ActorModelMemory.DataPaths), this.DataPath))
					return false;

				if (this.Parent is ActorMemory actor)
				{
					return actor.ModelType == 0;
				}

				return true;
			}
		}

		protected override bool CanRead(BindInfo bind)
		{
			if (bind.Name == nameof(ActorModelMemory.ExtendedAppearance))
			{
				// No extended appearance for anything that isn't a player!
				if (!this.IsPlayer)
				{
					if (this.ExtendedAppearance != null)
					{
						this.ExtendedAppearance.Dispose();
						this.ExtendedAppearance = null;
					}

					return false;
				}
			}

			return base.CanRead(bind);
		}
	}
}