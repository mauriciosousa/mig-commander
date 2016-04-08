using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mig_commander
{
    public class Command
    {
        private static string key = "o daniel gosta de comer arroz com feijao";

        private string _profile;
        private string _action;
        private string _timestamp;

        public string Profile
        {
            get
            {
                return _profile;
            }
        }

        public string Action
        {
            get
            {
                return _action;
            }
        }

        public string Timestamp
        {
            get
            {
                return _timestamp;
            }
        }

        public Command(string profile = "Debug", string action = "log")
        {
            _profile = profile;
            _action = action;
            _timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public Command(string encPDU)
        {
            Console.WriteLine(encPDU);
            Console.WriteLine(DataEncryptor.Decrypt(encPDU, key));
            string actualPDU = DataEncryptor.Decrypt(encPDU, key);

            string [] statements = actualPDU.Split(' ');
            foreach (string statement in statements)
            {
                if (statement.Contains("="))
                {
                    string[] cmd = statement.Split('=');
                    if (cmd.Length == 2)
                    {
                        if (cmd[0] == "profile" || cmd[0] == "p") _profile = cmd[1];
                        if (cmd[0] == "action" || cmd[0] == "a") _action = cmd[1];
                        if (cmd[0] == "timestamp" || cmd[0] == "t") _timestamp = cmd[1];
                    }
                }
            }

            Console.WriteLine(Timestamp + " " + Profile + " " + Action);
        }

        public string getPDU()
        {
            string pdu = "t=" + Timestamp + " p=" + Profile + " a=" + Action;

            return DataEncryptor.Encrypt(pdu, key);
        }
    }
}
