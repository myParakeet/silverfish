using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_KAR_114 : SimTemplate //Barnes
    {
        // Battlecry: Summon a 1/1 copy of a random minion in your deck.

        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.NEW1_019); // Knife Juggler (for a conservative proc)

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            int pos = (own.own) ? p.ownMinions.Count : p.enemyMinions.Count;
            p.callKid(kid, pos, own.own);

            List<Minion> temp = (own.own) ? p.ownMinions : p.enemyMinions;
            temp[pos].Hp = 1;
            temp[pos].Angr = 1;
        }
    }
}