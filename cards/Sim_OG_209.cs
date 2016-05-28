using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_OG_209 : SimTemplate //* Hallazeal the Ascended
	{
        //Whenever your spells deal damage, restore that much Health to your hero.

        public override void onCardIsGoingToBePlayed(Playfield p, CardDB.Card c, bool wasOwnCard, Minion triggerEffectMinion, Minion target, int choice)
        {
            if (triggerEffectMinion.own == wasOwnCard && c.type == CardDB.cardtype.SPELL)
            {
                target2 = (wasOwnCard) ? p.ownHero : p.enemyHero;
                p.minionGetDamageOrHeal(target2, -PenalityManager.Instance.guessTotalSpellDamage(p, c.name, wasOwnCard));
            }
        }
    }
}