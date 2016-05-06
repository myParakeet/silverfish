using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_OG_133 : SimTemplate //* N'Zoth, the Corruptor
    {
        //Battlecry: Summon your Deathrattle minions that died this game.

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            int kids = 7 - p.ownMinions.Count;
           
            for (int i = 0; i < kids; i++)
            {
                p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_078), own.zonepos, own.own); 
            }
        }
    }
}