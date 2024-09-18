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
            bool[] coils = modbusMaster.ReadCoils(slaveId, 7, 10);
            modbusMaster.WriteSingleCoil(1, 8, false);
            modbusMaster.WriteMultipleRegisters(1, 0, new ushort[] {10, 123});
            Console.WriteLine("Holding Registers:");
            for (int i = 0; i < coils.Length; i++)
            {
                Console.WriteLine($"Address {startAddress + i}: {coils[i]}");
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
