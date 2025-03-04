// Generates random magical items for TTRPGs.

using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using libAnnaC;

string ProgramName = "itemGen";
string ProgramVersion = "0.4.x";
string[] ItemTypes = { "sword", "axe", "wand", "shield", "tome", "armor", "ring", "amulet", "bracers", "boots", "sash", "dagger", "bow", "mace", "robe", "cloak" };
string AdjFileName = "english_adjectives.txt";
string VerbFileName = "english_verbs.txt";
string PastVerbFileName = "english_verbs_past.txt";
string IngVerbFileName = "english_verbs_ing.txt";
string NounFileName = "english_nouns.txt";
string[] VerbTypes = { "basic", "past", "ing" };
string[] Combinations = { "AVN", "V", "AN", "VN", "N", "A", "AV", "PROT" };
string[] Consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
string FinalVerb = "";
string PluralNoun = "";
int CreateCount = 20;
Random random = new Random();

string Common = libAnna.WHITE + libAnna.BOLD;
string Fine = libAnna.BOLD + libAnna.PURPLE;
string Magical = libAnna.BOLD + libAnna.CYAN;
string Rare = libAnna.BOLD + libAnna.YELLOW;
string Legendary = libAnna.YELLOW;
string Unique = libAnna.BOLD + libAnna.RED;
string SetItem = libAnna.BOLD + libAnna.GREEN;
string[] Qualities = { Common, Fine, Magical, Rare, Legendary, SetItem, Unique };

string[] Adjectives = File.ReadAllLines(AdjFileName);
string[] Verbs = File.ReadAllLines(VerbFileName);
string[] Nouns = File.ReadAllLines(NounFileName);
string[] PastVerbs = File.ReadAllLines(PastVerbFileName);
string[] IngVerbs = File.ReadAllLines(IngVerbFileName);

Console.WriteLine("\n" + ProgramName + " " + ProgramVersion + "\n");
Console.WriteLine($"Legend:{ Common } Common{ libAnna.ENDC }, { Fine }Fine{ libAnna.ENDC }, { Magical}Magical{libAnna.ENDC}, {Rare}Rare{libAnna.ENDC}, {Legendary}Legendary{libAnna.ENDC}, {Unique}Unique{libAnna.ENDC}, {SetItem}Set{libAnna.ENDC}\n");

void GenerateItems(int row)
{
    string ItemType = ItemTypes[random.Next(ItemTypes.Length)];
    string Verb = Verbs[random.Next(Verbs.Length)];
    string Adjective = Adjectives[random.Next(Adjectives.Length)];
    string Noun = Nouns[random.Next(Nouns.Length)];
    string PastVerb = PastVerbs[random.Next(PastVerbs.Length)];
    string IngVerb = IngVerbs[random.Next(IngVerbs.Length)];
    string VerbType = VerbTypes[random.Next(VerbTypes.Length)];
    string Combination = Combinations[random.Next(Combinations.Length)];
    string LastChar = Noun[Noun.Length - 1].ToString();
    string CurrentRow = libAnna.BOLD + libAnna.BLACK + String.Format("{0, 2}: ", row.ToString()) + libAnna.ENDC;
    string ItemQuality = Qualities[random.Next(Qualities.Length)];
    
    switch (LastChar)
    {
        case "s":
            PluralNoun = Noun;
            break;
        case "y":
            if (Consonants.Any(Noun.Substring(Noun.Length - 2, 1).Contains)) { PluralNoun = Noun.Substring(0, Noun.Length - 1) + "ies"; }
            else { PluralNoun = Noun + "s"; }
            break;
        case "f":
            PluralNoun = Noun.Substring(0, Noun.Length - 1) + "ves";
            break;
        case "x":
        case "h":
            PluralNoun = Noun + "es";
            break;
        default:
            PluralNoun = Noun + "s";
            break;
    }

    switch (VerbType)
    {
        case "basic":
            FinalVerb = Verb;
            break;
        case "past":
            FinalVerb = PastVerb;
            break;
        case "ing":
            FinalVerb = IngVerb;
            break;
    }

    switch (Combination)
    {
        case "AVN":
            Console.WriteLine(CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + FinalVerb.Capitalize() + " " + Noun.Capitalize() + libAnna.ENDC);
            break;
        case "V":
            Console.WriteLine(CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + IngVerb.Capitalize() + libAnna.ENDC);
            break;
        case "AN":
            Console.WriteLine(CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + Noun.Capitalize() + libAnna.ENDC);
            break;
        case "VN":
            Console.WriteLine(CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + FinalVerb.Capitalize() + " " + Noun.Capitalize() + libAnna.ENDC);
            break;
        case "N":
            Console.WriteLine(CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Noun.Capitalize() + libAnna.ENDC);
            break;
        case "A":
            Console.WriteLine(CurrentRow + ItemQuality + Adjective.Capitalize() + " " + ItemType.Capitalize() + libAnna.ENDC);
            break;
        case "AV":
            Console.WriteLine(CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + IngVerb.Capitalize() + libAnna.ENDC);
            break;
        case "PROT":
            Console.WriteLine(CurrentRow + ItemQuality + ItemType.Capitalize() + " of Protection from " + PluralNoun.Capitalize() + libAnna.ENDC);
            break;
    }
}

for (int i = 0; i < CreateCount; i++)
{
    GenerateItems(i);
}
Console.WriteLine("\n");