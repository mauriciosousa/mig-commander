using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mig_commander;

namespace mig_commander_cli
{


    class Program
    {
        private static string prompt = "mig > ";

        static void Main(string[] args)
        {



            MigCommander migcommander = MigCommander.Instance;
            migcommander.NewCommand += Migcommander_NewCommand;


            bool shouldRun = true;
            do
            {

                Console.Write(prompt);
                string line = Console.ReadLine();

                if (line == "exit")
                {
                    shouldRun = false;
                }
                else
                {
                    string profile = null;
                    string action = null;

                    string[] statements = line.Split(' ');
                    foreach (string statement in statements)
                    {
                        if (statement.Contains("="))
                        {
                            string[] cmd = statement.Split('=');
                            if (cmd.Length == 2)
                            {
                                if (cmd[0] == "profile" || cmd[0] == "p") profile = cmd[1];
                                if (cmd[0] == "action" || cmd[0] == "a") action = cmd[1];
                            }
                        }
                    }

                    if (action != null && profile != null)
                    {
                        mig_commander.Command c = new Command(profile, action);
                        migcommander.Broadcast(c);
                    }
                    else
                    {
                        Console.WriteLine("unknown command");
                    }
                }
            }
            while (shouldRun);
        }

        private static void Migcommander_NewCommand(object sender, MigCommanderReceivedEventArgs e)
        {
            Console.WriteLine("evr  " + e.command.Profile + "  " + e.command.Action);
        }
    }
}
