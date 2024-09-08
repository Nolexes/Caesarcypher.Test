using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {


        string command = ""; // string command initialisiert außerhalb des loops damit von überall zugänglich 
        int encryptionint; // int für den shift im cypher außerhalb des loops definiert
        string input = ""; // string für den textinput außerhalb des loops definiert 
        
        while (true)    // Loop für das Commando des Switchcases wird nur bei validem Input unterbrochen 
        {
            Console.WriteLine("do you want to 'encrypt' or 'decrypt' a text?:");
            command = Console.ReadLine();

            if (command == "encrypt" || command == "decrypt")
            {
                break; //unterbricht den Loop bei richtiger eingabe
            }
            else
            {
                Console.WriteLine("command has to be 'encrypt' or 'decrypt'");
            }

        }

        switch (command)
        {
            case "encrypt": // In diesem Fall wird die Nachricht verschlüsselt 
                while (true)
                {
                    Console.WriteLine("Enter the encryption key(1-25): ");
                    string encryptionkey = Console.ReadLine();
                    if (int.TryParse(encryptionkey, out encryptionint) && encryptionint <= 25 && encryptionint >= 1) // Loop der überprüft ob die Eingabe eine int ist und im angegebenen Bereich
                    {
                        break; //beendet den while loop falls die Eingabe korrekt war.
                    }
                    else
                    {
                        Console.WriteLine("Invalid Key. The encryption key must be a number between 1 and 25."); // Ansonsten wird solange wieder zur eingabe aufgefordert mit den Anweisungen bis eine valide Zahl eingegeben wurde
                    }

                }
                while (true)
                {
                    Console.WriteLine("Enter the text you want to encrypt: ");
                    input = Console.ReadLine();

                    if (Regex.IsMatch(input, @"^[a-zA-Z\s]+$")) //Mit dem Regex wird sichergestellt das die Eingabe nur aus einem Zeichenpool von a-z, A-Z und \s (Leerzeichen) besteht ansonsten wieder unendlicher Loop bis zur richtigen Eingabe
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Input can only contain valid letters from a-z and A-Z");
                    }
                }

                string output = CypherEncryption(input, encryptionint);     //Gibt den Verschlüsselten Text aus 
                Console.WriteLine("The encrypted text is: " + output);

                static string CypherEncryption(string input, int shift) //Funktion zum Verschlüsseln des Textes nimmt einen String, und eine Int als Input (hierfür verwenden wir oben (input, encryptionint)
                {
                    char[] buffer = input.ToCharArray(); //Erstellt ein Buffer Array aus den einzelnen Chars des Strings

                    for (int i = 0; i < buffer.Length; i++) //Loopt einmal durch das gesamte Array
                    {
                        char letter = buffer[i];
                        if (char.IsUpper(letter))
                        {
                            letter = (char)((letter + shift - 'A') % 26 + 'A');  //Formel zur Umrechnung falls Großbuchstabe
                        }
                        else if (char.IsLower(letter))
                        {
                            letter = (char)(((letter + shift - 'a') % 26) + 'a'); //Formel zur Umrechnung falls Kleinbuchstabe
                        }

                        buffer[i] = letter; //Überschreibt den alten Char mit dem Neuen 
                    }
                    return new string(buffer); // gibt das komplette Array wieder als String aus 
                }

                break;

            case "decrypt":  // in diesem Fall wird die Nachricht entschlüsselt
                while (true)
                {
                    Console.WriteLine("Enter the encryption key(1-25): ");
                    string encryptionkey = Console.ReadLine();
                    if (int.TryParse(encryptionkey, out encryptionint) && encryptionint <= 25 && encryptionint >= 1)        //Gleiche Abfrage wie beim Verschlüsseln
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Key. The encryption key must be a number between 1 and 25.");
                    }

                }
                while (true)
                {
                    Console.WriteLine("Enter the text you want to decrypt: ");      //Auch wieder das Gleiche wie oben
                    input = Console.ReadLine();

                    if (Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Input can only contain valid letters from a-z and A-Z");
                    }
                }

                string outputde = CypherDecryption(input, encryptionint);       //Funktion zum Entschlüsseln des Textes
                Console.WriteLine("The decrypted text is: " + outputde);

                static string CypherDecryption(string input, int shift)
                {
                    char[] buffer = input.ToCharArray();

                    for (int i = 0; i < buffer.Length; i++)
                    {
                        char letter = buffer[i];

                        
                        if (char.IsUpper(letter))
                        {
                            letter = (char)((letter - shift - 'A' + 26) % 26 + 'A');  //Beim entschlüsseln müssen wir +26 addieren damit durch das Abziehen des Keys keine negativen Zahlen entstehen durch %26 bleibt das Ergebnis richtig
                        }
                        
                        else if (char.IsLower(letter))
                        {
                            letter = (char)((letter - shift - 'a' + 26) % 26 + 'a');
                        }
                        
                        buffer[i] = letter;
                    }
                    return new string(buffer);
                }

                break;
        }
            
        }
    
}