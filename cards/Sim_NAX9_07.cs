using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_NAX9_07 : SimTemplate //* Mark of the Horsemen
	{
		// Give your minions and your weapon +1/+1.
		
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			p.allMinionOfASideGetBuffed(ownplay, 1, 1);
			
			if (ownplay)
            {
                if (p.ownWeaponDurability >= 1)
                {
                    p.ownWeaponDurability++;
                    p.ownWeaponAttack++;
                    p.ownHero.Angr++;
                }
            }
            else
            {
                if (p.enemyWeaponDurability >= 1)
                {
                    p.enemyWeaponDurability++;
                    p.enemyWeaponAttack++;
                    p.enemyHero.Angr++;
                }
            }
		}
	}
}