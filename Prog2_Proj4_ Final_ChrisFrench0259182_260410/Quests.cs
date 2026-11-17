using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prog2_Proj4_Final_ChrisFrench0259182_260410;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public abstract class Quest
    {
        public string _name { get; set; }
        public bool _complete { get; set; }
        public abstract void CountTowards();
    }
    public abstract class NumberQuest : Quest
    {
        public int _numCompleted { get; set; }
        public int _numNeeded { get; set; }
    }
    public abstract class BoolQuest : Quest
    {

    }
    public class KillQuest : NumberQuest
    {
        public override void CountTowards()
        {
            _numCompleted++;
            if (_numCompleted >= _numNeeded) _complete = true;
        }
        public KillQuest(int needed)
        {
            _name = "Slay " + needed + " Enemies";
            _numNeeded = needed;
        }
    }
    public class LocationQuest : BoolQuest
    {
        public char _locationChecker { get; set; }
        public override void CountTowards()
        {
            _complete = true;
        }

        public LocationQuest(char location, string name)
        {
            _name = name;
            _locationChecker = location;
        }
    }
    public class QuestManager
    {
        public QuestManager() 
        {
            quests = GameManager.fileLoader._currentData._questData.GetAllQuests();
        }

        public Quest[] quests { get; private set; }
        public void EnemyKilled()
        {
            foreach (Quest q in quests)
            {
                if (!(q is KillQuest) || q._complete) continue;
                KillQuest locationQuest = (KillQuest)q;
                locationQuest.CountTowards();
            }
        }

        public void LocationVisited(char c)
        {
            foreach (Quest q in quests)
            {
                if (!(q is LocationQuest) || q._complete) continue;
                LocationQuest locationQuest = (LocationQuest)q;
                if(locationQuest._locationChecker == c) locationQuest.CountTowards();
            }
        }
    }
}
