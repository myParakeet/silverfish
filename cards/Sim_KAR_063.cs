using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_KAR_063 : SimTemplate //Spirit Claws
    {
        // Has +2 Attack while you have Spell Damage.

        CardDB.Card card = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.KAR_063);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.equipWeapon(card, ownplay);
        }
    }
}