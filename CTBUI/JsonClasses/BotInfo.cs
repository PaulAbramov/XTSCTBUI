﻿/*

___    ___  ______   ________    __       ______
\  \  /  / |   ___| |__    __|  /  \     |   ___|
 \  \/  /  |  |___     |  |    / /\ \    |  |__
  |    |   |   ___|    |  |   /  __  \    \__  \
 /	/\  \  |  |___     |  |  /  /  \  \   ___|  |
/__/  \__\ |______|    |__| /__/    \__\ |______|

Written by Paul "Xetas" Abramov


*/

namespace CTBUI.JsonClasses
{
    /// <summary>
    /// This class will be the storage for all the info we need for out Bot
    /// Username is the username to login to the bot
    /// Password is the password to login to the bot
    /// BotName is the displayed name of the bot to the steamcommunity
    /// AcceptDonations can be set to true or false without the upper quotation marks
    /// AcceptEscrow defines if we want to accept trades which will be held back for some days, can be set to true or false without the upper quotation marks
    /// Accept1on1Trades defines if we want to accept trades with cards which are 1:1 from same set, can be set to true or false without the upper quoatation marks
    /// Accept1on2Trades defines if we want to accept trades with cards which are 1:2 (do not have to be same set), can be set to true or false without the upper quotation marks
    /// Admins is a string array which holds the uint64 steamID of the admins which can interact with the bot and receive his items and so on
    /// Admins are separated by a comma and look like this: "76561198000479819"
    /// </summary>
    public class BotInfo
    {
        public string Username;
        public string Password;
        public string BotName = "";
        public bool AcceptFriendRequests = true;
        public bool AcceptDonations = true;
        public bool AcceptEscrow = false;
        public bool Accept1on1Trades = true;
        public bool Accept1on2Trades = true;
        public string GroupToInviteTo = "";
        public string[] Admins = { "" };
    }
}
