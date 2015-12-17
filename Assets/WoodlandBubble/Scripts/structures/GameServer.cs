using UnityEngine;
using System.Collections;
using WebApplication10.structures.games;
using WebApplication10.structures.players;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Text;
using System;
public class GameServer : MonoBehaviour {

	public static GameServer instance = null;
	private Game _game;
	private float timer = 3;
	private FtpWebRequest request;
	private float timeRemaining = 120;
	// Use this for initialization
	void Start () {
		// Get the object used to communicate with the server.
		/*request = (FtpWebRequest)WebRequest.Create("ftp://www.funwall.net/");
		request.Method = WebRequestMethods.Ftp.UploadFile;

		// This example assumes the FTP site uses anonymous logon.
		request.Credentials = new NetworkCredential ("funwalladmin","");

		// Copy the contents of the file to the request stream.
		StreamReader sourceStream = new StreamReader("testfile.txt");
		byte [] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
		sourceStream.Close();
		request.ContentLength = fileContents.Length;

		Stream requestStream = request.GetRequestStream();
		requestStream.Write(fileContents, 0, fileContents.Length);
		requestStream.Close();

		FtpWebResponse response = (FtpWebResponse)request.GetResponse();

		Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

		response.Close();*/

		instance = this;
		_game = new Game();
		_game.Players.Add(new player("john", "img/avatars/ninja.png", 3403));
		_game.Players.Add(new player("Reese", "img/avatars/cptamerica.png", 3403));
		_game.Players.Add(new player("Belvedier", "img/avatars/ironman.png", 8458));
		_game.Players.Add(new player("Excelcisior", "img/avatars/alien.png", 8384));
		_game.Players.Add(new player("Blue", "img/avatars/thor.png", 8223));
		_game.Players.Add(new player("Xavier", "img/avatars/geek.png", 3483));


		
	}


	
	// Update is called once per frame
	void Update () {
		if(timeRemaining < 0 && timeRemaining != null)
			return;
		timer+= Time.deltaTime;
		if(timer > 1)
		{
			_game.TimeRemaining = (int)timeRemaining;
			string s = "";
			XmlSerializer _mySerializer = new XmlSerializer(typeof(Game)); //serializer variable
			using (StringWriter textWriter = new StringWriter()) 
			{
				_mySerializer.Serialize(textWriter, _game);
				s = textWriter.ToString();
			}
			timer = 0;
        


            //WebRequest request = WebRequest.Create("http://localhost:52487/recievexml");
            WebRequest request = WebRequest.Create("http://webapplication107066.azurewebsites.net/recievexml");
            request.Method = "POST";

			string postData = s;
            Debug.Log(postData);
            byte [] byteArray = Encoding.UTF8.GetBytes(postData);
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = byteArray.Length;
			Stream dataStream = request.GetRequestStream();
			dataStream.Write(byteArray, 0, byteArray.Length);
			dataStream.Close();
			WebResponse response = request.GetResponse();
			Debug.Log("HTTP Response: " + ((HttpWebResponse)response).StatusDescription);
			dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);
			string responseFromServer = reader.ReadToEnd();
			Debug.Log("Server Response: " + responseFromServer);
			reader.Close();
			dataStream.Close();
			response.Close();

			for(int i = 0; i< PlayerInstance._otherPlayers.Count; i++)
			{
				PlayerInstance._otherPlayers[i].RpcUpdateTime((int)timeRemaining);
			}

		}
		timeRemaining -= Time.deltaTime;
		if(timeRemaining < 0)
		{
			for(int i = 0; i< PlayerInstance._otherPlayers.Count; i++)
			{
				PlayerInstance._otherPlayers[i].RpcRoundOver();
			}
		}
	}

	void OnExit() {
		
	}
}
