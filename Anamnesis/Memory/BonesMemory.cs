﻿// © Anamnesis.
// Licensed under the MIT license.

namespace Anamnesis.Memory
{
	public class BonesMemory : MemoryBase
	{
		////[Bind(0x000, BindFlags.Pointer)] public IntPtr HkAnimationFile;
		[Bind(0x010)] public ArrayMemory<TransformMemory>? Transforms { get; set; }
	}
}