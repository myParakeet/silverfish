using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_OG_114 : SimTemplate //* Forbidden Ritual
	{
		//Spend all your Mana. Summon that many 1/1 Tentacles.

        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.OG_114a); //Icky Tentacle

		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			if (ownplay)
			{
				int pos = p.ownMinions.Count;
				if (p.mana > 0)
				{
					p.callKid(kid, pos, ownplay, false);
					p.mana--;
					for (int i = 0; i < p.mana; i++)
					{
						p.callKid(kid, pos, ownplay);
					}
					p.mana = 0;
				}				
			}
		}
	}
}