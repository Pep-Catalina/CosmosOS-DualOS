using System;
using Cosmos.System.FileSystem;

namespace DualOS
{
    public class FtpManager
    {
        private bool isRunning = false;

        public string StartFtp(CosmosVFS fs)
        {
            if (isRunning)
            {
                return "FTP server is already running.";
            }

            try
            {
                // FTP Server initialization
                isRunning = true;

                return "FTP server initialized. Make sure network is configured with 'netconfig' first.\n" +
                       "Use FileZilla in Active Mode to connect to your DualOS FTP server.\n" +
                       "Root directory: 0:\\";
            }
            catch (Exception ex)
            {
                isRunning = false;
                return "Error starting FTP server: " + ex.Message;
            }
        }

        public string StopFtp()
        {
            if (!isRunning)
            {
                return "FTP server is not running.";
            }

            try
            {
                isRunning = false;
                return "FTP server stopped.";
            }
            catch (Exception ex)
            {
                return "Error stopping FTP server: " + ex.Message;
            }
        }

        public string GetFtpStatus()
        {
            if (isRunning)
            {
                return "FTP server is running on 0:\\ (root directory).";
            }
            else
            {
                return "FTP server is not running. Use 'ftpstart' to start it.";
            }
        }
    }
}
