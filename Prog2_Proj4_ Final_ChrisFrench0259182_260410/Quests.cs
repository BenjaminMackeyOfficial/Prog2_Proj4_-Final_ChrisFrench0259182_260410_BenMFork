using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public abstract class Quest
    {
        public string _name;
        public bool _complete;
        public abstract void CountTowards();
    }
    public abstract class NumberQuest : Quest
    {
        public int _numCompleted;
        public int _numNeeded;
    }
    public abstract class BoolQuest : Quest //couldnt thinnk of a better name lol
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
        public char _locationChecker;
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
        /* the 3 quests
        "Ah, that was refreshing! ("Heal from sprint water")"
        "Back you, I say Back! (Defeat an enemy)"
        "Safe at last! (Win)"
        */

        public QuestManager() 
        { 
            quests = new Quest[3];

            quests[0] = new KillQuest(10);
            quests[1] = new LocationQuest('w', "Take a dip! (Heal in water)");
            quests[2] = new LocationQuest('X', "Escape Neverware");
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
