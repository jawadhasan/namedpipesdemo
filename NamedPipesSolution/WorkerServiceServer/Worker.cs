using System.IO.Pipes;

namespace WorkerServiceServer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly NamedPipeServerStream _pipe;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            //create pipe
            _pipe = new NamedPipeServerStream("DemoPipe", PipeDirection.InOut, 
                1);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Demo1(stoppingToken);
            Console.ReadKey();
        }

        private async Task Demo1(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Server is started!");

            //wait until client connects
            await _pipe.WaitForConnectionAsync(stoppingToken);
            _logger.LogInformation("Client is connected");


            //write and read data
            try
            {
                //write data
                byte dataSend = 53;
                _pipe.WriteByte(dataSend);
                _logger.LogInformation($"Server send: {dataSend}");

                //read data
                var dataReceive = _pipe.ReadByte();
                _logger.LogInformation($"Server receive: {dataReceive}");
            }
            //catch exception on broken or disconnected pipe
            catch (IOException exception)
            {
                _logger.LogError("Error: {0}", exception.Message);
            }

            //close pipe
            _pipe.Close();
        }
    }
}