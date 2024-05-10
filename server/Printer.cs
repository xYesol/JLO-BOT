﻿using System;

namespace JLO_BOT
{
    public static class Printer
    {
        public static void PrintWelcomeMessage()
        {
            string[] stringArray = new string[11];
            stringArray[0] = "+-------------------------------------------------------------------------------------------------------------------------------+";
            stringArray[1] = "|______/\\\\\\\\\\\\\\\\\\\\\\__/\\\\\\___________________/\\\\\\\\\\_____________________/\\\\\\\\\\\\\\\\\\\\\\\\\\_________/\\\\\\\\\\_______/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\_    |";
            stringArray[2] = "| _____\\/////\\\\\\///__\\/\\\\\\_________________/\\\\\\///\\\\\\__________________\\/\\\\\\/////////\\\\\\_____/\\\\\\///\\\\\\____\\///////\\\\\\/////__   |";
            stringArray[3] = "|  _________\\/\\\\\\_____\\/\\\\\\_______________/\\\\\\/__\\///\\\\\\________________\\/\\\\\\_______\\/\\\\\\___/\\\\\\/__\\///\\\\\\________\\/\\\\\\_______  |";
            stringArray[4] = "|   _________\\/\\\\\\_____\\/\\\\\\______________/\\\\\\______\\//\\\\\\__/\\\\\\\\\\\\\\\\\\\\\\_\\/\\\\\\\\\\\\\\\\\\\\\\\\\\\\___/\\\\\\______\\//\\\\\\_______\\/\\\\\\_______ |";
            stringArray[5] = "|    _________\\/\\\\\\_____\\/\\\\\\_____________\\/\\\\\\_______\\/\\\\\\_\\///////////__\\/\\\\\\/////////\\\\\\_\\/\\\\\\_______\\/\\\\\\_______\\/\\\\\\_______|";
            stringArray[6] = "|     _________\\/\\\\\\_____\\/\\\\\\_____________\\//\\\\\\______/\\\\\\________________\\/\\\\\\_______\\/\\\\\\_\\//\\\\\\______/\\\\\\________\\/\\\\\\______|";
            stringArray[7] = "|      __/\\\\\\___\\/\\\\\\_____\\/\\\\\\______________\\///\\\\\\__/\\\\\\__________________\\/\\\\\\_______\\/\\\\\\__\\///\\\\\\__/\\\\\\__________\\/\\\\\\_____|";
            stringArray[8] = "|       _\\//\\\\\\\\\\\\\\\\\\______\\/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\____\\///\\\\\\\\\\/___________________\\/\\\\\\\\\\\\\\\\\\\\\\\\\\/_____\\///\\\\\\\\\\/___________\\/\\\\\\____|";
            stringArray[9] = "|        __\\/////////_______\\///////////////_______\\/////_____________________\\/////////////_________\\/////_____________\\///____|";
            stringArray[10] = "+-------------------------------------------------------------------------------------------------------------------------------+";
            /*
            ______/\\\\\\\\\\\__/\\\___________________/\\\\\_____________________/\\\\\\\\\\\\\_________/\\\\\_______/\\\\\\\\\\\\\\\_        
             _____\/////\\\///__\/\\\_________________/\\\///\\\__________________\/\\\/////////\\\_____/\\\///\\\____\///////\\\/////__       
              _________\/\\\_____\/\\\_______________/\\\/__\///\\\________________\/\\\_______\/\\\___/\\\/__\///\\\________\/\\\_______      
               _________\/\\\_____\/\\\______________/\\\______\//\\\__/\\\\\\\\\\\_\/\\\\\\\\\\\\\\___/\\\______\//\\\_______\/\\\_______     
                _________\/\\\_____\/\\\_____________\/\\\_______\/\\\_\///////////__\/\\\/////////\\\_\/\\\_______\/\\\_______\/\\\_______    
                 _________\/\\\_____\/\\\_____________\//\\\______/\\\________________\/\\\_______\/\\\_\//\\\______/\\\________\/\\\_______   
                  __/\\\___\/\\\_____\/\\\______________\///\\\__/\\\__________________\/\\\_______\/\\\__\///\\\__/\\\__________\/\\\_______  
                   _\//\\\\\\\\\______\/\\\\\\\\\\\\\\\____\///\\\\\/___________________\/\\\\\\\\\\\\\/_____\///\\\\\/___________\/\\\_______ 
                    __\/////////_______\///////////////_______\/////_____________________\/////////////_________\/////_____________\///________
             */

            Console.SetWindowSize(150, 30);
            foreach (string str in stringArray)
            {
                foreach (char c in str)
                {
                    if (c != '_')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(c);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
