// Generates random magical items for TTRPGs.

using System.IO;
using System.Linq;
using libAnnaC;

const string ProgramName = "itemGen";
const string ProgramVersion = "0.6.x";
const string libDir = ".\\libAnna\\";
const string AdjFileName = libDir + "english_adjectives.txt";
const string VerbFileName = libDir + "english_verbs.txt";
const string PastVerbFileName = libDir + "english_verbs_past.txt";
const string IngVerbFileName = libDir + "english_verbs_ing.txt";
const string NounFileName = libDir + "english_nouns.txt";
const string Common = libAnna.WHITE + libAnna.BOLD;
const string Fine = libAnna.BOLD + libAnna.PURPLE;
const string Magical = libAnna.BOLD + libAnna.CYAN;
const string Rare = libAnna.BOLD + libAnna.YELLOW;
const string Legendary = libAnna.YELLOW;
const string Unique = libAnna.BOLD + libAnna.RED;
const string SetItem = libAnna.BOLD + libAnna.GREEN;
const int CreateCount = 20;

string[] ItemTypes = { "sword", "axe", "wand", "shield", "tome", "armor", "ring", "amulet", "bracers", "boots", "sash", "dagger", "bow", "mace", "robe", "cloak" };
string[] SetTypes = { "Vestments", "Clothes", "Attire", "Apparel", "Rags", "Garb", "Kit", "Outfit", "Trappings", "Instruments", "Gear", "Regalia", "Getup", "Ensemble", "Raiment", "Garments" };
string[] VerbTypes = { "basic", "past", "ing" };
string[] Combinations = { "AVN", "V", "AN", "VN", "N", "A", "AV", "PROT", "NN", "ANN" };
string[] Qualities = { Common, Fine, Magical, Rare, Legendary, SetItem, Unique };
string[] Adjectives = [];
string[] Verbs = [];
string[] Nouns = [];
string[] PastVerbs = [];
string[] IngVerbs = [];

string SetName = "";
string SetTitle = "";
int VerbIndex = 0;
Random random = new Random();

string MakePlural(string noun)
{
    string lCharacter = noun[noun.Length - 1].ToString();
    string[] Consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
    string pNoun = string.Empty;
    
    switch (lCharacter)
    {
        case "s":
            pNoun = noun;
            break;
        case "y":
            if (Consonants.Any(noun.Substring(noun.Length - 2, 1).Contains)) { pNoun = noun.Substring(0, noun.Length - 1) + "ies"; }
            else { pNoun = noun + "s"; }
            break;
        case "f":
            pNoun = noun.Substring(0, noun.Length - 1) + "ves";
            break;
        case "x":
        case "h":
            pNoun = noun;
            break;
        default:
            pNoun = noun + "s";
            break;
    }
    if (noun == "wife") { pNoun = "wives"; } // special case for this word.
    return pNoun;
}

string[] ReadFile(string fileName)
{
    string[] arrayName = [];
    try
    {
        arrayName = File.ReadAllLines(fileName);
        return arrayName;
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"\nError: File {fileName} does not exist. Ending Program.\n");
        Environment.Exit(1);
        return arrayName;
    }
}

string ChooseVerbType(string basic, string ing, string past, string type)
{
    string result = string.Empty;
    switch (type)
    {
        case "basic":
            result = basic;
            break;
        case "past":
            result = past;
            break;
        case "ing":
            result = ing;
            break;
    }
    return result;
}

string PickRandom(string[] input)
{
    string result = string.Empty;
    result = input[random.Next(input.Length)];
    return result;
}

Adjectives = ReadFile(AdjFileName);
Nouns = ReadFile(NounFileName);
PastVerbs = ReadFile(PastVerbFileName);
IngVerbs = ReadFile(IngVerbFileName);
Verbs = ReadFile(VerbFileName);

if (PastVerbs.Length == IngVerbs.Length && PastVerbs.Length == Verbs.Length) { Console.Clear(); }
else { Console.WriteLine("\nVerb files mismatched. Make sure all files are present and correct. Ending Program.\n"); Environment.Exit(1); }

