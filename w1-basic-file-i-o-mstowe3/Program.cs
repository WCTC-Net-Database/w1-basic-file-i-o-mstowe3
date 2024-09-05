using System; 
using System.IO;
using System.Runtime.InteropServices;



class Program
{
    static string[] lines;
    static void Main()
    {
        string filePath="input.csv";
        string[] lines = File.ReadAllLines(filePath);

        //Menu
        while (true)
            {

            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter The Number Of Your choice And Press Enter: ");
            string mainMenuChoice = Console.ReadLine();
            Console.WriteLine();

            switch (mainMenuChoice)
                {
                    case "1":
                        Console.WriteLine();
                        DisplayCharacters(lines);
                    break;
                    
                    case "2":
                        Console.WriteLine();
                        AddCharacter(ref lines);
                    break;
                    
                    case "3":
                        Console.WriteLine();
                        LevelCharacter(lines);
                    break;
                    
                    case "4":
                    return;
                    
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid choice. Please Select the Number of your Choice and Press Enter.");
                    break;

                 }
            }
    }
    static void DisplayCharacters(string[] lines)
    {
        //Variables
        String characterID;
        string characterName;
        string characterClass;
        string characterLevel;
        string characterHP;
        string characterEquipment;


    
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] quotedNameArray = line.Split(',');
            var quotedNameTest = quotedNameArray[1];


            // Quoted Name
            if (quotedNameTest.StartsWith('"'))
            {
                // Parse line into individual attributes

                String[] CharacterFields = line.Split(',');
                characterID = CharacterFields[0];
                var characterLastName = CharacterFields[1].Trim('"');
                var characterFirstName = CharacterFields[2].Trim('"');
                characterName = ($"{characterFirstName} {characterLastName}");
                characterClass = CharacterFields[3];
                characterLevel = CharacterFields[4];
                characterHP = CharacterFields[5];
                characterEquipment = CharacterFields[6].Replace("|", ", ");
            }
            else
            {
                    // Remove quotes
                    // Replace | in equipment with ',' and space

                String[] CharacterFields = line.Split(',');
                characterID = CharacterFields[0];
                characterName = CharacterFields[1].Trim('"');
                characterClass = CharacterFields[2];
                characterLevel = CharacterFields[3];
                characterHP = CharacterFields[4];
                characterEquipment = CharacterFields[5].Replace("|", ", ");
            }

            //  Display character attributes in one line
            Console.WriteLine($"ID= {characterID}; Name= {characterName}; Class= {characterClass}; Level= {characterLevel}; Hit Points= {characterHP}; Equipment= {characterEquipment}");
            Console.WriteLine();

         
        }
    }

    // Method to Add a Character
    static void AddCharacter(ref string[] lines)
    {
        // Implement logic to add a new character

    
        var lastCharacterID = lines.Length - 1;
      

        // Prompt 
        Console.WriteLine();
        Console.WriteLine("Enter Your Character's Name and Press Enter: ");
        var nameInput = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Enter Your Character's Class and Press Enter: ");
        var classInput = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Enter Your Character's Level and Press Enter: ");
        var levelInput = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Enter Your Character's Hit Power and Press Enter: ");
        var hitPowerInput = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine("Enter Your Character's First Piece of Equipment and Press Enter: ");
        var firstEquipment = Console.ReadLine();

        var equipmentList = new System.Text.StringBuilder();
        equipmentList.Append(firstEquipment);



        // Menu if player wants to add equipment
        
        Console.WriteLine();
        Console.WriteLine($"1. Add piece of equipment to character {nameInput}");
        Console.WriteLine($"2. Equipment added. Build character {nameInput}");
        Console.WriteLine($"3. Do not build character {nameInput}. Reset and return to Main Menu");
        Console.Write("Enter the number of your choice and press enter: ");
        string buildCharacterMenuChoice = Console.ReadLine();
        
        
        

        switch (buildCharacterMenuChoice)
        {
            case "1":
                Console.WriteLine();
                Console.WriteLine("Enter Your Character's Next Piece of Equipment and Press Enter (or press Enter to finish): ");
                var nextEquipment = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nextEquipment))
                {
                    break; 
                }

                
                equipmentList.Append($", {nextEquipment}");
                break;
           
            case "2":
                Console.WriteLine(); 
            // Build Character ID
            var characterID = DateTime.Now.ToString("yyyyMMddHHmmss");

          
            var characterData = $"{characterID},{nameInput},{classInput},{levelInput},{hitPowerInput},{equipmentList.ToString()}";

            
            AppendToCsv("input.csv", characterData);

            // Display character attributes
            Console.WriteLine($"Character ID: {characterID}");
            Console.WriteLine($"Name: {nameInput}");
            Console.WriteLine($"Class: {classInput}");
            Console.WriteLine($"Level: {levelInput}");
            Console.WriteLine($"Hit Power: {hitPowerInput}");
            Console.WriteLine($"Equipment: {equipmentList.ToString()}");
            Console.WriteLine("Character has been added to Party.");
                break;
            
            case "3":
                return;
            default:
                Console.WriteLine();
                Console.WriteLine("Invalid choice. Please Select the Number of your choice and Press Enter.");
                break;
        }
    }
     static void LevelCharacter(string[] lines)
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        // Find the one to level up
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

          
            
            if (line.Contains(nameToLevelUp))
            {

               
                string[] fields = line.Split(",");

                string heroClass = fields[^4];
                int level = Convert.ToInt16(fields[^3]);
                int hitPoints = Convert.ToInt16(fields[^2]);
                string equipment = fields[^1];

                string name;
                int commaIndex;

                if (line.StartsWith("\""))
                {
                
                    
                    commaIndex = line.IndexOf(',');
                    name = line.Substring(0, commaIndex);
                    int pos = name.Length + 1;

                    var line2 = line.Substring(pos);

                    int commaIndex2 = line2.IndexOf(',');

                    int nameEndsIndex = pos + commaIndex2;

                    // TODO: Remove quotes from the name if present and parse the name
                    // name = ...
                    name = line.Substring(0,nameEndsIndex);
                    line = line.Substring(nameEndsIndex);
                    name = name.Replace("\"","");
                }
                else
                {
                    // TODO: Name is not quoted, so store the name up to the first comma
                    commaIndex = line.IndexOf(',');
                    name = line.Substring(0,commaIndex);
                    line = line.Substring(commaIndex);
                }

                
                level++;
                Console.WriteLine($"Character {name} leveled up to level {level}!");

                if (name.Contains(","))
                {
                    name = $"\"{name}\"";
                }
                lines[i] = $"{name},{heroClass},{level},{hitPoints},{equipment}";

                break;
            }
        }
      
      }
      static void AppendToCsv(string filePath, string data)
{
    try
    {
        using (StreamWriter sw = new StreamWriter(filePath, append: true))
        {
            sw.WriteLine(data);
            Console.WriteLine("Data appended: " + data); // Debug outp
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while writing to the CSV file: {ex.Message}");
    }
}
}