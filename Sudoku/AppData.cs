using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sudoku
{
    public class AppData
    {
        private static Dictionary<Difficulty, TimeSpan> _timestamps = new();
        private static string _folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MySudoku";
        private static string _file = "stats.json";
        private static string _fullPath = $"{_folder}\\{_file}";

        static AppData()
        {
            if (File.Exists(_fullPath))
            {
                _timestamps = JsonSerializer.Deserialize<Dictionary<Difficulty, TimeSpan>>(File.ReadAllText(_fullPath));
                if (_timestamps == null) _timestamps = new();
            }
            else Directory.CreateDirectory(_folder);
        }

        private static void save()
        {
            File.WriteAllText(_fullPath, JsonSerializer.Serialize(_timestamps));
        }

        public static void CheckThenEdit(Difficulty difficulty, TimeSpan time)
        {
            TimeSpan last = Get(difficulty);

            if (time < last || last == TimeSpan.Zero)
            {
                _timestamps[difficulty] = time;
                save();
            }
        }

        public static TimeSpan Get(Difficulty difficulty)
        {
            if (_timestamps.ContainsKey(difficulty))
                return _timestamps[difficulty];
            return TimeSpan.Zero;
        }  
    }
}
