using Godot;
using hundun.unitygame.gamelib;
using System.Diagnostics;
using System.IO;
using System.Text.Json; // 导入 System.Text.Json 命名空间
using System.Text.Json.Serialization; // 用于 JSON 属性和选项
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace hundun.unitygame.adapters
{
	public class GodotSaveTool<T_SAVE> : ISaveTool<T_SAVE>
	{
		//const string ITCHIO_FRIENDLY_FOLDER = "/idbfs/9c227d13233f21c6cb7967e47e8aed70-v20230406";
		const string fileName = "save.json";
		static JsonSerializerOptions options = new JsonSerializerOptions
		{
			IncludeFields = true, // 启用字段反序列化
			WriteIndented = true, // 输出可读性更好的 JSON
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // 属性名转换为驼峰命名法（如 PlayerName -> playerName）
			// 核心设置：确保 null 值不会被忽略
			DefaultIgnoreCondition = JsonIgnoreCondition.Never
		};

		public bool hasRootSave()
		{
			return File.Exists(GetFilePath(fileName));
		}

		public void lazyInitOnGameCreate()
		{
			GD.Print("Current UnitySaveTool GetFilePath = " + GetFilePath(fileName));
		}

		public T_SAVE readRootSaveData()
		{
			string json = ReadFromFIle(fileName);
			T_SAVE data = JsonSerializer.Deserialize<T_SAVE>(json, options);
			return data;
		}

		public void writeRootSaveData(T_SAVE saveData)
		{
			string json = JsonSerializer.Serialize(saveData, options);
			WriteToFile(fileName, json);
		}

		private void WriteToFile(string fileName, string json)
		{
			string path = GetFilePath(fileName);
			FileStream fileStream = new FileStream(path, FileMode.Create);

			using (StreamWriter writer = new StreamWriter(fileStream))
			{
				writer.Write(json);
			}
		}

		private string ReadFromFIle(string fileName)
		{
			string path = GetFilePath(fileName);
			if (File.Exists(path))
			{
				using (StreamReader reader = new StreamReader(path))
				{
					string json = reader.ReadToEnd();
					GD.Print("ReadFromFIle Success by path: " + path);
					return json;
				}
			}
			else
			{
				GD.PushWarning("File not found");
			}

			return "ReadFromFIle fail: " + path;
		}

		private string GetFilePath(string fileName)
		{
			// 1. 构建 Godot 用户路径
			string godotUserPath = "user://" + fileName;

			// 2. 将 Godot 路径转换为操作系统文件路径
			string osFilePath = ProjectSettings.GlobalizePath(godotUserPath);
			GD.Print("osFilePath = " + osFilePath);
			return osFilePath;
		}
	}
}
