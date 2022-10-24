
using System.IO.Pipes;

Console.WriteLine("SimpleClient Started!");

//connect
var pipe = new NamedPipeClientStream(".", "DemoPipe", 
    PipeDirection.InOut);

Console.WriteLine("SimpleClient waiting for connection!");
pipe.Connect(); //will return only once a connection is established
Console.WriteLine("Connected with server");

//read data
var dataReceive = pipe.ReadByte();
Console.WriteLine("Client receive: " + dataReceive);

//write data
byte dataSend = 24;
pipe.WriteByte(dataSend);
Console.WriteLine("Client send: " + dataSend);

//close pipe
pipe.Close();

Console.ReadKey();