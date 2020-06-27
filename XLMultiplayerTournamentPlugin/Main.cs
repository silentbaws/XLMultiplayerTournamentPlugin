using Newtonsoft.Json;
using System;
using System.IO;
using XLMultiplayerServer;

namespace XLMultiplayerTournamentPlugin {
	public class StatSettings {
		[JsonProperty]
		public float maxGravity;

		[JsonProperty]
		public float minGravity;

		[JsonProperty]
		public float maxPopForce;

		[JsonProperty]
		public float minPopForce;

		[JsonProperty]
		public float maxHighPopForce;

		[JsonProperty]
		public float minHighPopForce;

		[JsonProperty]
		public float maxGrindPopForce;

		[JsonProperty]
		public float minGrindPopForce;

		[JsonProperty]
		public float maxManualPopForce;

		[JsonProperty]
		public float minManualPopForce;

		[JsonProperty]
		public float maxFlipSpeed;

		[JsonProperty]
		public float minFlipSpeed;
		
		[JsonProperty]
		public float maxScoopSpeed;

		[JsonProperty]
		public float minScoopSpeed;

		[JsonProperty]
		public float maxPushForce;

		[JsonProperty]
		public float minPushForce;

		[JsonProperty]
		public float maxTopSpeed;

		[JsonProperty]
		public float minTopSpeed;

		[JsonProperty]
		public float maxBodySpin;

		[JsonProperty]
		public float minBodySpin;

		[JsonProperty]
		public float maxPumpForce;

		[JsonProperty]
		public float minPumpForce;

		[JsonProperty]
		public bool enforceManualCatchSetting;

		[JsonProperty]
		public bool useManualCatch;
	}

	public class Main {
		private static Plugin pluginInfo;
		private static StatSettings statSettings;

		public static void Load(Plugin info) {
			pluginInfo = info;
			pluginInfo.OnToggle = OnToggle;
			pluginInfo.ProcessMessage = ProccessMessage;
		}

		private static void OnToggle(bool enabled) {
			if (enabled) {
				string settingsString = File.ReadAllText(Path.Combine(pluginInfo.path, "Settings.json"));
				statSettings = JsonConvert.DeserializeObject<StatSettings>(settingsString);
			}
		}

		private static void ProccessMessage(Player sender, byte[] message) {
			pluginInfo.LogMessage("Got XXL Message", ConsoleColor.Cyan);
			if (message.Length != 45) {
				pluginInfo.LogMessage($"Player {sender.playerID} does not have xxlmod installed {message.Length}", ConsoleColor.Red);
			} else {
				float gravity = BitConverter.ToSingle(message, 0);
				float popForce = BitConverter.ToSingle(message, 4);
				float highPopForce = BitConverter.ToSingle(message, 8);
				float grindPopForce = BitConverter.ToSingle(message, 12);
				float manualPopForce = BitConverter.ToSingle(message, 16);
				float flipSpeed = BitConverter.ToSingle(message, 20);
				float scoopSpeed = BitConverter.ToSingle(message, 24);
				float pushForce = BitConverter.ToSingle(message, 28);
				float topSpeed = BitConverter.ToSingle(message, 32);
				float bodySpin = BitConverter.ToSingle(message, 36);
				float pumpForce = BitConverter.ToSingle(message, 40);
				bool manualCatch = BitConverter.ToBoolean(message, 44);

				pluginInfo.LogMessage(gravity.ToString(), ConsoleColor.Blue);

				if (statSettings.maxGravity != 0 && (gravity < statSettings.minGravity || gravity > statSettings.maxGravity)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxPopForce != 0 && (popForce < statSettings.minPopForce || popForce > statSettings.maxPopForce)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxHighPopForce != 0 && (highPopForce < statSettings.minHighPopForce || highPopForce > statSettings.maxHighPopForce)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxGrindPopForce != 0 && (grindPopForce < statSettings.minGrindPopForce || grindPopForce > statSettings.maxGrindPopForce)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxManualPopForce != 0 && (manualPopForce < statSettings.minManualPopForce || manualPopForce > statSettings.maxManualPopForce)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxFlipSpeed != 0 && (flipSpeed < statSettings.minFlipSpeed || flipSpeed > statSettings.maxFlipSpeed)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxScoopSpeed != 0 && (scoopSpeed < statSettings.minScoopSpeed || scoopSpeed > statSettings.maxScoopSpeed)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxPushForce != 0 && (pushForce < statSettings.minPushForce || pushForce > statSettings.maxPushForce)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxTopSpeed != 0 && (topSpeed < statSettings.minTopSpeed || topSpeed > statSettings.maxTopSpeed)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxBodySpin != 0 && (bodySpin < statSettings.minBodySpin || bodySpin > statSettings.maxBodySpin)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.maxPumpForce != 0 && (pumpForce < statSettings.minPumpForce || pumpForce > statSettings.maxPumpForce)) {
					pluginInfo.DisconnectPlayer(sender);
				}

				if (statSettings.enforceManualCatchSetting && statSettings.useManualCatch != manualCatch) {
					pluginInfo.DisconnectPlayer(sender);
				}
			}
		}
	}
}
