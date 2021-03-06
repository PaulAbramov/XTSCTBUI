﻿/*

___    ___  ______   ________    __       ______
\  \  /  / |   ___| |__    __|  /  \     |   ___|
 \  \/  /  |  |___     |  |    / /\ \    |  |__
  |    |   |   ___|    |  |   /  __  \    \__  \
 /	/\  \  |  |___     |  |  /  /  \  \   ___|  |
/__/  \__\ |______|    |__| /__/    \__\ |______|

Written by Paul "Xetas" Abramov


*/

using System.Collections.Generic;
using Newtonsoft.Json;

namespace CTBUI.Web.TradeOffer.JsonClasses
{
    /// <summary>
    /// Class to serialize and deserialize a response from the API call "IEconService/GetTradeOffers/v1"
    /// JsonProperty gets the result default values and parses it into our variables
    /// </summary>
    public class GetOffersResponse
    {
        [JsonProperty("trade_offers_sent")]
        public List<TradeOffer> TradeOffersSent { get; set; }

        [JsonProperty("trade_offers_received")]
        public List<TradeOffer> TradeOffersReceived { get; set; }

        [JsonProperty("descriptions")]
        public List<TradeOfferItemDescription> Descriptions { get; set; }
    }
}
