using Cosmos.HAL;
using Cosmos.System.Network;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using System;

namespace DualOS
{
    public class NetworkManager
    {
        // Indica si la xarxa ja ha estat configurada
        private bool networkConfigured = false;
        // Guarda la màscara de subxarxa configurada
        private Address currentSubnetMask = null;
        // Guarda la porta d'enllaç configurada
        private Address currentGateway = null;

        public string ConfigureStaticIp(string ip, string mask, string gateway)
        {
            try
            {
                // Busca la targeta de xarxa anomenada eth0
                NetworkDevice nic = NetworkDevice.GetDeviceByName("eth0");

                // Si no es troba cap targeta de xarxa, retorna un error
                if (nic == null)
                {
                    return "Network device eth0 not found.";
                }

                // Converteix les dades rebudes en format Address
                Address ipAddress = ParseAddress(ip);
                Address subnetMask = ParseAddress(mask);
                Address gatewayAddress = ParseAddress(gateway);

                IPConfig.Enable(nic, ipAddress, subnetMask, gatewayAddress);

                // Almacenar los valores de configuración
                currentSubnetMask = subnetMask;
                currentGateway = gatewayAddress;
                networkConfigured = true;

                return "Static IP configured: " + NetworkConfiguration.CurrentAddress.ToString();
            }
            catch (Exception ex)
            {
                return "Error configuring static IP: " + ex.Message;
            }
        }

        public string GetCurrentIp()
        {
            try
            {
                 // Obté la IP actual configurada al sistema
                var currentIp = NetworkConfiguration.CurrentAddress;

                if (currentIp == null || currentIp.ToString() == "0.0.0.0")
                {
                    return "⚠️  WARNING: No IP address assigned!\n" +
                           "Network interface is not configured.\n\n" +
                           "To configure network use:\n" +
                           "  netconfig <ip> <subnet_mask> <gateway>\n\n" +
                           "Example:\n" +
                           "  netconfig 192.168.1.100 255.255.255.0 192.168.1.1";
                }
                
                // Crea el text amb la informació de la IP actual
                string info = "IP Address:   " + currentIp.ToString() + "\n";

                if (currentSubnetMask != null)
                {
                    info += "Subnet Mask:  " + currentSubnetMask.ToString() + "\n";
                }

                if (currentGateway != null)
                {
                    info += "Gateway:      " + currentGateway.ToString();
                }
                // Retorna tota la informació de xarxa
                return info;
            }
            catch (Exception ex)
            {
                return "❌ Error reading current IP: " + ex.Message;
            }
        }

        private Address ParseAddress(string value)
        {
            string[] parts = value.Split('.');

            if (parts.Length != 4)
            {
                throw new Exception("Invalid IPv4 format.");
            }

            return new Address(
                byte.Parse(parts[0]),
                byte.Parse(parts[1]),
                byte.Parse(parts[2]),
                byte.Parse(parts[3])
            );
        }
    }
}
