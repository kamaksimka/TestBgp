using TestBgp;

string serverIp = "109.167.241.225";
int port = 1790;

int studentNumber = 8; // ← ВПИШИ СВОЙ НОМЕР
int asn = 65000 + studentNumber;

var client = new BgpClient(serverIp, port, asn);
client.Run();

Console.ReadLine();