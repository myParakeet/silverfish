using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_NAX9_04H : SimTemplate //* Sir Zeliek
	{
        // Your hero is Immune.

        public override void onAuraStarts(Playfield p, Minion own)
        {
            if (own.own)
            {
                p.ownHero.immune = true;
                if (p.ownWeaponCard.name == CardDB.cardName.runeblade && p.anzOwnHorsemen < 1)
                {
                    int bonus = (p.ownWeaponCard.cardIDenum == CardDB.cardIDEnum.NAX9_05H) ? 6 : 3;
                    p.minionGetBuffed(p.ownHero, -1 * Math.Min(bonus, p.ownWeaponAttack - 1), 0);
                    p.ownWeaponAttack = Math.Min(1, p.ownWeaponAttack - bonus);
                }
                p.anzOwnHorsemen++;
            }
            else
            {
                p.enemyHero.immune = true;
                if (p.enemyWeaponCard.name == CardDB.cardName.runeblade && p.anzEnemyHorsemen < 1)
                {
                    int bonus = (p.enemyWeaponCard.cardIDenum == CardDB.cardIDEnum.NAX9_05H) ? 6 : 3;
                    p.minionGetBuffed(p.enemyHero, -1 * Math.Min(bonus, p.enemyWeaponAttack - 1), 0);
                    p.enemyWeaponAttack = Math.Min(1, p.enemyWeaponAttack - bonus);
                }
                p.anzEnemyHorsemen++;
            }
        }

        public override void onAuraEnds(Playfield p, Minion own)
        {
            if (own.own)
            {
                p.anzOwnHorsemen--;
                if (p.anzOwnHorsemen < 1)
                {
                    p.ownHero.immune = false;
                    if (p.ownWeaponCard.name == CardDB.cardName.runeblade)
                    {
                        int bonus = (p.ownWeaponCard.cardIDenum == CardDB.cardIDEnum.NAX9_05H) ? 6 : 3;
                        p.minionGetBuffed(p.ownHero, bonus, 0);
                        p.ownWeaponAttack += bonus;
                    }
                }
            }
            else
            {
                p.anzEnemyHorsemen--;
                if (p.anzEnemyHorsemen < 1)
                {
                    p.enemyHero.immune = false;
                    if (p.enemyWeaponCard.name == CardDB.cardName.runeblade)
                    {
                        int bonus = (p.enemyWeaponCard.cardIDenum == CardDB.cardIDEnum.NAX9_05H) ? 6 : 3;
                        p.minionGetBuffed(p.enemyHero, bonus, 0);
                        p.enemyWeaponAttack += bonus;
                    }
                }
            }
        }
    }
}