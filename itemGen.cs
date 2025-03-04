// Generates random magical items for TTRPGs.

using System.IO;
using System.Linq;
using libAnnaC;

const string ProgramName = "itemGen";
const string ProgramVersion = "0.5.x";
const string libDir = ".\\libAnna\\";
const string AdjFileName = libDir + "english_adjectives.txt";
const string VerbFileName = libDir + "english_verbs.txt";
const string PastVerbFileName = libDir + "english_verbs_past.txt";
const string IngVerbFileName = libDir + "english_verbs_ing.txt";
const string NounFileName = libDir + "english_nouns.txt";

string[] ItemTypes = { "sword", "axe", "wand", "shield", "tome", "armor", "ring", "amulet", "bracers", "boots", "sash", "dagger", "bow", "mace", "robe", "cloak" };
string[] SetTypes = { "Vestments", "Clothes", "Attire", "Apparel", "Rags", "Garb", "Kit", "Outfit", "Trappings", "Instruments", "Gear", "Regalia", "Getup", "Ensemble", "Raiment", "Garments" };
string[] VerbTypes = { "basic", "past", "ing" };
string[] Combinations = { "AVN", "V", "AN", "VN", "N", "A", "AV", "PROT" };
string[] Consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
string FinalVerb = "";
string PluralNoun = "";
string SetName = "";
string SetTitle = "";
int CreateCount = 20;
int VerbIndex = 0;
Random random = new Random();

const string Common = libAnna.WHITE + libAnna.BOLD;
const string Fine = libAnna.BOLD + libAnna.PURPLE;
const string Magical = libAnna.BOLD + libAnna.CYAN;
const string Rare = libAnna.BOLD + libAnna.YELLOW;
const string Legendary = libAnna.YELLOW;
const string Unique = libAnna.BOLD + libAnna.RED;
const string SetItem = libAnna.BOLD + libAnna.GREEN;
string[] Qualities = { Common, Fine, Magical, Rare, Legendary, SetItem, Unique };

string[] Adjectives = [];
string[] Verbs = [];
string[] Nouns = [];
string[] PastVerbs = [];
string[] IngVerbs = [];

string[] ReadFile(string fileName)
{
    string[] arrayName = [];
    try { arrayName = File.ReadAllLines(fileName); return arrayName; }
    catch (FileNotFoundException) { Console.WriteLine($"\nError: File {fileName} does not exist. Ending Program.\n"); Environment.Exit(1); return arrayName; }
}

Adjectives = ReadFile(AdjFileName);
Nouns = ReadFile(NounFileName);
PastVerbs = ReadFile(PastVerbFileName);
IngVerbs = ReadFile(IngVerbFileName);
Verbs = ReadFile(VerbFileName);

if (PastVerbs.Length == IngVerbs.Length && PastVerbs.Length == Verbs.Length) { Console.Clear(); }
else { Console.WriteLine("\nVerb files mismatched. Make sure all files are present and correct. Ending Program.\n"); Environment.Exit(1); }

Console.WriteLine(ProgramName + " " + ProgramVersion + "\n");
Console.WriteLine($"Legend:{ Common } Common{ libAnna.ENDC }, { Fine }Fine{ libAnna.ENDC }, { Magical}Magical{libAnna.ENDC}, {Rare}Rare{libAnna.ENDC}, {Legendary}Legendary{libAnna.ENDC}, {Unique}Unique{libAnna.ENDC}, {SetItem}Set{libAnna.ENDC}\n");

string GenerateItems(int row)
{
    string ItemType = ItemTypes[random.Next(ItemTypes.Length)];
    string VerbType = VerbTypes[random.Next(VerbTypes.Length)];
    string Adjective = Adjectives[random.Next(Adjectives.Length)];
    string Noun = Nouns[random.Next(Nouns.Length)];

    VerbIndex = random.Next(PastVerbs.Length);
   
    string PastVerb = PastVerbs[VerbIndex];
    string IngVerb = IngVerbs[VerbIndex];
    string Verb = Verbs[VerbIndex];

    string Combination = Combinations[random.Next(Combinations.Length)];
    string LastChar = Noun[Noun.Length - 1].ToString();
    string CurrentRow = libAnna.BOLD + libAnna.BLACK + String.Format("{0, 2}: ", row.ToString()) + libAnna.ENDC;
    string ItemQuality = Qualities[random.Next(Qualities.Length)];
    string SetType = SetTypes[random.Next(SetTypes.Length)];
    string ItemOutput = "";

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
            PluralNoun = Noun;
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
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + FinalVerb.Capitalize() + " " + Noun.Capitalize() + libAnna.ENDC;
            SetTitle = IngVerb.Capitalize();
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
            SetTitle = Verb.Capitalize();
            break;
        case "PROT":
            ItemOutput = CurrentRow + ItemQuality + ItemType.Capitalize() + " of Protection from " + PluralNoun.Capitalize() + libAnna.ENDC;
            SetTitle = Noun.Capitalize();
            break;
    }

    if (ItemQuality == SetItem) { SetName = $" ({SetType} of {SetTitle})"; if (Combination == "AN" || Combination == "A") { SetName = $" ({SetTitle} {SetType})"; } }
    else { SetName = ""; }

    return $"{ItemOutput} {libAnna.BOLD}{libAnna.BLUE}{SetName}{libAnna.ENDC}";
}

for (int i = 1; i < CreateCount + 1; i++)
{
    Console.WriteLine(GenerateItems(i));
}
Console.WriteLine("\n");