Console.WriteLine(ProgramName + " " + ProgramVersion + "\n");
Console.WriteLine($"Legend:{ Common } Common{libAnna.ENDC}, {Fine}Fine{libAnna.ENDC}, {Magical}Magical{libAnna.ENDC}, {Rare}Rare{libAnna.ENDC}, {Legendary}Legendary{libAnna.ENDC}, {Unique}Unique{libAnna.ENDC}, {SetItem}Set{libAnna.ENDC}\n");

string GenerateItems(int row)
{
    string ItemType = PickRandom(ItemTypes);
    string VerbType = PickRandom(VerbTypes);
    string Adjective = PickRandom(Adjectives);
    string Noun = PickRandom(Nouns);
    string Noun2 = PickRandom(Nouns);
    string Combination = PickRandom(Combinations);
    string ItemQuality = PickRandom(Qualities);
    string SetType = PickRandom(SetTypes);

    VerbIndex = random.Next(PastVerbs.Length);
   
    string PastVerb = PastVerbs[VerbIndex];
    string IngVerb = IngVerbs[VerbIndex];
    string Verb = Verbs[VerbIndex];
    string CurrentRow = libAnna.BOLD + libAnna.BLACK + String.Format("{0, 2}: ", row.ToString()) + libAnna.ENDC;
    string ItemOutput = "";

    string FinalVerb = ChooseVerbType(Verb, IngVerb, PastVerb, VerbType);
    string PluralNoun = MakePlural(Noun);
    string PluralNoun2 = MakePlural(Noun2);

    switch (Combination)
    {
        case "AVN":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + FinalVerb.Capitalize() + " " + Noun.Capitalize() + libAnna.ENDC;
            SetTitle = Adjective.Capitalize() + " " + PluralNoun.Capitalize();
            break;
        case "V":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + IngVerb.Capitalize() + libAnna.ENDC;
            SetTitle = Verb.Capitalize();
            break;
        case "AN":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + Noun.Capitalize() + libAnna.ENDC;
            SetTitle = Adjective.Capitalize();
            break;
        case "VN":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + FinalVerb.Capitalize() + " " + Noun.Capitalize() + libAnna.ENDC;
            SetTitle = IngVerb.Capitalize();
            break;
        case "N":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Noun.Capitalize() + libAnna.ENDC;
            SetTitle = PluralNoun.Capitalize();
            break;
        case "A":
            ItemOutput = CurrentRow + ItemQuality + Adjective.Capitalize() + " " + ItemType.Capitalize() + libAnna.ENDC;
            SetTitle = Adjective.Capitalize();
            break;
        case "AV":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + IngVerb.Capitalize() + libAnna.ENDC;
            SetTitle = PastVerb.Capitalize();
            break;
        case "NN":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Noun.Capitalize() + " " + Noun2.Capitalize() + libAnna.ENDC;
            SetTitle = PluralNoun.Capitalize();
            break;
        case "ANN":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + Noun.Capitalize() + " " + Noun2.Capitalize() + libAnna.ENDC;
            SetTitle = Adjective.Capitalize() + " " + PluralNoun2.Capitalize();
            break;
        case "PROT":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of Protection from " + PluralNoun.Capitalize() + libAnna.ENDC;
            SetTitle = Noun.Capitalize();
            break;
    }

    if (ItemQuality == SetItem)
    {
        string[] specialCases = { "AN", "A", "AV" };
        SetName = $" ({SetType} of {SetTitle})";
        if (specialCases.Any(Combination.Contains))
        {
            SetName = $" ({SetTitle} {SetType})";
        }
        return $"{ItemOutput} {libAnna.BOLD}{libAnna.BLUE}{SetName}{libAnna.ENDC}";
    }
    else
    {
        SetName = "";
        return ItemOutput;
    }
}

for (int i = 1; i < CreateCount + 1; i++)
{
    Console.WriteLine(GenerateItems(i));
}
Console.WriteLine("\n");