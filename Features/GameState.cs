﻿using System;
using System.Collections.Generic;
using System.IO;
using Comfort.Common;
using EFT.Trainer.Configuration;
using EFT.Trainer.Extensions;
using UnityEngine;

#nullable enable

namespace EFT.Trainer.Features
{
	public class GameState : CachableMonoBehaviour<GameStateSnapshot>
	{
		public static GameStateSnapshot? Current { get; private set; }

		public override float CacheTimeInSec { get; set; } = 2f;

		[ConfigurationProperty(Skip = true)] // we do not want to offer save/load support for this
		public override bool Enabled { get; set; } = true;

		[ConfigurationProperty(Skip = true)] // we do not want to offer save/load support for this
		public override KeyCode Key { get; set; } = KeyCode.None;

		public static Shader OutlineShader { get; private set; }

		private void Awake()
		{
			var bundle = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, "outline"));
			OutlineShader = bundle.LoadAsset<Shader>("assets/outline.shader");
		}

		public override GameStateSnapshot? RefreshData()
		{
			var snapshot = new GameStateSnapshot();
			var world = Singleton<GameWorld>.Instance;

			if (world == null)
				return null;

			var players = world.RegisteredPlayers;
			if (players == null)
				return null;

			var hostiles = new List<Player>();
			snapshot.Hostiles = hostiles;

			foreach (var player in players)
			{
				if (player.IsYourPlayer)
				{
					snapshot.LocalPlayer = player;
					continue;
				}

				if (!player.IsAlive())
					continue;

				hostiles.Add(player);
			}

			snapshot.Camera = Camera.main;

			Current = snapshot;
			return snapshot;
		}
	}

	public class GameStateSnapshot
	{
		public Camera? Camera { get; set; }
		public Player? LocalPlayer { get; set; }
		public IEnumerable<Player> Hostiles { get; set; } = Array.Empty<Player>();
	}
}
