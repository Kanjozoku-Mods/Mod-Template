using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace KanjozokuMod {
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	[BepInProcess("Kanjozoku Game.exe")]
    public class KanjozokuExampleMod : BaseUnityPlugin {
		
		public Harmony Harmony { get; } = new Harmony(PluginInfo.PLUGIN_GUID);
		
		public static KanjozokuExampleMod Instance = null;
		
        private void Awake() {
			/* Keep Instance */
			Instance = this;
			
			/* Unity Patching */
			Harmony.PatchAll();
			Logger.LogInfo($"{PluginInfo.PLUGIN_GUID} is loaded!");
        }
		
		private void _Log(string msg, LogLevel lvl) {
			Logger.Log(lvl, msg);
		}

		public static void Log(string msg, LogLevel lvl = LogLevel.Info) {
			if (KanjozokuExampleMod.Instance == null)
				return;
			Instance._Log(msg, lvl);
		}
    }
	
    [HarmonyPatch(typeof(GlobalManager), "Start")]
    public static class ExamplePatch {
        private static void Prefix(GlobalManager __instance) {
			KanjozokuExampleMod.Log("Game Has Started", LogLevel.Message);
        }
    }
}
