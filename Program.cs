using System;
using System.Collections.Generic;
using System.Threading;



public class Program
{
    static int playerX = 5; 
    static int playerY = 5; 

    static int opponentX = 40; 
    static int opponentY = 5;  

    
    static void Main(string[] args)
    {
        Fighter Ryu = new Fighter(1, "Ryu", 160, 20, "To live is to fight, to fight is to live!");
        Fighter Sagat = new Fighter(2, "Sagat", 190, 16, "Challenge me anytime! I'll always be here!");
        Fighter Bison = new Fighter(3, "Bison", 150, 22, "Even a baby could defeat you! Those without strength are disgusting!!");
        Fighter Chun_Li = new Fighter(4, "Chun-Li", 130, 30, "I'm the strongest woman in the world!");
        Fighter Guile = new Fighter(5, "Guile", 175, 18, "I came. I saw. I destroyed! Power. Skill. Strength. Period.");
        Fighter Cammy = new Fighter(6, "Cammy", 140, 26, "Your missing teeth will remind you of my victory!");

        List<Fighter> fighters = new List<Fighter>
        {
            Ryu,
            Sagat,
            Bison,
            Chun_Li,
            Guile,
            Cammy
        };



        Console.WriteLine("Chceš spustiť hru?\n[yes/...]");
        var start = Console.ReadLine() == "yes" ? "Spúšťame hru!" : "Je mi ľúto, zadal si zlú hodnotu";

        

        Console.WriteLine(start);

        if(start != "yes"){
            return;
        }

        foreach (var fighter in fighters)
        {
            Console.WriteLine($"Fighter {fighter.ID}: {fighter.Name}");
        }

        int selectedId;
        Fighter selectedFighter = null;

    
        Console.WriteLine("\nZadaj ID bojovníka:");
        var input = Console.ReadLine();

        if (int.TryParse(input, out selectedId))
        {
            selectedFighter = fighters.Find(f => f.ID == selectedId);

            if (selectedFighter != null)
            {
                Console.WriteLine($"\nVybral si si bojovníka: {selectedFighter.Name}");
            }
            else
            {
                Console.WriteLine("Bojovník s týmto ID neexistuje.");
                return; 
            }
        }
        else
        {
            Console.WriteLine("Neplatný vstup. Zadaj prosím číslo ID.");
            return; 
        }
    

        Random rnd = new Random();
        Fighter opponentFighter;
        do
        {
            opponentFighter = fighters[rnd.Next(fighters.Count)];
        } while (opponentFighter == selectedFighter);

        Console.WriteLine($"Tvoj súper je: {opponentFighter.Name}");

        Console.Clear();
        Console.WriteLine($"{selectedFighter.Name} (HP: {selectedFighter.HP})\t\t\t\t\t{opponentFighter.Name} (HP: {opponentFighter.HP})\n");

        ConsoleKeyInfo keyInfo;
        bool gameRunning = true;

        while (gameRunning && selectedFighter.HP > 0 && opponentFighter.HP > 0)
        {
            Console.Clear();

            Console.WriteLine($"{selectedFighter.Name} (HP: {selectedFighter.HP})\t\t\t\t\t{opponentFighter.Name} (HP: {opponentFighter.HP})\n");

            DrawStickman(playerX, playerY);

            MoveOpponent(rnd);

            DrawStickman(opponentX, opponentY);

            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.A:
                        playerX--; 
                        break;
                    case ConsoleKey.D:
                        playerX++;
                        break;
                    case ConsoleKey.Spacebar:
                        selectedFighter.Attack(opponentFighter);
                        Console.WriteLine();
                        Console.WriteLine($"{selectedFighter.Name} útočí na {opponentFighter.Name}!");
                        break;
                    case ConsoleKey.Escape:
                        gameRunning = false; 
                        break;
                }
            }

            if (rnd.Next(2) == 0)
            {
                opponentFighter.Attack(selectedFighter);
                Console.WriteLine($"{opponentFighter.Name} útočí na {selectedFighter.Name}!");
            }

            if (selectedFighter.HP <= 0)
            {
                selectedFighter.HP = 0; 
                Console.WriteLine();
                Console.WriteLine("DEFEAT");
                Console.WriteLine();
                Console.WriteLine($"{selectedFighter.Name} bol porazený!");
                Console.WriteLine($"{opponentFighter.Name}: {opponentFighter.Quote}");
                gameRunning = false;
            }
            else if (opponentFighter.HP <= 0)
            {
                opponentFighter.HP = 0; 
                Console.WriteLine();
                Console.WriteLine("VICTORY");
                Console.WriteLine();
                Console.WriteLine($"{opponentFighter.Name} bol porazený!");
                Console.WriteLine($"{opponentFighter.Name}: {opponentFighter.Quote}");
                gameRunning = false;
            }

            Thread.Sleep(200);
        }

        Console.WriteLine("Hra skončila.");
    }

    static void DrawStickman(int x, int y)
    {
        string[] stickman = new string[]
        {
            "  O  ",
            " /|\\ ",
            "/ | \\",
            " / \\ ",
            "/   \\"
        };

        for (int i = 0; i < stickman.Length; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(stickman[i]);
        }
    }

    

    static void MoveOpponent(Random random)
    {
        int direction = random.Next(2); 

        switch (direction)
        {
            case 1:
                opponentX--; 
                break;
            case 2:
                opponentX++; 
                break;
        }
    }
}
