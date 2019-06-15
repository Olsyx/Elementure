using Elementure.GameLogic;
using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {
	public static class VerbManager {

		private static string MODIFIERS_PATH = Application.dataPath + "/GameFiles/VerbModifiers/";
		private static string MODIFIERS_EXTENSION = "json";

		static VerbManager() {
			FileManager.TryDirectory(MODIFIERS_PATH);
		}

		public static Verb GetVerb(Agent agent, VerbTypes type, ModifierTypes modifier = ModifierTypes.None) {
			switch(type) {
				case VerbTypes.Walk:
					return new Walk(modifier, agent);

				case VerbTypes.Jump:
					return new Jump(modifier, agent);

				case VerbTypes.Roll:
					return new Roll(modifier, agent);

				case VerbTypes.Teleport:
					return new Teleport(modifier, agent);

				case VerbTypes.Strike:
					return new Strike(modifier, agent);

				case VerbTypes.Shoot:
					return new Shoot(modifier, agent);

				case VerbTypes.Protect:
					return new Protect(modifier, agent);

				case VerbTypes.Throw:
					return new Throw(modifier, agent);

				case VerbTypes.Invert:
					return new Invert(modifier, agent);

			}
			return null;
		}

		public static VerbSheet LoadProfile(string fileName) {
			string fileData = FileManager.Read($"{MODIFIERS_PATH}{fileName}.{MODIFIERS_EXTENSION}");
			return JsonConvert.DeserializeObject<VerbSheet>(fileData);
		}

		private static void WriteDummy() {
			VerbSheet dummySheet = new VerbSheet();
			FileManager.WriteTo($"{MODIFIERS_PATH}Dummy.{MODIFIERS_EXTENSION}", JsonConvert.SerializeObject(dummySheet));
		}
	}
}