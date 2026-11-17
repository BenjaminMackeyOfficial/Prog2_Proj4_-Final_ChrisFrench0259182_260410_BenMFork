using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static prog2_Proj3_beta_ChrisFrench0259182_260324.FileData;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class FileLoader
    {
        public const string DATAFILEPATH = "Data.Json";
        public FileData _currentData { get; set; }

        public FileData LoadFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string j = reader.ReadToEnd();
                    _currentData = JsonConvert.DeserializeObject<FileData>(j);
                }
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
            if (_currentData == null)
            {
                Console.WriteLine("No data Loaded!");
                _currentData = new FileData();
            }
            return _currentData;
        }

        public void CreateExampleFile()
        {
            FileData dat = new FileData();
            QuestData qDat = new QuestData();
            PlayerStatData pDat = new PlayerStatData();

            qDat.AddQuest(new KillQuest(1));
            qDat.AddQuest(new LocationQuest('x', "Reach the end!"));

            pDat._maxNameLLength = 15;
            pDat._plaAtkUP = 10;
            pDat._plaMaxHP = 10;
            pDat._name = "Sir gobsmacked";

            dat.HandPlayerStatData(pDat);
            dat.HandQuestData(qDat);

            _currentData = dat;

            JsonSerializerOptions options = new JsonSerializerOptions
            { WriteIndented = true};


            string j = System.Text.Json.JsonSerializer.Serialize(dat, options);

            File.WriteAllText(DATAFILEPATH, j);
        }

    }
    public class FileData
    {
        public QuestData _questData { get; private set; }
        public PlayerStatData _playerStatData { get; private set;  }

        public void HandQuestData(QuestData qDat) => _questData = qDat;
        public void HandPlayerStatData(PlayerStatData pDat) => _playerStatData = pDat; 

        public FileData()
        {
            _questData = new QuestData();
            _playerStatData = new PlayerStatData();
        }

        //differentTypes
        public class QuestData
        {
            public KillQuest[] _killQuests { get; set; }
            public LocationQuest[] _locationQuest { get; set; }

            public void AddQuest(Quest quest)
            {
                if(_killQuests == null) _killQuests = new KillQuest[0];
                if(_locationQuest == null) _locationQuest = new LocationQuest[0];
                if (quest is LocationQuest)
                {
                    LocationQuest[] nq = new LocationQuest[_locationQuest.Length + 1];
                    _locationQuest.CopyTo(nq, 0);
                    nq[nq.Length - 1] = (LocationQuest)quest;

                    _locationQuest = nq;
                }
                else if (quest is KillQuest)
                {
                    KillQuest[] nq = new KillQuest[_killQuests.Length + 1];
                    _killQuests.CopyTo(nq, 0);
                    nq[nq.Length - 1] = (KillQuest)quest;

                    _killQuests = nq;
                }
                else
                {
                    Console.WriteLine("Error: Quest data type unknown! Stop adding things!");
                }
            }

            public Quest[] GetAllQuests()
            {
                if (_killQuests == null) _killQuests = new KillQuest[0];
                if (_locationQuest == null) _locationQuest = new LocationQuest[0];
                Quest[] q = new Quest[(_killQuests.Length + _locationQuest.Length)];
                _killQuests.CopyTo(q, 0);
                _locationQuest.CopyTo(q, _killQuests.Length);
                return q;
            }
            public QuestData()
            {

            }
        }
        public class PlayerStatData
        {
            public string _name { get; set; }
            public int _maxNameLLength { get; set; }
            public int _plaAtkUP { get; set; }
            public int _plaMaxHP { get; set; }
            public PlayerStatData()
            {

            }
        }
        //
    }


}
