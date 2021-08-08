﻿using System.Diagnostics.CodeAnalysis;
using EFT.Interactive;

#nullable enable

namespace EFT.Trainer.Extensions
{
	public static class LootableContainerExtension
	{
		public static bool IsValid([NotNullWhen(true)] this LootableContainer? lootableContainer)
		{
			return lootableContainer != null 
			       && lootableContainer.Template != null;
		}
	}
}
