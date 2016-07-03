using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Pen_EX1_162 : PenTemplate //direwolfalpha
	{
		public override float getPlayPenalty(Playfield p, Handmanager.Handcard hc, Minion target, int choice, bool isLethal)
		{
            return p.enemyMinions.Count == 0 ? 5 : 0; // Small penalty for playing dire wolf alpha against an empty board
        }
	}
}
