using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Pen_CS2_022 : PenTemplate //polymorph
	{
		public override float getPlayPenalty(Playfield p, Handmanager.Handcard hc, Minion target, int choice, bool isLethal)
		{
            if (target.own)
            {
                return 500; //todo parakeet; is this necessary? allow polymorph own if advantagous?
            }
            else
            {
                return 16; //cardvalue; don't waste on small threats
            }
        }
	}
}
