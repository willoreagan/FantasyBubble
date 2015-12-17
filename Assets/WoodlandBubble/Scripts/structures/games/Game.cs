using System;
using System.Collections.Generic;
using System.Linq;

using System.Xml.Serialization;

using WebApplication10.structures.players;

namespace WebApplication10.structures.games
{
    [Serializable]
    public class Game
    {
        [XmlArray]
        public List<player> Players = new List<player>();

        [XmlAttribute]
        public string FivePlayersBG_URL = "leaderboardImage";
        [XmlAttribute]
        public string TitleImage_URL = "/img/games/wizardbubbles/title.png";
        [XmlAttribute]
        public string BrandingGraphic_URL = "brandImage";
        [XmlAttribute]
        public int TimeRemaining = 120;
        [XmlAttribute]
        public string appleStore_URL = "www.tinyurl.com/hello";
        [XmlAttribute]
        public string googleStore_URL = "www.tinyurl.com/asfaljw";
        [XmlAttribute]
        public string appleQRCode_URL = "www.tinyurl.com/aakljw";
        [XmlAttribute]
        public string googleQRCode_URL = "www.tinyurl.com/asfaljw";
    }
}