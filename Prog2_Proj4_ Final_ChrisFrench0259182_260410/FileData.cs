using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static prog2_Proj3_beta_ChrisFrench0259182_260324.FileData;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class FileLoader
    {
        public FileData _currentData;
        
        public FileData LoadFile(string path)
        {
            try
            {
                string j = File.ReadAllText(path);
                _currentData = JsonSerializer.Deserialize<FileData>(j); 
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: The file does not exist at the specified path.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: You do not have permission to access this file.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Disk or network error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            if(_currentData == null) _currentData = new FileData();
            return _currentData;
        }

        public void CreateExampleFile()
        {
            FileData dat = new FileData();
            QuestData qDat = new QuestData();
            PlayerStatData pDat = new PlayerStatData();
        }
        public void Save(string path)//not called
        {
            string j = JsonSerializer.Serialize(_currentData);
            File.WriteAllText(path, j);
        }

    }
    public class FileData
    {
        public QuestData _questData { get; private set; }
        public PlayerStatData _playerStatData { get; private set;  }
        public FileData()
        {
            _questData = new QuestData();
            _playerStatData = new PlayerStatData(); 
        }

        //differentTypes
        public class QuestData
        {
            public KillQuest[] _killQuests { get; private set; }
            public LocationQuest[] _locationQuest { get; private set; }

            public Quest[] _allQuests { get {

                    Quest[] q = new Quest[(_killQuests.Length + _locationQuest.Length) - 2];
                    _killQuests.CopyTo(q, 0);
                    _locationQuest.CopyTo(q, _killQuests.Length);

                    return q;
                } }

            public QuestData()
            {

            }
        }
        public class PlayerStatData
        {
            public string _name;
            public int _maxNameLLength;
            public int _plaAtkUP;
            public int _plaMaxHP;
            public PlayerStatData()
            {

            }
        }
        //
    }


}
