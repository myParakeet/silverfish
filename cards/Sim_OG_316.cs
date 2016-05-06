using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_OG_316 : SimTemplate //* Herald Volazj
	{
		//Battlecry: Summon a 1/1 copy of each of your other minions.

		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            List<Minion> temp = (own.own) ? p.ownMinions : p.enemyMinions;
            int pos = temp.Count;
            if (pos > 2)
            {
                int i = 0;
                int count = pos - 1;
                CardDB.Card kid = null;
                for (; pos < 7; pos++)
                {   
                    kid = temp[i].handcard.card;
                    p.callKid(kid, pos, own.own);
                    temp[pos].Hp = 1;
                    temp[pos].Angr = 1;
                    count--;
                    if (count < 1) break;
                    i++;
                }
            }
        }
    }
}