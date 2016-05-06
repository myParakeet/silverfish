using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_OG_109 : SimTemplate //* Darkshire Librarian
    {
        //Battlecry: Discard a random card. Deathrattle: Draw a card.

        public override void onDeathrattle(Playfield p, Minion m)
        {
            p.drawACard(CardDB.cardName.unknown, m.own);
        }

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (own.own)
            {
                if (p.owncards.Count > 0)
                {
                    p.owncarddraw--;
                    p.owncards.RemoveAt(0);
                    p.triggerCardsChanged(true);
                }
            }
            else
            {
                if (p.enemyAnzCards > 0)
                {
                    p.enemycarddraw--;
                    p.enemyAnzCards--;
                    p.triggerCardsChanged(false);
                }
            }
        }
    }
}