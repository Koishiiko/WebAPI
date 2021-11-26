using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace WebAPI.utils {
    public static class FileUtils {

        public static string SaveFile(IFormFile file, string path) {
            string newPath = Renamne(path, out string newName);
            using (var fs = File.Create(newPath)) {
                file.OpenReadStream().CopyTo(fs);
            }

            return newName;
        }

        private static string Renamne(string path, out string newName) {
            if (!File.Exists(path)) {
                newName = Path.GetFileName(path);
                return path;
            }

            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);

            // 如果文件名以(i)结尾 则将(i)移除后重新添加
            Match match = Regex.Match(name, @"^.*?(?=\s?\(\d\))");
            name = match.Success ? match.Value : name;

            int count = 1;
            string newPath;
            do {
                newName = $"{name} ({count}){extension}";
                newPath = Path.Combine(directory, newName);
                count++;
            } while (File.Exists(newPath));

            return newPath;
        }
    }
}
