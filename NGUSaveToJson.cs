using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NGUSaveToJson {
	public class NGUSaveToJson {
		public PlayerData playerdata { get; private set; }
		public string playerdataJSON { get; private set; }
		public bool ownSaveLoaded { get; private set; }
		public bool ownSaveShared { get; private set; }
		public string saveId { get; private set; }
		public string rawData { get; private set; }
		public bool loadingFromApi { get; set; }

		public NGUSaveToJson() {
		}

		public async Task MainAsync(string path) {
			if (File.Exists(path)) {
				using (var reader = File.OpenText(path)) {
					string fileRawContents = await reader.ReadToEndAsync();
					SetRawData(fileRawContents);
					SetPlayerData(DecodeToJSON(fileRawContents));
				}
				var dir = Path.GetDirectoryName(path);
				var file = Path.Combine(dir, "ngusave.json");
				using (var writer = File.CreateText(file)) {
					writer.Write(playerdataJSON);
				}
			} else {
				Console.WriteLine("Wrong file path");
			}


		}

		public void SetPlayerData(PlayerData pd) {
			playerdata = pd;
			playerdataJSON = JsonConvert.SerializeObject(playerdata, Formatting.Indented);
			ownSaveLoaded = true;
		}

		public void SetRawData(string raw) {
			rawData = raw;
		}

		private static T DeserializeBase64<T>(string base64Data) {
			byte[] bytes = Convert.FromBase64String(base64Data);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Binder = new CustomBinder();

			using (MemoryStream memoryStream = new MemoryStream(bytes)) {
				return (T) formatter.Deserialize(memoryStream);
			}
		}

		private static string GetChecksum(string data) {
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

			return Convert.ToBase64String(md5.ComputeHash(Convert.FromBase64String(data)));
		}

		public PlayerData DecodeToJSON(string contents) {

			SaveData saveData = DeserializeBase64<SaveData>(contents);

			string checksum = GetChecksum(saveData.playerData);
			if (saveData.checksum != checksum) {
				throw new Exception("checksum mismatch");
			}

			return DeserializeBase64<PlayerData>(saveData.playerData);
		}

		sealed class CustomBinder : SerializationBinder {
			public override Type BindToType(string assemblyName, string typeName) {
				// Force assembly name so we can use 'wrong' assembly in BinaryFormatter
				assemblyName = assemblyName.Replace("1.0.0.0", "0.0.0.0");
				assemblyName = assemblyName.Replace("Assembly-CSharp", "NGUSaveToJson");
				typeName = typeName.Replace("Assembly-CSharp", "NGUSaveToJson");

				// The following line of code returns the type.
				Type typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName), true);

				return typeToDeserialize;
			}
		}
	}
}
