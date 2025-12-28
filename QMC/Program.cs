bool Auto = false;
int force = 0;

Console.WriteLine("Welcome to the 'Quick Memory Consumer'!");
Console.WriteLine("This project's target is to create an experimental DiskUsageSim named QMC.");
Console.Write("\nForce: ");
if(!Auto)
{
    force = int.Parse(Console.ReadLine());
}
else
{
    force = 10000;
}

string userName = Environment.UserName;
string rndPath = @"C:\Users\omere\Desktop\TestDirectory\QMC.txt";

DriveInfo[] drives = DriveInfo.GetDrives();

string diskName = null;
string path1;
string path2;
string path3;

List<string> allPaths = new List<string>();
List<string> allRndNames = new List<string>();

int Success = 0;
int Failed = 0;

float maxDiskSize = 0;
DriveInfo UseDisk = null;

foreach (DriveInfo drive in drives)
{
    if (drive.TotalSize > maxDiskSize)
    {
        maxDiskSize = drive.TotalSize;
        UseDisk = drive;
    }
}
Console.WriteLine(UseDisk.Name);
diskName = UseDisk.Name;

void changeDisk()
{
    try
    {
        if (UseDisk.AvailableFreeSpace < 100)
        {
            List<float> Sizes = new List<float>();
            foreach (DriveInfo drive in drives)
            {
                Sizes.Add(drive.TotalSize);
            }
            Sizes.Sort();
            foreach (DriveInfo drive in drives)
            {
                if (drive.TotalSize == Sizes[Sizes.Count-2])
                    UseDisk = drive;
            }
            diskName = UseDisk.Name;
        }
    }
    catch { }
}

SelectPath();

void SelectPath()
{
    changeDisk();

    Random rnd = new Random();

    try
    {
        if (Directory.GetDirectories(diskName).Length > 0)
        {
            foreach (string subPaths in Directory.GetDirectories(diskName))
            {
                Console.WriteLine(subPaths);
            }
            path1 = Directory.GetDirectories(diskName)[rnd.Next(Directory.GetDirectories(diskName).Length - 1)];
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine(path1); Console.ResetColor();

            if (Directory.GetDirectories(path1).Length > 0)
            {
                foreach (string subPaths in Directory.GetDirectories(path1))
                {
                    Console.WriteLine(subPaths);
                }
                path2 = Directory.GetDirectories(path1)[rnd.Next(Directory.GetDirectories(path1).Length - 1)];
                Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine(path2); Console.ResetColor();

                if (Directory.GetDirectories(path2).Length > 0)
                {
                    foreach (string subPaths in Directory.GetDirectories(path2))
                    {
                        Console.WriteLine(subPaths);
                    }
                    path3 = Directory.GetDirectories(path2)[rnd.Next(Directory.GetDirectories(path2).Length - 1)];
                    Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine(path3); Console.ResetColor();

                    rndPath = path3;
                }
                else { rndPath = path2; }
            }
            else { rndPath = path1; }
        }
        else { rndPath = diskName; }
    }
    catch { }
}

void retry()
{
    SelectPath();
}

void createFile()
{
    try
    {
        string rndName = Path.GetRandomFileName();
        StreamWriter sw = File.CreateText(rndPath + @"\" + rndName + ".txt");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(rndPath + @"\" + rndName + ".txt");
        Console.ResetColor();
        for (int i = 0; i < force*100; i++)
        {
            sw.WriteLine(rndName);
        }
        allPaths.Add(rndPath);
        allRndNames.Add(rndName);
        Success++;

        sw.Close();
    }
    catch (Exception ex) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(ex); Console.ResetColor(); Failed++; }
}

Thread.Sleep(1);
for (int i = 0; i < force; i++)
{
    SelectPath();
    createFile();
}

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Succesful: " + Success);
Console.WriteLine("Failed: " + Failed);
Console.ResetColor();

string afterProcess = Console.ReadLine();

if (afterProcess == "ReverseAll")
{
    try
    {
        int Nn = 0;
        foreach (string oldPath in allPaths)
        {
            File.Delete(oldPath + @"\" + allRndNames[Nn] + ".txt");
            Nn++;
        }
    }
    catch { }
}

else if(afterProcess == "DeleteAll")
{
    try
    {
        foreach (string oldPath in allPaths)
        {
            // Directory.Delete(oldPath); --> Removed
        }
    }
    catch { }
}