using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    //Facade
    public class TV
    {
        public void On() => Console.WriteLine("TV is now ON.");
        public void Off() => Console.WriteLine("TV is now OFF.");
        public void SetChannel(int channel) => Console.WriteLine($"TV channel set to {channel}.");
    }

    public class AudioSystem
    {
        public void On() => Console.WriteLine("Audio system is now ON.");
        public void Off() => Console.WriteLine("Audio system is now OFF.");
        public void SetVolume(int level) => Console.WriteLine($"Audio volume set to {level}.");
    }

    public class DVDPlayer
    {
        public void Play() => Console.WriteLine("DVD is now playing.");
        public void Pause() => Console.WriteLine("DVD is paused.");
        public void Stop() => Console.WriteLine("DVD has stopped.");
    }

    public class GameConsole
    {
        public void On() => Console.WriteLine("Game console is now ON.");
        public void StartGame(string game) => Console.WriteLine($"Game '{game}' is now running.");
    }
    public class HomeTheaterFacade
    {
        private readonly TV _tv;
        private readonly AudioSystem _audioSystem;
        private readonly DVDPlayer _dvdPlayer;
        private readonly GameConsole _gameConsole;

        public HomeTheaterFacade(TV tv, AudioSystem audioSystem, DVDPlayer dvdPlayer, GameConsole gameConsole)
        {
            _tv = tv;
            _audioSystem = audioSystem;
            _dvdPlayer = dvdPlayer;
            _gameConsole = gameConsole;
        }

        public void WatchMovie()
        {
            Console.WriteLine("Setting up for movie night...");
            _tv.On();
            _audioSystem.On();
            _dvdPlayer.Play();
            _audioSystem.SetVolume(30);
        }

        public void EndMovie()
        {
            Console.WriteLine("Shutting down movie night...");
            _dvdPlayer.Stop();
            _audioSystem.Off();
            _tv.Off();
        }

        public void PlayGame(string game)
        {
            Console.WriteLine("Setting up for gaming session...");
            _tv.On();
            _audioSystem.On();
            _gameConsole.On();
            _gameConsole.StartGame(game);
            _audioSystem.SetVolume(40);
        }

        public void TurnOffSystem()
        {
            Console.WriteLine("Turning off the system...");
            _tv.Off();
            _audioSystem.Off();
            _dvdPlayer.Stop();
            _gameConsole.On();
        }
    }
    //Component
    public abstract class FileSystemComponent
    {
        public string Name { get; set; }
        public abstract void Display(int depth);
        public abstract int GetSize();
    }
    public class File : FileSystemComponent
    {
        public int Size { get; set; }

        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + " File: " + Name + " (" + Size + " KB)");
        }

        public override int GetSize()
        {
            return Size;
        }
    }
    public class Directory : FileSystemComponent
    {
        private List<FileSystemComponent> _components = new List<FileSystemComponent>();

        public Directory(string name)
        {
            Name = name;
        }

        public void Add(FileSystemComponent component)
        {
            if (!_components.Contains(component))
            {
                _components.Add(component);
            }
        }

        public void Remove(FileSystemComponent component)
        {
            if (_components.Contains(component))
            {
                _components.Remove(component);
            }
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + " Directory: " + Name);
            foreach (var component in _components)
            {
                component.Display(depth + 2);
            }
        }

        public override int GetSize()
        {
            int totalSize = 0;
            foreach (var component in _components)
            {
                totalSize += component.GetSize();
            }
            return totalSize;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Facade
            TV tv = new TV();
            AudioSystem audioSystem = new AudioSystem();
            DVDPlayer dvdPlayer = new DVDPlayer();
            GameConsole gameConsole = new GameConsole();

            HomeTheaterFacade homeTheater = new HomeTheaterFacade(tv, audioSystem, dvdPlayer, gameConsole);

            homeTheater.WatchMovie();
            Console.WriteLine();
            homeTheater.EndMovie();
            Console.WriteLine();
            homeTheater.PlayGame("Adventure Quest");
            Console.WriteLine();
            homeTheater.TurnOffSystem();
            Console.WriteLine();

            //Component
            Directory root = new Directory("Root");
            Directory documents = new Directory("Documents");
            Directory pictures = new Directory("Pictures");

            File file1 = new File("Resume.docx", 200);
            File file2 = new File("Photo1.jpg", 1500);
            File file3 = new File("Presentation.pptx", 750);

            documents.Add(file1);
            pictures.Add(file2);
            pictures.Add(file3);

            root.Add(documents);
            root.Add(pictures);

            Console.WriteLine("Displaying file system structure:");
            root.Display(1);

            Console.WriteLine($"\nTotal size of root directory: {root.GetSize()} KB");
        }
    }
}
