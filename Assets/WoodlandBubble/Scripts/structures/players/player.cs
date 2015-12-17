using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace WebApplication10.structures.players
{
    [Serializable]
	public class player
    {
        public player()
        {

        }
        [XmlAttribute]
        public string name = "hello";
        [XmlAttribute]
        public string avatar = "avi";
        [XmlAttribute]
        public int score = 100;

		public player(string name, string avatar, int score)
		{
			this.name = name;
			this.avatar = avatar;
			this.score = score;
		}
    }
}