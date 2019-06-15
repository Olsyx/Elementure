using Elementure.GameLogic;
using Elementure.GameLogic.Words;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VerbSheetLoader {

	private static string MODIFIERS_PATH = Application.dataPath + "/GameFiles/VerbModifiers/";
	private static string MODIFIERS_EXTENSION = "json";
	
	static VerbSheetLoader() {
		FileManager.TryDirectory(MODIFIERS_PATH);
	}
		
	public static VerbSheet Load(string fileName) {
		string fileData = FileManager.Read($"{MODIFIERS_PATH}{fileName}.{MODIFIERS_EXTENSION}");
		return JsonConvert.DeserializeObject<VerbSheet>(fileData);
	}

	private static void WriteDummy() {
		VerbSheet dummySheet = new VerbSheet();
		FileManager.WriteTo($"{MODIFIERS_PATH}Dummy.{MODIFIERS_EXTENSION}", JsonConvert.SerializeObject(dummySheet));
	}
}
