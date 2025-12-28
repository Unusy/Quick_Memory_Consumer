# Quick Memory Consumer
C# Resource Stress Testing Tool | An experimental project exploring C# File I/O, system drive analysis, and automated directory traversal.

This project is for educational and research purposes only. Running this script will generate a large volume of files and consume significant disk space. The "DeleteAll" function is destructive. Use this only in a controlled virtual environment (VM) or a dedicated test directory. The author is not responsible for any data loss.

## 🔍 Overview
QMC is a C# console application designed to demonstrate how a program can interact with the Windows File System to simulate "Resource Exhaustion." It analyzes available hardware drives, identifies the largest volume, and performs a randomized directory crawl to distribute data.

## 🛠️ Technical Features
Drive Intelligence: Uses DriveInfo to dynamically target the drive with the highest capacity.

Randomized Traversal: Implements a recursive-style logic to pick random subdirectories for file placement.

Mass I/O Generation: Utilizes StreamWriter to generate high-volume text data.

Cleanup Protocols: Features two distinct modes for post-process management: ReverseAll (Targeted file deletion) and DeleteAll (Directory purging).

## 🚀 How it Works
* Selection: The script identifies the primary storage drive.
* Pathing: It "drills down" into the folder structure using Directory.GetDirectories.
* Inflation: It creates .txt files filled with a specified "force" (multiplier) of data strings.
* Tracking: All created paths are stored in a List<string> to allow for the reversal of the process.
