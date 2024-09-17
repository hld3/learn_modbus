using System.Net.Sockets;
using NModbus;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string ipAddress = "ADD SERVER IP";
            int port = 502;

            TcpClient client = new TcpClient(ipAddress, port);

            var factory = new ModbusFactory();
            var modbusMaster = factory.CreateMaster(client);
            byte slaveId = 1;
            ushort startAddress = 0;
            ushort numberOfPoints = 10;
            ushort[] holdingRegisters = modbusMaster.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);
            Console.WriteLine("Holding Registers:");
            for (int i = 0; i < holdingRegisters.Length; i++)
            {
                Console.WriteLine($"Address {startAddress + i}: {holdingRegisters[i]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
