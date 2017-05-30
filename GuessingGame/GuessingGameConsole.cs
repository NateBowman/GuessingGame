//  --------------------------------------------------------------------------------------------------------------------
//     <copyright file="GuessingGameConsole.cs">
//         Copyright (c) Nathan Bowman. All rights reserved.
//         Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace GuessingGame
{
    using System;
    using System.Linq;
    using System.Threading;

    internal class GuessingGameConsole
    {
        private const string CONSOLENAME = "Guess ";

        private static readonly string[] BreakupArray = new[]
                                                            {
                                                                "It's not you, its me.", "You deserve better.", "I think we need some space for a while.",
                                                                "Our lives are going in different directions.", "You're just so much more mature than me.",
                                                                "I can’t do this anymore.", "I’m bringing you down.",
                                                            };

        private static readonly string[] NameArray = new[] { "Dan", "Peter", "Eric", "Paulina", "Amy", "Molly", "Connor", "Juan", "Mick", "Rod", "Nate", "Matt" };

        private static readonly Random RandomGenerator = new Random();

        private static void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        private static void DoThinkingAnimation(string thinkingString, string finishedString)
        {
            var outputPadded = thinkingString.PadRight(thinkingString.Length + 5);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;

            for (var j = 0; j < 5; j++)
            {
                Console.Write($"\r{outputPadded}");
                Console.SetCursorPosition(Console.CursorLeft - 5, Console.CursorTop);
                for (var i = 0; i < 5; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(150);
                }
            }

            Console.WriteLine("\r" + finishedString.PadRight(outputPadded.Length));
            Console.ResetColor();
        }

        private static void Main(string[] args)
        {
            Console.Clear();
            Console.Title = "";
            Console.CursorVisible = false;

            SetConsoleColoursAndType("I am the great and powerful ");
            SetConsoleColoursAndCycle(NameArray, 3000, NameArray.Random() + "!");

            Thread.Sleep(1000);

            SetConsoleColoursAndType("No, wait... that's not right ");
            Thread.Sleep(500);
            SetConsoleColoursAndTypeLn("");

            DoThinkingAnimation("Whats my name again?!?", "OZ!! thats it!! ... i'm the great and ");

            SetConsoleColoursAndTypeLn("*cough*");

            SetConsoleColoursAndTypeLn("*cough*");
            Thread.Sleep(2000);

            while (true)
            {
                Console.Clear();
                var valueToMatch = RandomGenerator.Next(1, 20);

                SetConsoleColoursAndTypeLn("I AM THE GREAT AND POWERFULL OZ!");
                Thread.Sleep(1000);

                SetConsoleColoursAndTypeLn("Im sending you a number between 1 and 20 telepathically");
                Thread.Sleep(1000);

                DoThinkingAnimation("Sending", "Can you tell what it is yet?");
                Console.CursorVisible = true;

                var attemptCap = 0;
                while (true)
                {
                    attemptCap++;
                    var input = Question("Ok, What number am I thinking of?");

                    int iInput;
                    if (int.TryParse(input, out iInput))
                    {
                        Console.CursorVisible = false;
                        if (iInput == 0)
                        {
                            SetConsoleColoursAndTypeLn("Holy! Shhtuff... We did it!!");
                            Thread.Sleep(1000);

                            SetConsoleColoursAndTypeLn($"i sent you {valueToMatch}, and you said {iInput}... BRILLIANT!!");
                            break;
                        }

                        var diference = Math.Abs(iInput - valueToMatch);
                        if ((diference < 1) && (attemptCap < 3))
                        {
                            SetConsoleColoursAndTypeLn("Damn, really close, I'll send it again!");
                            Thread.Sleep(500);

                            SetConsoleColoursAndTypeLn("We've got it this time...");
                        }
                        else if ((diference < 5) && (attemptCap < 3))
                        {
                            SetConsoleColoursAndTypeLn("I sense were a little disconneted");
                            Thread.Sleep(500);

                            SetConsoleColoursAndTypeLn("Try to lean in for a better connection :)");
                            Thread.Sleep(1000);

                            SetConsoleColoursAndTypeLn($"Lets try it again! Maybe a little {(iInput < valueToMatch ? "higher" : "lower")} this time.");
                        }
                        else if (attemptCap < 3)
                        {
                            SetConsoleColoursAndTypeLn("Huh??");
                            Thread.Sleep(500);

                            SetConsoleColoursAndType("Is this thing on?");
                            Console.Beep();
                            SetConsoleColoursAndTypeLn($"We were really off the mark. Its way {(iInput < valueToMatch ? "higher" : "lower")} than that.");
                            Thread.Sleep(500);

                            SetConsoleColoursAndTypeLn("Im sure we can do better.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            SetConsoleColoursAndTypeLn($"i sent you {valueToMatch}, and you said {iInput}!!");
                            Thread.Sleep(250);

                            SetConsoleColoursAndTypeLn("Did you remember to lean in?");
                            Thread.Sleep(500);

                            DoThinkingAnimation("Formulating breakup excuses", "Excuses Generated, Selecting...");
                            SetConsoleColoursAndCycle(BreakupArray, 2000);

                            Thread.Sleep(500);
                            SetConsoleColoursAndTypeLn("ONLY JOKING!");

                            Thread.Sleep(500);
                            Console.CursorVisible = true;
                            break;
                        }
                    }
                }

                if (!YesOrNoQuestion("Would you like to try again? Y/N"))
                {
                    Environment.Exit(0);
                }
            }
        }

        private static string Question(string question)
        {
            ClearInputBuffer();
            SetConsoleColoursAndTypeLn(question);

            return Console.ReadLine();

            ;
        }

        private static void SetConsoleColoursAndCycle(string[] outputStrings, int timeInMs, string finalString = "")
        {
            if (finalString == string.Empty)
            {
                finalString = outputStrings.Random();
            }

            var maxLength = outputStrings.Max(s => s.Length);
            var cursorLeft = Console.CursorLeft;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            while (timeInMs > 0)
            {
                Console.Write(outputStrings.Random().PadRight(maxLength));
                Console.CursorLeft = cursorLeft;
                Thread.Sleep(300);
                timeInMs -= 300;
            }

            Console.WriteLine(finalString.PadRight(maxLength));
            Console.ResetColor();
        }

        private static void SetConsoleColoursAndOutput(string outputString)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(outputString);
            Console.ResetColor();
        }

        private static void SetConsoleColoursAndType(string outputString)
        {
            var charArray = outputString.ToCharArray();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var character in charArray)
            {
                Console.Write(character);
                Thread.Sleep(50);
            }

            Console.ResetColor();
        }

        private static void SetConsoleColoursAndTypeLn(string outputString)
        {
            var charArray = outputString.ToCharArray();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var character in charArray)
            {
                Console.Write(character);
                Thread.Sleep(50);
            }

            Console.WriteLine();
            Console.ResetColor();
        }

        private static bool YesOrNoQuestion(string question)
        {
            ClearInputBuffer();
            SetConsoleColoursAndTypeLn(question);
            var key = ConsoleKey.A;

            while ((key != ConsoleKey.Y) && (key != ConsoleKey.N))
            {
                key = Console.ReadKey().Key;
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            return key == ConsoleKey.Y;
        }
    }
}