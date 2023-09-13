
/*
This RPG data streaming assignment was created by Fernando Restituto with 
pixel RPG characters created by Sean Browning.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

#region Assignment Instructions

/*  Hello!  Welcome to your first lab :)

Wax on, wax off.

    The development of saving and loading systems shares much in common with that of networked gameplay development.  
    Both involve developing around data which is packaged and passed into (or gotten from) a stream.  
    Thus, prior to attacking the problems of development for networked games, you will strengthen your abilities to develop solutions using the easier to work with HD saving/loading frameworks.

    Try to understand not just the framework tools, but also, 
    seek to familiarize yourself with how we are able to break data down, pass it into a stream and then rebuild it from another stream.


Lab Part 1

    Begin by exploring the UI elements that you are presented with upon hitting play.
    You can roll a new party, view party stats and hit a save and load button, both of which do nothing.
    You are challenged to create the functions that will save and load the party data which is being displayed on screen for you.

    Below, a SavePartyButtonPressed and a LoadPartyButtonPressed function are provided for you.
    Both are being called by the internal systems when the respective button is hit.
    You must code the save/load functionality.
    Access to Party Character data is provided via demo usage in the save and load functions.

    The PartyCharacter class members are defined as follows.  */

public partial class PartyCharacter
{
    public int classID;

    public int health;
    public int mana;

    public int strength;
    public int agility;
    public int wisdom;

    public LinkedList<int> equipment;

}


/*
    Access to the on screen party data can be achieved via …..

    Once you have loaded party data from the HD, you can have it loaded on screen via …...

    These are the stream reader/writer that I want you to use.
    https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter
    https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader

    Alright, that’s all you need to get started on the first part of this assignment, here are your functions, good luck and journey well!
*/


#endregion

#region Assignment Part 1

static public class AssignmentPart1
{
    static public void SavePartyButtonPressed()
    {
        foreach (PartyCharacter pc in GameContent.partyCharacters)
        {
            Debug.Log("PC class id == " + pc.classID);

            using (StreamWriter sw = new StreamWriter("Assets/SavedChars/SavedChars.txt"))
            {
                foreach (PartyCharacter pc1 in GameContent.partyCharacters)
                {
                    sw.WriteLine(pc1.classID);
                    sw.WriteLine(pc1.health);
                    sw.WriteLine(pc1.mana);
                    sw.WriteLine(pc1.strength);
                    sw.WriteLine(pc1.agility);
                    sw.WriteLine(pc1.wisdom);

                    // Convert the equipment LinkedList<int> to a comma-separated string
                    string equipmentString = string.Join(",", pc1.equipment);
                    sw.WriteLine(equipmentString);
                }
            }
        }
    }

    static public void LoadPartyButtonPressed()
    {
        // Clears Characters for Reset
        GameContent.partyCharacters.Clear();

        using (StreamReader sr = new StreamReader("Assets/SavedChars/SavedChars.txt"))
        {
            while (!sr.EndOfStream)
            {
                string classId = sr.ReadLine();
                string health = sr.ReadLine();
                string mana = sr.ReadLine();
                string strength = sr.ReadLine();
                string agility = sr.ReadLine();
                string wisdom = sr.ReadLine();
                string equipmentLine = sr.ReadLine();

                if (classId != null && health != null && mana != null && strength != null && agility != null && wisdom != null && equipmentLine != null) 
                                                                                       // AAAAAH I FORGOT TO ADD THIS ONE LITTLE THING ^^^^ AND IT TOOK FOREVER TO FIIIND :D
                {
                    Debug.Log(classId);

                    string[] equipmentValues = equipmentLine.Split(',');
                    if (equipmentValues.Length >= 3)
                    {
                        int equipment1 = int.Parse(equipmentValues[0]);
                        int equipment2 = int.Parse(equipmentValues[1]);
                        int equipment3 = int.Parse(equipmentValues[2]);

                        PartyCharacter pc = new PartyCharacter(
                            int.Parse(classId),
                            int.Parse(health),
                            int.Parse(mana),
                            int.Parse(strength),
                            int.Parse(agility),
                            int.Parse(wisdom)
                        );

                        pc.equipment.AddLast(equipment1);
                        pc.equipment.AddLast(equipment2);
                        pc.equipment.AddLast(equipment3);

                        GameContent.partyCharacters.AddLast(pc);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse equipment data.");
                    }
                }
            }
        }

        GameContent.RefreshUI();
    }


}

#endregion


#region Assignment Part 2

////  Before Proceeding!
////  To inform the internal systems that you are proceeding onto the second part of this assignment,
////  change the below value of AssignmentConfiguration.PartOfAssignmentInDevelopment from 1 to 2.
////  This will enable the needed UI/function calls for your to proceed with your assignment.
static public class AssignmentConfiguration
{
    public const int PartOfAssignmentThatIsInDevelopment = 1;
}

///*

//In this part of the assignment you are challenged to expand on the functionality that you have already created.  
//    You are being challenged to save, load and manage multiple parties.
//    You are being challenged to identify each party via a string name (a member of the Party class).

//To aid you in this challenge, the UI has been altered.  

//    The load button has been replaced with a drop down list.  
//    When this load party drop down list is changed, LoadPartyDropDownChanged(string selectedName) will be called.  
//    When this drop down is created, it will be populated with the return value of GetListOfPartyNames().

//    GameStart() is called when the program starts.

//    For quality of life, a new SavePartyButtonPressed() has been provided to you below.

//    An new/delete button has been added, you will also find below NewPartyButtonPressed() and DeletePartyButtonPressed()

//Again, you are being challenged to develop the ability to save and load multiple parties.
//    This challenge is different from the previous.
//    In the above challenge, what you had to develop was much more directly named.
//    With this challenge however, there is a much more predicate process required.
//    Let me ask you,
//        What do you need to program to produce the saving, loading and management of multiple parties?
//        What are the variables that you will need to declare?
//        What are the things that you will need to do?  
//    So much of development is just breaking problems down into smaller parts.
//    Take the time to name each part of what you will create and then, do it.

//Good luck, journey well.

//*/

static public class AssignmentPart2
{

    static List<string> listOfPartyNames;

    static public void GameStart()
    {
        listOfPartyNames = new List<string>();
        listOfPartyNames.Add("sample 1");
        listOfPartyNames.Add("sample 2");
        listOfPartyNames.Add("sample 3");

        GameContent.RefreshUI();
    }

    static public List<string> GetListOfPartyNames()
    {
        return listOfPartyNames;
    }

    static public void LoadPartyDropDownChanged(string selectedName)
    {
        GameContent.RefreshUI();
    }

    static public void SavePartyButtonPressed()
    {
        GameContent.RefreshUI();
    }

    static public void DeletePartyButtonPressed()
    {
        GameContent.RefreshUI();
    }

}

#endregion


