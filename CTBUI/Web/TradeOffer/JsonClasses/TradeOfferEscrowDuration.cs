﻿/*

___    ___  ______   ________    __       ______
\  \  /  / |   ___| |__    __|  /  \     |   ___|
 \  \/  /  |  |___     |  |    / /\ \    |  |__
  |    |   |   ___|    |  |   /  __  \    \__  \
 /	/\  \  |  |___     |  |  /  /  \  \   ___|  |
/__/  \__\ |______|    |__| /__/    \__\ |______|

Written by Paul "Xetas" Abramov


*/

namespace CTBUI.Web.TradeOffer.JsonClasses
{
    /// <summary>
    /// Class to serialize and deserialize the escrowduration of a trade, so we know if it will be hold by steam for some days
    /// </summary>
    public class TradeOfferEscrowDuration
    {
        public bool Success { get; set; }
        public int DaysOurEscrow { get; set; }
        public int DaysTheirEscrow { get; set; }
    }
}
