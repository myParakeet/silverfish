using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_OG_302 : SimTemplate //* Usher of Souls
	{
		//Whenever a friendly minion dies, give your C'Thun +1/+1 (wherever it is).
		
        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            int diedMinions = (m.own) ? p.tempTrigger.ownMinionsDied : p.tempTrigger.enemyMinionsDied;
            if (diedMinions == 0) return;
            int residual = (p.pID == m.pID) ? diedMinions - m.extraParam2 : diedMinions;
            m.pID = p.pID;
            m.extraParam2 = diedMinions;
			p.anzOgOwnCThunHpBonus += residual;
			p.anzOgOwnCThunAngrBonus += residual;
        }
	}
}