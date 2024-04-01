using System.IO;
using UnityEngine;

namespace Structure.DataStores
{
    public class JsonDataStore
    {
        private const string DataDirectory = "JsonData/";

        public static void StoreData(string fileName, object data)
        {
            var filePath = GetFilePath(fileName);
            var json = JsonUtility.ToJson(data);

            ResourceDataStore.StoreData(filePath, json);
        }

        public static T GetData<T>(string fileName)
        {
            var filePath = GetFilePath(fileName);
            var data = ResourceDataStore.GetData(filePath);
            if (data == null)
            {
                return default(T);
            }

            return JsonUtility.FromJson<T>(data);
        }

        private static string GetFilePath(string fileName)
        {
            var filePath = Path.Combine(DataDirectory, fileName);
            if (!filePath.EndsWith(".json"))
            {
                filePath += ".json";
            }

            return filePath;
        }

        public static string GetDataDirectory(string fileName) =>
            ResourceDataStore.GetDataDirectory(GetFilePath(fileName));

        public static bool Exists(string file)
        {
            var path = GetFilePath(file);
            return ResourceDataStore.Exists(path);
        }
    }
}