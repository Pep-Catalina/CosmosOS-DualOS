using System;

namespace DualOS
{
    public class NetworkManager
    {
        // Indica si la red ha sido configurada
        private bool networkConfigured = false;
        // Guarda la máscara de subred configurada
        private string currentSubnetMask = null;
        // Guarda la puerta de enlace configurada
        private string currentGateway = null;

        public bool IsNetworkConfigured()
        {
            return networkConfigured;
        }

        public string ConfigureStaticIp(string ip, string mask, string gateway)
        {
            try
            {
                // Validar formato básico de IP
                if (!IsValidIpFormat(ip) || !IsValidIpFormat(mask) || !IsValidIpFormat(gateway))
                {
                    return "Error: Invalid IP format. Use: xxx.xxx.xxx.xxx";
                }

                // Almacenar los valores de configuración
                currentSubnetMask = mask;
                currentGateway = gateway;
                networkConfigured = true;

                return "✓ Static IP configured successfully!\n" +
                       "IP Address:   " + ip + "\n" +
                       "Subnet Mask:  " + mask + "\n" +
                       "Gateway:      " + gateway;
            }
            catch (Exception ex)
            {
                networkConfigured = false;
                return "Error configuring static IP: " + ex.Message;
            }
        }

        public string GetCurrentIp()
        {
            if (!networkConfigured)
            {
                return "⚠️  Network not configured.\n" +
                       "Use: netconfig <ip> <mask> <gateway>";
            }

            if (currentSubnetMask == null || currentGateway == null)
            {
                return "⚠️  Network configuration incomplete!";
            }

            return "✓ Network is configured\n" +
                   "Subnet Mask:  " + currentSubnetMask + "\n" +
                   "Gateway:      " + currentGateway;
        }

        private bool IsValidIpFormat(string ip)
        {
            try
            {
                string[] parts = ip.Split('.');
                if (parts.Length != 4)
                    return false;

                foreach (string part in parts)
                {
                    int value = int.Parse(part);
                    if (value < 0 || value > 255)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
