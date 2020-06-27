using XLMultiplayer;
using HarmonyLib;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace XLMultiplayerTournamentPluginClient {
    public class PluginMain {
		private static Plugin pluginInfo;

		private static void Load(Plugin plugin) {
			pluginInfo = plugin;
			pluginInfo.OnToggle = OnToggle;
		}

		private static void OnToggle(bool enabled) {
			if (enabled) {
				if (AccessTools.TypeByName("XXLMod.Settings") != null) {
					CheckStats();
				} else {
					HandleFailure();
				}
			}
		}

		private static XXLMod.Settings GetSettings() {
			return Traverse.Create(AccessTools.TypeByName("XXLMod.Main")).Field("settings").GetValue<XXLMod.Settings>();
		}

		private static void SetSettings(XXLMod.Settings settings) {
			Traverse.Create(AccessTools.TypeByName("XXLMod.Main")).Field("settings").SetValue(settings);
		}

		private static void HandleFailure() {
			pluginInfo.SendMessage(pluginInfo, new byte[] { 0 }, true);
		}

		private static async void CheckStats() {
			while (pluginInfo.enabled) {
				// TODO: Get settings here and send to server

				XXLMod.Settings stats = GetSettings();

				List<byte> sendMessage = new List<byte>();
				sendMessage.AddRange(BitConverter.GetBytes(stats.gravity));
				sendMessage.AddRange(BitConverter.GetBytes(stats.popForce));
				sendMessage.AddRange(BitConverter.GetBytes(stats.highPopForce));
				sendMessage.AddRange(BitConverter.GetBytes(stats.grindPopForce));
				sendMessage.AddRange(BitConverter.GetBytes(stats.manualPopForce));
				sendMessage.AddRange(BitConverter.GetBytes(stats.flipSpeed));
				sendMessage.AddRange(BitConverter.GetBytes(stats.scoopSpeed));
				sendMessage.AddRange(BitConverter.GetBytes(stats.pushForce));
				sendMessage.AddRange(BitConverter.GetBytes(stats.topSpeed));
				sendMessage.AddRange(BitConverter.GetBytes(stats.bodySpin));
				sendMessage.AddRange(BitConverter.GetBytes(stats.maxPumpSpeed));
				sendMessage.AddRange(BitConverter.GetBytes(stats.manualCatch));

				pluginInfo.SendMessage(pluginInfo, sendMessage.ToArray(), true);

				await Task.Delay(10000);
			}
		}
	}
}
