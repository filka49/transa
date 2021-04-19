using System;
using System.Security.Cryptography;
using System.Text;

namespace RPSLS
{
    class Program
    {
        static int Main(string[] arg)
        {
            string[] mas = arg;
            string userInput = "";
            string tmp_str="";
            string hmac_code = "";
            string hmac_key = "";
            int count = 0;
            int win_c = 0;
            int win_h = 0;
            for (int i=0; i<mas.Length; i++)
            {
                tmp_str += mas[i];
            }
            hmac_key = GetPass();
            if (checkArgumente(mas))
            {
                for (int i = 0; i < mas.Length; i++)
                {
                    userMenuAndInput();
                    Console.Clear();
                    Console.WriteLine(logicController(userInput, mas) + "\nHMAC KEY: " + hmac_key);
                }
            }

            void userMenuAndInput()
            {
                menu();
                userInput = Console.ReadLine();
                while (checkUserInput(userInput) != true)
                {
                    menu();
                    userInput = Console.ReadLine();
                }
            }


            void menu()
            {
                
                hmac_code = HMACHASH(tmp_str, hmac_key);
                Console.WriteLine("HMAC: " + hmac_code);
                Console.WriteLine("Available moves:\n1-rock \n2-paper \n3-scissors\n4-lizard \n5-spock\n0-exit");
                Console.Write("\nEnter your move: ");
            }

            bool checkUserInput(string uI)
            {
                if (uI.Length == 1)
                {
                    switch (uI[0])
                    {
                        case '0': return true;
                        case '1': return true;
                        case '2': return true;
                        case '3': return true;
                        case '4': return true;
                        case '5': return true;
                        default: return false;
                    }
                }
                return false;
            }

            bool checkArgumente(string[] arr)
            {
                if (arr.Length >= 3 && arr.Length % 2 != 0)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        switch (arr[i])
                        {
                            case "rock": break;
                            case "paper": break;
                            case "scissors": break;
                            case "lizard": break;
                            case "spock": break;
                            default:
                                Console.WriteLine("Ошибка строки " + arr[i]);
                                return false;
                        }
                        // rock paper scissors lizard spock
                    }
                }
                else
                {
                    Console.WriteLine("Invalid entry, enter an odd number of arguments (at least 3). For example: rock paper scissors lizard spock");
                    return false;
                }
                return true;
            }

            string logicController(string Input, string[] myarg)
            {
                string figure = "";

                switch (Input)
                {
                    case "1":
                        figure = "rock";
                        break;
                    case "2":
                        figure = "paper";
                        break;
                    case "3":
                        figure = "scissors";
                        break;
                    case "4":
                        figure = "lizard";
                        break;
                    case "5":
                        figure = "spock";
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                }

                if (figure == "rock")
                {
                    switch (myarg[count])
                    {
                        case "rock":
                            return "Draw";
                        case "paper":
                            win_c++; 
                            return "Lose";
                        case "scissors":
                            win_h++;
                            return "Win";
                        case "lizard":
                            return "Win";
                        case "spock":
                            return "Lose";
                    }
                }

                if (figure == "paper")
                {
                    switch (myarg[count])
                    {
                        case "rock":
                            return "Win";
                        case "paper":
                            return "Draw";
                        case "scissors":
                            return "Lose";
                        case "lizard":
                            return "Lose";
                        case "spock":
                            return "Win";
                    }
                }

                if (figure == "scissors")
                {
                    switch (myarg[count])
                    {
                        case "rock":
                            return "Lose";
                        case "paper":
                            return "Win";
                        case "scissors":
                            return "Draw";
                        case "lizard":
                            return "Win";
                        case "spock":
                            return "Lose";
                    }
                }

                if (figure == "lizard")
                {
                    switch (myarg[count])
                    {
                        case "rock":
                            return "Lose";
                        case "paper":
                            return "Win";
                        case "scissors":
                            return "Lose";
                        case "lizard":
                            return "Draw";
                        case "spock":
                            return "Win";
                    }
                }

                if (figure == "spock")
                {
                    switch (myarg[count])
                    {
                        case "rock":
                            return "Win";
                        case "paper":
                            return "Lose";
                        case "scissors":
                            return "Win";
                        case "lizard":
                            return "Lose";
                        case "spock":
                            return "Draw";
                    }
                }

                count++;
                return "Win";
            }
            string HMACHASH(string str, string key)
            {
                byte[] bkey = Encoding.Default.GetBytes(key);
                using (var hmac = new HMACSHA256(bkey))
                {
                    byte[] bstr = Encoding.Default.GetBytes(str);
                    var bhash = hmac.ComputeHash(bstr);
                    return BitConverter.ToString(bhash).Replace("-", string.Empty);
                }
            }
            string GetPass()
            {
                int[] arr = new int[16];
                Random rnd = new Random();
                string Password = "";

                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = rnd.Next(48, 90);
                    Password += (char)arr[i];
                }
                return Password;
            }
            return 0;
        }
    }
}