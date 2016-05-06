using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_LOE_026 : SimTemplate //* Anyfin Can Happen
	{
		//Summon 7 Murlocs that died this game.

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int place = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;

            p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_506a), place, ownplay, false);
            p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_509), place, ownplay);
            p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_506), place, ownplay);
            p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_064), place, ownplay);
            p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_508), place, ownplay);
            p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_507), place, ownplay);
            p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_076), place, ownplay);
        }
    }
}