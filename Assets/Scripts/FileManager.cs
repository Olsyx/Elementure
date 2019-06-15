using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Elementure.GameLogic {

	public static class FileManager {
		public static void TryDirectory(string path) {
			if (Directory.Exists(path)) {
				return;
			}
			Directory.CreateDirectory(path);
		}

		public static bool Exists(string path) {
			return File.Exists(path);
		}

		public static int Count(string path, string extension) {
			return Directory.GetFiles(path, $"*.{extension}", SearchOption.AllDirectories).Length;
		}

		public static void Create(string path) {
			FileStream file = File.Create(path);
			file.Close();
		}

		public static string Read(string path) {
			return File.ReadAllText(path);
		}

		public static void WriteTo(string path, string data) {
			using (StreamWriter writer = new StreamWriter(path, false)) {
				writer.WriteLine(data);
			}
		}
	}

}