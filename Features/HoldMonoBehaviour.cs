﻿using EFT.Trainer.Configuration;
using UnityEngine;

#nullable enable

namespace EFT.Trainer.Features
{
	public class HoldMonoBehaviour : MonoBehaviour
	{
		[ConfigurationProperty]
		public virtual KeyCode Key { get; set; } = KeyCode.None;

		private void Update()
		{
			if (Key != KeyCode.None && Input.GetKey(Key))
				UpdateWhenHold();
		}

		protected virtual void UpdateWhenHold() {}
	}
}
